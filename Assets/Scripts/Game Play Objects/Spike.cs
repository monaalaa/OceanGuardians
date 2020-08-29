using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : DeadZone
{
    protected override void PlayDeathAnimationbehaviour()
    {
        //Change Player animation
        PlayerController.Instance.playerAnimator.SetTrigger("Die");
    }
}