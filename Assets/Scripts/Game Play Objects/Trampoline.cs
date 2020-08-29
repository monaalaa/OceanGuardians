using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    float InitialForce = 400;

    [SerializeField]
    float MaxForce;

    WaitForEndOfFrame wait = new WaitForEndOfFrame();

    private void Start()
    {
        PlayerManager.Instance.OnHitGround += WaitToReset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Animator>().Play("Play");

            PlayerController.Instance.playerAnimator.SetFloat("Tramboline", PlayerController.Instance.myRigidbody.velocity.y);
            PlayerController.Instance.playerAnimator.SetBool("Tramboline Jump", true);
            PlayerController.Instance.playerAnimator.SetBool("Grounded", false);
            PlayerController.Instance.LocalPlayerInstance.GetComponent<Rigidbody>().AddForce(new Vector3(0, InitialForce, 0));
            Playsound();
             IncrementForce();
            StopAllCoroutines();
            StartCoroutine("ChangeAnimationBasedOnVelocity");
            //StartCoroutine("WaitToReset");
        }
    }

    void WaitToReset()
    {
        InitialForce = 500;
    }

    void IncrementForce()
    {
        if (InitialForce < 650)
        {
            InitialForce += 50;
        }
    }

    void Playsound()
    {
        AudioClip audioSource = Resources.Load<AudioClip>("Sounds/Boing");
        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioSource);
    }

    IEnumerator ChangeAnimationBasedOnVelocity()
    {
        PlayerController.Instance.playerAnimator.SetFloat("Tramboline", PlayerController.Instance.myRigidbody.velocity.y);
        yield return wait;
        if (PlayerController.Instance.LocalPlayerInstance.playerCharacter != CharacterStatus.Grounded)
        {
            GameManager.Instance.StartCoroutine(ChangeAnimationBasedOnVelocity());
        }
        else
        {
            PlayerController.Instance.playerAnimator.SetBool("Tramboline Jump", false);
            StopAllCoroutines();
        }
    }

}
