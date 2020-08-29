using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsEnd : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Instance.LocalPlayerInstance.playerCharacter = CharacterStatus.Grounded;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}

