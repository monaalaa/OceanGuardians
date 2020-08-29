using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Instance.LocalPlayerInstance.playerCharacter = CharacterStatus.Died;
            PlayDeathAnimationSound();
            PlayDeathAnimationbehaviour();
            //Fire Event Player died reset to CheckPoint 
            PlayerManager.Instance.InvokeOnPlayerDied();
        }
    }

    void PlayDeathAnimationSound()
    {
        //Play death Sound
        //AudioClip audioSource = Resources.Load<AudioClip>("Sounds/Evil_laugh");
        //SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource2, audioSource);
    }

    protected virtual void PlayDeathAnimationbehaviour()
    {
    }
}
