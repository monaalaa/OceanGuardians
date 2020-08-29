using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : PlayerActions
{
    public override void DoAction()
    {
        PlayerController.Instance.playerAnimator.SetTrigger("Attack");
    }
}
