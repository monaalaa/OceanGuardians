using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Calss manage communication batween Player and othe Classes
/// Ex: If Player Collide with dragable object fire OnPlayerCollide so darag action created 
/// </summary>
/// 

[RequireComponent(typeof(PlayerDataManager))]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Action<Collider> OnPlayerCollide;

    public Action<string, GameObject> OnActionCreated;
    public Action<string> OnActionDestroied;

    public Action OnPlayerStopMoving;

    public Action OnDeattachPlayerFromRope;

    public Action OnPlayerDied;
    public Action OnPlayeJump;

    public Action OnHitGround;
    public Action OnExitHitGround;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    internal void InvokeOnPlayerCollide(Collider collision)
    {
        if (OnPlayerCollide != null)
            OnPlayerCollide.Invoke(collision);
    }

    internal void InvokeOnActionPressed(string actionName, GameObject actionObject = null)
    {
        if (OnActionCreated != null)
            OnActionCreated.Invoke(actionName, actionObject);
    }

    internal void InvokeOnActionDestroied(string actionName)
    {
        if (OnActionDestroied != null)
            OnActionDestroied.Invoke(actionName);
    }

    internal void InvokeOnDeattachPlayerFromRope()
    {
        if (OnDeattachPlayerFromRope != null)
            OnDeattachPlayerFromRope.Invoke();
    }

    internal void InvokeOnPlayerDied()
    {
        StartCoroutine(ResetPlayerPosition());
        if (OnPlayerDied != null)
            OnPlayerDied.Invoke();
    }

    internal void InvokeOnPlayeJump()
    {
        if (OnPlayeJump != null)
            OnPlayeJump.Invoke();
    }

    IEnumerator ResetPlayerPosition()
    {
        yield return new WaitForSeconds(1.4f);
        PlayerController.Instance.LocalPlayerInstance.gameObject.SetActive(true);
        Vector3 pos = new Vector3(CheckPointManager.LastCheckPoint.position.x,

        CheckPointManager.LastCheckPoint.position.y, PlayerController.Instance.LocalPlayerInstance.transform.localPosition.z);

        PlayerController.Instance.LocalPlayerInstance.transform.localPosition = pos;
        
        //Spaghetti way to fix bug :D
        
        #region
        PlayerController.Instance.LocalPlayerInstance.Grounded = false;
        PlayerController.Instance.LocalPlayerInstance.Grounded = true;
        #endregion

        PlayerController.Instance.playerAnimator.SetBool("Grounded", true);
        PlayerController.Instance.playerAnimator.SetFloat("Direction", 1);
        PlayerController.Instance.LocalPlayerInstance.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
    }

    public void InstantiatePlayer(PlayerData playerData)
    {
        GameObject plyer = GameObject.Instantiate(Resources.Load("PlayerAvatar/Player" /*+ playerData.AvatarPrefab*/)) as GameObject;
        plyer.transform.position = new Vector3(playerData.StartPosition.X, playerData.StartPosition.Y, playerData.StartPosition.Z);
        //Change  material 
    }

    public void InvokeOnPlayerStopMoving()
    {
        if (OnPlayerStopMoving != null)
            OnPlayerStopMoving.Invoke();
    }
}
