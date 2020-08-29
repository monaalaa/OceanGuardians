using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    [SerializeField]
    CharacterJoint End;
    [SerializeField]
    Rigidbody Rig;
    [SerializeField]
    Rigidbody[] test;
    private void Start()
    {
        PlayerManager.Instance.OnDeattachPlayerFromRope += DeattachFromRope;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            AttachToRope();
        }
    }

    void AttachToRope()
    {
        if (PlayerController.Instance.LocalPlayerInstance.playerCharacter != CharacterStatus.Hang)
        {
            Rig.drag = 0;
            foreach (Rigidbody item in test)
            {
                item.drag = 0;
            }
            Transform player = PlayerController.Instance.LocalPlayerInstance.transform;
            player.SetParent(transform);
            player.localPosition = new Vector3(-0.18f * PlayerController.Instance.LocalPlayerInstance.Direction, 0.8f, 0);
            PlayerController.Instance.LocalPlayerInstance.playerCharacter = CharacterStatus.Hang;
            PlayerController.Instance.playerAnimator.SetTrigger("Hang");
            PlayerController.Instance.playerAnimator.SetBool("jump", false);
            End.connectedBody = PlayerController.Instance.myRigidbody;
            PlayerController.Instance.LocalPlayerInstance.playerCollider.enabled = false;
            PlayerController.Instance.myRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        }
    }

    void DeattachFromRope()
    {
        End.connectedBody = null;
        PlayerController.Instance.LocalPlayerInstance.transform.SetParent(null);
        PlayerController.Instance.LocalPlayerInstance.playerCollider.enabled = true;
        End.connectedBody = End.transform.parent.GetComponent<Rigidbody>();
        PlayerController.Instance.myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        PlayerController.Instance.myRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
        PlayerController.Instance.myRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForEndOfFrame();
        Rig.drag = 100000;
        foreach (Rigidbody item in test)
        {
            item.drag = 3;
        }
        yield return new WaitForEndOfFrame();
        GetComponent<BoxCollider>().enabled = true;
    }
}
