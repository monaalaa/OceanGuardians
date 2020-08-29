using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMain : GameCharacters
{
    //ToDo: create scriptable object to contain player collider and animator "Rig Component" 
    public Collider playerCollider;

    public LayerMask GroundLayer;
    public Transform GroundCheck;

    bool grounded;
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
                    playerCharacter = CharacterStatus.Grounded;
                    PlayerManager.Instance.OnHitGround();
                    PlayerController.Instance.playerAnimator.SetBool("Grounded", true);
                }

                else
                {
                    if (playerCharacter == CharacterStatus.Grounded)
                    {
                        PlayerManager.Instance.OnExitHitGround();
                    }
                }
            }
        }
    }

    PlayerController playerControler;
    int rayCastLayerMask = 1 << 8;
    RaycastHit hit;

    string action;
    PLayerActionsEnum pLayerActionsEnum = PLayerActionsEnum.None;
    internal List<PlayerActions> Actions = new List<PlayerActions>();

    internal CharacterStatus playerCharacter = CharacterStatus.Grounded;
    internal float Direction = 1;

    Rigidbody rigidbody;
    Animator animator;

    private void Start()
    {
        InstantiatePlayerControllerBasedOnGameMode();
        PlayerManager.Instance.OnActionDestroied += DestroyAction;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(GroundCheck.transform.position, 0.2f, GroundLayer).Length > 0)
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }

        ExcuteRaycast(this.transform.TransformDirection(Vector3.forward));

         if (Input.GetKeyDown(KeyCode.Space))
        {
            PreformAction("Jump");
        }
    }

    private void ExcuteRaycast(Vector3 fwd)
    {
        if (Physics.Raycast((this.transform.position + new Vector3(0, -0.5f, 0)), fwd, out hit, 0.75f, rayCastLayerMask))
        {
            action = hit.collider.gameObject.tag;

            if (Actions.Where(i => i.GetType() == Type.GetType(action)) != null)
            {
                PlayerManager.Instance.InvokeOnActionPressed(action, hit.collider.gameObject);
            }
            pLayerActionsEnum = PLayerActionsEnum.DoAction;
        }
        else
        {
            if (pLayerActionsEnum != PLayerActionsEnum.Attack)
            {
                pLayerActionsEnum = PLayerActionsEnum.Attack;
                //Remove Old Action 
                PlayerManager.Instance.InvokeOnActionDestroied(action);
                //  Reset Action Button to Attack
                PlayerManager.Instance.InvokeOnActionPressed("Attack");
            }
        }
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

    public override void ExcuteDeath(string placeOfDeath)
    {
        animator.SetTrigger(placeOfDeath);
        playerCharacter = CharacterStatus.Died;
        PlayerManager.Instance.InvokeOnPlayerDied();
    }

    public override void ExcuteAddForce(float forceValue, Vector3 direction)
    {
        rigidbody.AddForce(forceValue * direction);
    }

    public override void ExcuteClimb()
    {
        playerCharacter = CharacterStatus.Climbing;

        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        animator.SetBool("Climb", true);
    }

    public override void DeExcuteClimb()
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        animator.SetBool("Climb", false);
    }

    public override void ExcuteClimbEdge()
    {
        animator.SetFloat("Climb ready", 1);
        PlayerController.Instance.ToggleRootMotion();
    }

    public override void DeExcuteClimbEdge()
    {
        animator.SetBool("Climb", false);
        PlayerController.Instance.ToggleRootMotion();
    }

    public override void ExcuteAttachToJoint()
    {
        // PlayerController.Instance.LocalPlayerInstance.Direction, 0.8f, 0);
        playerCharacter = CharacterStatus.Hang;
        animator.SetTrigger("Hang");
        animator.SetBool("jump", false);
        playerCollider.enabled = false;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        //All constrains is the same but FreezeRotationX
        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
    }

    public override void ExcuteDeAttachFromJoint()
    {
        playerCollider.enabled = true;
        transform.SetParent(null);
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
    }
}
