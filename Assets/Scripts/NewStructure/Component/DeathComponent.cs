using UnityEngine;

[DisallowMultipleComponent]
public class DeathComponent : Component
{
    [SerializeField]
    DeathData componentData;

    public override void PreformComponent()
    {
        if (DestroyObject)
            ObjectToPreform.gameObject.SetActive(false);
        ObjectToPreform.ExcuteDeath(gameObject.name);
        //May Play Sound For death if there
        Playsound();
        // Instantiate effect
        ShowEffects();
    }

    void ShowEffects()
    {
        GameObject tempEffect;
        tempEffect = Instantiate(componentData.Effect);
        tempEffect.transform.localPosition = ObjectToPreform.transform.position;
    }

    void Playsound()
    {
        AudioClip audioSource = Resources.Load<AudioClip>("Sounds/"+ componentData.Sound);
        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioSource);
    }
}
