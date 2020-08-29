using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    #region Vars
    public static UIManager Instance;
    public Canvas MenuCanvas;

    public Image BlurImage;

    #endregion

    #region Methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void TogglePanel(GameObject Panel)
    {
        Panel.SetActive(!Panel.activeSelf);
        ToggleOverlay(Panel.activeSelf);
    }

    public void ToggleSound()
    {
        GameManager.Instance.InvokeOnToggleSound();
    }

    public void Reload(string sceneName)
    {
        GameManager.Instance.UnsubscribeToEvents();
        SceneController.Instance.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    private void ToggleOverlay(bool blur)
    {
        BlurImage.enabled = blur;
    }

    #endregion
}
