using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheckPointType
{
    InGameCheckPoint,
    GameEndCheckPoint
}
public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    Transform InitialCheckPoint;

    static Action<CheckPoint> ProgressSaver;
    public static Transform LastCheckPoint;

    private void Start()
    {
        LastCheckPoint = InitialCheckPoint;
        ProgressSaver += SetCheckPoint;
    }

    void SetCheckPoint(CheckPoint checkPoint)
    {
        //Save Player Progress
        if (checkPoint.pointType == CheckPointType.InGameCheckPoint)
        {
            LastCheckPoint = checkPoint.gameObject.transform;
        }

        //Player moved to End
        else if (checkPoint.pointType == CheckPointType.GameEndCheckPoint)
        {
            PlayerController.Instance.playerAnimator.SetTrigger("Victory");
            GameManager.Instance.InvokeEndGame();
        }
    }

    public static void InvokeProgressSaver(CheckPoint checkPoint)
    {
        if (ProgressSaver != null)
            ProgressSaver.Invoke(checkPoint);
    }

}
