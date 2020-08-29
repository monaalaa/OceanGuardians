using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : DeadZone
{
    [SerializeField]
    GameObject splash;

    protected override void PlayDeathAnimationbehaviour()
    {
        base.PlayDeathAnimationbehaviour();

        GameObject splashGO = GameObject.Instantiate(splash);

        splashGO.GetComponentsInChildren<RectTransform>()[1].localPosition = PlayerController.Instance.LocalPlayerInstance.transform.position;

        Playsound();

        GameObject shark = GameObject.Instantiate(Resources.Load("Enemies/Attack Shark") as GameObject);
        shark.transform.position = PlayerController.Instance.LocalPlayerInstance.transform.position;
        shark.GetComponentInChildren<Animator>().Play("Kill Player");
    }

    void Playsound()
    {
        AudioClip audioSource = Resources.Load<AudioClip>("Sounds/Water_Splash");
        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioSource);
    }
}
