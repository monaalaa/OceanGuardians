using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> enemiesInLevel;

    private void OnEnable()
    {
        //SceneManager.activeSceneChanged += OnLevelFinishedLoading;
        if (SceneManager.GetActiveScene().name.Equals("Level 1"))
        {
            enemiesInLevel = new List<GameObject>();
            enemiesInLevel.Add(EnemyFactory.CreateEnemy(EnemyType.Boss));
        }
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene prevScene, Scene newScene)
    {
        if (newScene.name.Equals("Level 1"))
        {
            enemiesInLevel = new List<GameObject>();
            enemiesInLevel.Add(EnemyFactory.CreateEnemy(EnemyType.Boss));
        }
    }
}
