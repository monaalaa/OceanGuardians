using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    PlayerController.Instance.myRigidbody.isKinematic = true;
        //    PlayerController.Instance.myRigidbody.useGravity = false;
        //    PlayerController.Instance.playerAnimator.SetBool("Clamp", true);
        //    PlayerController.Instance.LocalPlayerInstance.playerCharacter = CharacterStatus.ClampStairs;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Instance.LocalPlayerInstance.playerCharacter = CharacterStatus.Climbing;

            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().useGravity = true;

            PlayerController.Instance.playerAnimator.SetBool("Clamp", false);
        }
    }
}
