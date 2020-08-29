using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public float SplashTime = 5;
    public static SceneController Instance;
    public GameObject Bg;
    public GameObject LoadingCanvas;
    public Image LoadingFillImage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        LoadScene("MainMenu", SplashTime);
    }

    public void LoadScene(string sceneName, float afterTime = 0)
    {
        ActivateLoadingMenu(true);
        if (afterTime > 0)
        {
            StartCoroutine(LoadSceneAfterTime(sceneName, afterTime));
        }
        else
        {
            StartCoroutine(LoadNewScene(sceneName));
        }
    }

    private IEnumerator LoadSceneAfterTime(string sceneName, float waitTime)
    {
        WaitForSeconds wait = new WaitForSeconds(waitTime);
        yield return wait;
        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            LoadingFillImage.fillAmount = ((async.progress / 0.9f));
            yield return null;
        }

        ActivateLoadingMenu(false);
    }

    private void ActivateLoadingMenu(bool show)
    {
        Bg.SetActive(show);
        LoadingCanvas.SetActive(show);
    }
}
