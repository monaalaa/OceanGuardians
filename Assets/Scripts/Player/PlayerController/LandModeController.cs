using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LandModeController : PlayerController
{
    float currentSpeed;
    float time = 0.8f;
    float _Direction;
    WaitForEndOfFrame wait = new WaitForEndOfFrame();

    bool slowDown;
    public LandModeController(PlayerMain localPlayer) : base(localPlayer)
    {
        //Instantiate player Action jumb
        localPlayer.Actions.Add(new Jump());
        PlayerManager.Instance.OnPlayerStopMoving += ResetPlayerMotion;
        PlayerManager.Instance.OnExitHitGround += ExitHitGround;
        playerAnimator.SetFloat("Direction", 1);
    }

    public override void MovePlayer(Vector2 direction, Vector2 value)
    {
        base.MovePlayer(direction, value);
         _Direction = (float)Math.Round(direction.x);

        if ((_Direction == 1 || _Direction == -1) && (value.x > 0.1 || value.x < -0.1)&&
            LocalPlayerInstance.playerCharacter != CharacterStatus.Died)
        {
            if (LocalPlayerInstance.playerCharacter == CharacterStatus.Hang)
            {
                myRigidbody.AddForce(Vector3.right * _Direction * 8);
            }

            else if (LocalPlayerInstance.playerCharacter != CharacterStatus.Pushing && LocalPlayerInstance.playerCharacter != CharacterStatus.Climbing)
            {
                if (value.x >= 0.6f)
                    value = new Vector2(1, value.y);
                //if (LocalPlayerInstance.SandGround && (value.x < -0.5f || value.x > 0.5f))
                //    LocalPlayerInstance.ToggleDust(true);
                //else
                //    LocalPlayerInstance.ToggleDust(false);

                Move(value);
            }

            else if (LocalPlayerInstance.playerCharacter == CharacterStatus.Pushing)
            {
                if (LocalPlayerInstance.Direction == _Direction)
                    Move(value);
            }
        }
    }

    void Move( Vector2 value)
    {
        currentSpeed = Math.Abs(value.x);

        myRigidbody.position += (playerTransform.forward * currentSpeed  * Time.deltaTime);
        if (time < 12)
            time += Time.deltaTime * 3;

        playerAnimator.SetFloat("Forward Speed", currentSpeed);

        ResetCameraRotation();

        // Change Player Animator based on direction
        RotatePlayerBasedOnDirection(_Direction);
    }

    void ResetPlayerMotion()
    {
        time = 0.8f;
        //LocalPlayerInstance.ToggleDust(false);
        if (LocalPlayerInstance.playerCharacter == CharacterStatus.Climbing)
        {
            playerAnimator.SetFloat("Climb Speed", 0);
        }
        else if (LocalPlayerInstance.playerCharacter == CharacterStatus.Grounded)
        {
            GameManager.Instance.StartCoroutineInAbstract(SlowDown());
        }
        else if (LocalPlayerInstance.playerCharacter == CharacterStatus.Jump)
        {
            GameManager.Instance.StartCoroutineInAbstract(Curve());
        }
    }

    IEnumerator SlowDown()
    {
        slowDown = true;
        float tempSpeed = currentSpeed;
        currentSpeed = 0;
        while (tempSpeed >= 0 && LocalPlayerInstance.playerCharacter == CharacterStatus.Grounded)
        {
            playerAnimator.SetFloat("Forward Speed", tempSpeed);
            playerAnimator.SetFloat("Direction", LocalPlayerInstance.Direction);
            myRigidbody.velocity = new Vector3(tempSpeed * LocalPlayerInstance.Direction * 2f, 0, 0);

            tempSpeed -= 0.03f;//0.02f;
            yield return wait;
        }
        GameManager.Instance.StopCoroutineInAbstract(SlowDown());
        slowDown = false;
        playerAnimator.SetFloat("Forward Speed", 0);
    }

    IEnumerator Curve()
    {
        while (currentSpeed >= 0 && LocalPlayerInstance.playerCharacter == CharacterStatus.Jump)
        {
            myRigidbody.velocity = new Vector3(currentSpeed * LocalPlayerInstance.Direction * 3.5f, myRigidbody.velocity.y, 0);

            currentSpeed -= 0.02f;
            yield return wait;
        }
        GameManager.Instance.StopCoroutineInAbstract(Curve());
    }

    void RotatePlayerBasedOnDirection(float direction)
    {
        if (LocalPlayerInstance.Direction != direction)
        {
            if (direction == 1)
            {
                LocalPlayerInstance.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }

            else if (direction == -1)
            {
                LocalPlayerInstance.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }

            LocalPlayerInstance.Direction = direction;
            playerAnimator.SetFloat("Direction", direction);

        }
    }

    /// <summary>
    /// Here the Vertical Action is making the camera move up and Down
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="value"></param>
    protected override void VerticalAction(Vector2 direction, Vector2 value)
    {
        float _Direction = (float)Math.Round(direction.y);
        //Change animation
        if (_Direction == 1 || _Direction == -1)
        {
            if (LocalPlayerInstance.playerCharacter != CharacterStatus.Climbing)
            {
                ChangeCameraDirection(Math.Round(direction.y));
            }
            else
            {
                //if (PlayerController.Instance.LocalPlayerInstance.CanClimb)
                    myRigidbody.position += (playerTransform.up * 0.5f * _Direction * Time.deltaTime);
                playerAnimator.SetFloat("Climb Speed", Math.Abs(value.y) * 2);
            }
        }
    }

    void ChangeCameraDirection(double Direction)
    {
        if ((Camera.main.transform.rotation.x * 100) <= 5 && (Camera.main.transform.rotation.x * 100) > -5)
        {
            Camera.main.transform.rotation *= Quaternion.AngleAxis((float)(0.5 * Direction) * Time.deltaTime, Vector3.left);
        }
    }

    void ResetCameraRotation()
    {
        //Camera.main.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    void ExitHitGround()
    {
        playerAnimator.SetBool("Grounded", false);
        GameManager.Instance.StopCoroutineInAbstract(SlowDown());
    }

}
