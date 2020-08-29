using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().name.Equals("Home"))
        {
            HomeManager.Instance.StoreClicked += OnShowStore;
        }
    }

    private void OnShowStore()
    {
        throw new NotImplementedException();
    }


}
