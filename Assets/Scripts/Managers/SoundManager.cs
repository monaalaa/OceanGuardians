using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    public AudioSource musicSource;
    public AudioSource BackGround2Source;
    public AudioSource efxSource;
    public AudioSource efxSource2;

    bool mute;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnToggleSound += OnMuteSound;
    }

    public void PlayClip(AudioSource source, AudioClip clip, float volume = 0f)
    {
        if (volume == 0)
            volume = source.volume;

        StopClip(source);
        source.clip = clip;
        source.Play();
        source.volume = volume;
    }

    public void StopClip(AudioSource source)
    {
        source.Stop();
    }

    void Mute()
    {
        efxSource.Pause();
        musicSource.Pause();
        mute = true;
    }

    void UnMute()
    {
        musicSource.Play();
        mute = false;
    }

    void OnMuteSound()
    {
        if (mute)
            UnMute();
        else
            Mute();
    }
}
