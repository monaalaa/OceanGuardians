using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    GameObject EndGamePanel;

    private void Start()
    {
        Playsound();
        GameManager.Instance.GameEnded += GameEnd;
    }

    void Playsound()
    {
        AudioClip audioSource = Resources.Load<AudioClip>("Sounds/GamePlay_Music");
        SoundManager.Instance.PlayClip(SoundManager.Instance.musicSource, audioSource);
        audioSource = Resources.Load<AudioClip>("Sounds/Ambient_Music");
        SoundManager.Instance.PlayClip(SoundManager.Instance.BackGround2Source, audioSource);
    }

    public void GameEnd()
    {
        UIManager.Instance.TogglePanel(EndGamePanel);      
        // Calculate Stars
        PlayerDataManager.Instance.CalculateStars();
    }

    public void TogglePause(GameObject pauseMenu)
    {
        UIManager.Instance.TogglePanel(pauseMenu);

        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
    }

    public void Reload()
    {
        UIManager.Instance.Reload("LandMode");
    }

    public void LoadMainMenu()
    {
        GameManager.Instance.UnsubscribeToEvents();
        SceneController.Instance.LoadScene("MainMenu");
    }
}
