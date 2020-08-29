using System;
using System.Collections.Generic;
using UnityEngine;
enum PLayerActionsEnum
{
    DoAction,
    Attack,
    None
}
public class Player : MonoBehaviour
{
    bool grounded;
    internal bool SandGround;

    [SerializeField]
    GameObject DustEffect;

    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public Collider playerCollider;
    public bool Grounded
    {
        get { return grounded; }
        set
        {
            if (grounded != value)
            {
                grounded = value;

                if (grounded)
                {
                    CharacterStatus = CharacterStatus.Grounded;
                    PlayerManager.Instance.OnHitGround();
                    PlayerController.Instance.playerAnimator.SetBool("Grounded", true);
                }

                else
                {
                    if (CharacterStatus == CharacterStatus.Grounded)
                    {
                        PlayerManager.Instance.OnExitHitGround();
                    }
                }
            }
        }
    }

    PlayerController playerControler;
    PLayerActionsEnum pLayerActionsEnum = PLayerActionsEnum.None;
    RaycastHit hit;
    string action;
    int layerMask = 1 << 8;

    internal float Direction = 1;
    internal CharacterStatus CharacterStatus = CharacterStatus.Grounded;
    internal List<PlayerActions> Actions = new List<PlayerActions>();
    public bool CanClimb;
    private void Start()
    {
        InstantiatePlayerControllerBasedOnGameMode();
        PlayerManager.Instance.OnActionDestroied += DestroyAction;
        GameManager.Instance.ExplosionOccured += OnExplosionOccured;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(GroundCheck.transform.position, 0.2f);
    }

    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(GroundCheck.transform.position, 0.2f, GroundLayer).Length > 0)
        {
            if (Physics.OverlapSphere(GroundCheck.transform.position, 0.2f, GroundLayer)[0].gameObject.tag == "Sand")
            {
                SandGround = true;
            }
            else
            {
                SandGround = false;
            }
            Grounded = true;
        }
        else
        {
            Grounded = false;
            SandGround = false;
        }
        Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay((this.transform.position + new Vector3(0, -0.5f, 0)), fwd, Color.green);
        if (Physics.Raycast((this.transform.position + new Vector3(0, -0.5f, 0)), fwd, out hit, 0.75f, layerMask))
        {
            if (pLayerActionsEnum != PLayerActionsEnum.DoAction)
            {
                action = hit.collider.gameObject.tag;
                PlayerManager.Instance.InvokeOnActionPressed(action, hit.collider.gameObject);
                pLayerActionsEnum = PLayerActionsEnum.DoAction;
            }
        }
        else
        {
            if (pLayerActionsEnum != PLayerActionsEnum.Attack)
            {
                pLayerActionsEnum = PLayerActionsEnum.Attack;
                PlayerManager.Instance.InvokeOnActionDestroied(action);
                //  Reset Action Button to Attack
                PlayerManager.Instance.InvokeOnActionPressed("Attack");
            }
        }

        #region Keyboard Test
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            PreformAction("Jump");
        }
        #endregion

    }

    void InstantiatePlayerControllerBasedOnGameMode()
    {
        Type t = Type.GetType(GameManager.Instance.CurrentIslandMode.ToString() + "Controller");
        playerControler = Activator.CreateInstance(t, new object[] { this }) as PlayerController;
    }

    public void PreformAction(string actionName)
    {
        playerControler.ActionKey(actionName);
    }

    void DestroyAction(string actionName)
    {
        Actions.Remove(Actions.Find(x => x.ToString() == actionName));
    }

    private void OnDestroy()
    {
        PlayerDataManager.Instance.SavePlayerData();
        //PlayerController.Instance = null;
        //playerControler = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && CharacterStatus == CharacterStatus.Jump)
        {
            CharacterStatus = CharacterStatus.Grounded;
            
            PlayerManager.Instance.OnHitGround();
            PlayerController.Instance.playerAnimator.SetBool("Grounded", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager.Instance.InvokeOnPlayerCollide(other);
        if (other.tag == "Stairs")
        {
            Climb();
        }
        else if (other.tag == "EndStairs")
        {
            EndClimb();
            other.gameObject.SetActive(false);
        }
    }

    private void Climb()
    {
        CanClimb = true;
        PlayerController.Instance.ClimbHelper();
        PlayerController.Instance.playerAnimator.SetBool("jump", false);
        PlayerController.Instance.ChangeStatus(CharacterStatus.Climbing);
    }

    private void EndClimb()
    {
        CanClimb = false;
        PlayerController.Instance.playerAnimator.SetFloat("Climb ready", 1);
        PlayerController.Instance.ToggleRootMotion();
    }

    public void OnFinishClimbing()
    {
        CanClimb = false;
        PlayerController.Instance.playerAnimator.SetFloat("Forward Speed", 0);

        PlayerController.Instance.playerAnimator.SetFloat("Climb ready", 0);

        PlayerController.Instance.ClimbHelper();

        PlayerController.Instance.ChangeStatus(CharacterStatus.Grounded);
    }

    public void OnExplosionOccured(bool isPlayerAffected)
    {
        if (isPlayerAffected)
        {
            //empty all collected garbage.
            PlayerDataManager.Instance.UncollectAllLitter();
        }
    }

    public void ToggleDust(bool value)
    {
        DustEffect.SetActive(value);
    }
}