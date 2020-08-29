using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaModeController : PlayerController
{
    public SeaModeController(PlayerMain localPlayer):base(localPlayer)
    {
        myRigidbody.useGravity = false;
        myRigidbody.freezeRotation = true;
    }

    public override void MovePlayer(Vector2 direction, Vector2 value)
    {
        //Play motion Animation/ and Play controler based on GameMode
        Vector3 moveVector = (playerTransform.right * value.x + playerTransform.up * value.y).normalized;
        myRigidbody.transform.Translate(moveVector * 1.2f /*LocalPlayerInstance.Speed*/* Time.deltaTime);
    }

}
  