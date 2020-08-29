using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleEvents : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayClip(SoundManager.Instance.musicSource, Resources.Load<AudioClip>("Sounds/Island_Menu_Music"));
        SoundManager.Instance.PlayClip(SoundManager.Instance.BackGround2Source, Resources.Load<AudioClip>("Sounds/Ambient_Music"));
    }
    public void OnLoadScene(string sceneName)
    {
        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, Resources.Load<AudioClip>("Sounds/LevelSelectButton"));
        SceneController.Instance.LoadScene(sceneName);
    }
}
