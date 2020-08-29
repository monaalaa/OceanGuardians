using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerActions
{
    PlayerMain myPlayer;
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    public Jump()
    {
        myPlayer = PlayerController.Instance.LocalPlayerInstance;
        PlayerManager.Instance.OnHitGround += HitGround;
    }

    public override void DoAction()
    {
        if (myPlayer.playerCharacter == CharacterStatus.Climbing)
        {
            PlayerController.Instance.ClimbHelper();
            PlayerController.Instance.ToggleRootMotion();
            PreparePlayerToJump();
        }

        else if (myPlayer.playerCharacter == CharacterStatus.Hang)
        {
            StopHanging();

            PreparePlayerToJump();
        }

        else if (myPlayer.playerCharacter == CharacterStatus.Grounded)
        {
            PreparePlayerToJump();
        }
    }

    private void StopHanging()
    {
        PlayerController.Instance.LocalPlayerInstance.transform.localRotation = Quaternion.Euler(0, 90 * PlayerController.Instance.LocalPlayerInstance.Direction, 0);

        PlayerManager.Instance.InvokeOnPlayeJump();
    }

    void PreparePlayerToJump()
    {
        PlayJumpSound();
        PlayerController.Instance.playerAnimator.SetFloat("Forward Speed", 0);
        GameManager.Instance.StartCoroutineInAbstract(WaitToJump());
        PlayerController.Instance.playerAnimator.SetBool("jump", true);
        PlayerController.Instance.playerAnimator.SetBool("Grounded", false);
        myPlayer.playerCharacter = CharacterStatus.Jump;
        GameManager.Instance.StartCoroutine(ChangeAnimationBasedOnVelocity());
    }

    public void HitGround()
    {
        if (myPlayer != null)
            PlayerController.Instance.ChangeStatus(CharacterStatus.Grounded);

        if (PlayerController.Instance.playerAnimator != null)
        {
            GameManager.Instance.StopCoroutineInAbstract(ChangeAnimationBasedOnVelocity());
            PlayerController.Instance.playerAnimator.SetBool("Grounded", true);
            PlayerController.Instance.playerAnimator.SetFloat("Forward Speed", 0);
            PlayerController.Instance.playerAnimator.SetBool("jump", false);
        }
    }

    IEnumerator ChangeAnimationBasedOnVelocity()
    {
        PlayerController.Instance.playerAnimator.SetFloat("Jump Speed", myPlayer.gameObject.GetComponent<Rigidbody>().velocity.y);

        yield return wait;


        if (myPlayer.playerCharacter != CharacterStatus.Grounded && PlayerController.Instance.playerAnimator.GetFloat("Jump Speed") != 0)
        {
            GameManager.Instance.StartCoroutine(ChangeAnimationBasedOnVelocity());
        }
        
    }

    IEnumerator WaitToJump()
    {
        yield return new WaitForSeconds(0.001f);
        myPlayer.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 300
            /*LocalPlayerInstance.JumpSpeed*/);
    }

    void PlayJumpSound()
    {
        AudioClip audioSource = Resources.Load<AudioClip>("Sounds/Jumping");
        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioSource);
    }
}
