using UnityEngine;

public abstract class PlayerController
{
    public static PlayerController Instance;
    internal Rigidbody myRigidbody;

    internal Animator playerAnimator;

    protected Transform playerTransform;

    private PlayerMain localPlayerInstance;
    public PlayerMain LocalPlayerInstance
    {
        get
        {
            return localPlayerInstance;
        }

        set
        {
            localPlayerInstance = value;
        }
    }

    public PlayerController(PlayerMain localPlayer)
    {
        Instance = this;

        localPlayerInstance = localPlayer;
        GameManager.Instance.OnInputPressed += TakeAction;

        myRigidbody = localPlayerInstance.gameObject.GetComponent<Rigidbody>();
        playerAnimator = localPlayerInstance.gameObject.GetComponent<Animator>();

        playerTransform = LocalPlayerInstance.gameObject.transform;
    }
   
    internal void ActionKey(string actionName)
    {
        PlayerActions action = localPlayerInstance.Actions.Find(x => x.ToString() == actionName);
        action.DoAction();
    }

    void DestroyPlayer()
    {
        GameObject.Destroy(localPlayerInstance);
    }

    public void TakeAction(Vector2 direction, Vector2 value)
    {
        MovePlayer(direction, value);
        VerticalAction(direction, value);
    }

    public virtual void MovePlayer(Vector2 direction, Vector2 value)
    {}

    protected virtual void VerticalAction(Vector2 direction, Vector2 value) { }

    public void KeepDistance(float distanceInX, GameObject ObjectToPreformActionOn)
    {
        //TODO: make animation root motion
        #region
        Vector3 temp = LocalPlayerInstance.transform.position;
        float x = ObjectToPreformActionOn.transform.position.x - distanceInX * LocalPlayerInstance.Direction;

        LocalPlayerInstance.transform.position = new Vector3(x, temp.y, temp.z);
        #endregion
    }

    public void ChangeStatus(CharacterStatus status)
    {
        LocalPlayerInstance.playerCharacter = status;
    }

    public void ClimbHelper()
    {
        playerAnimator.SetBool("Climb", !playerAnimator.GetBool("Climb"));
        myRigidbody.isKinematic = !myRigidbody.isKinematic;
        myRigidbody.useGravity = !myRigidbody.useGravity;
     }

    public void ToggleRootMotion()
    {
        playerAnimator.applyRootMotion = !playerAnimator.applyRootMotion;
    }
}
