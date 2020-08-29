using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    SeaMode,
    LandMode
}

public class GameManager : MonoBehaviour
{

    #region Vars
    public static GameManager Instance;

    public Action GameStarted;
    public Action GamePaused;
    public Action GameEnded;

    public Action SwipedLeft;
    public Action SwipedRight;

    public Action Tapped;

    public Action OnToggleSound;

    /// <summary>
    /// First vector for direction, second for Value
    /// </summary>
    public Action<Vector2, Vector2> OnInputPressed;
    public Action<bool> ExplosionOccured;
    public Action CollectLitter;
    public Action<Dictionary<EnemyType, EnemyData>> GetEnemyData;
    public Action<Dictionary<WeaponType, WeaponData>> GetWeaponData;
    public Action<List<SkinData>> GetSkinData;
    public Action CollectGeneratedItems;
    public GameMode CurrentIslandMode;
    #endregion

    #region Methods

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GetSkinData += DataLocalSaver.SaveSkinDataLocally;
    }

    internal void InvokeOnInputPressed(Vector2 direction, Vector2 Value)
    {
        if (OnInputPressed != null)
            OnInputPressed.Invoke(direction, Value);
    }

    public void OnGetEnemyData(Dictionary<EnemyType, EnemyData> enemyData)
    {
        if (GetEnemyData != null)
        {
            GetEnemyData.Invoke(enemyData);
        }
    }

    public void OnGetSkinData(List<SkinData> skinData)
    {
        if (GetSkinData != null)
        {
            GetSkinData.Invoke(skinData);
        }
    }

    public void OnGetWeaponData(Dictionary<WeaponType, WeaponData> weaponData)
    {
        if (GetWeaponData != null)
        {
            GetWeaponData.Invoke(weaponData);
        }
    }

    public void InvokeEndGame()
    {
        if (GameManager.Instance.GameEnded != null)
            GameManager.Instance.GameEnded.Invoke();
    }
    public void UnsubscribeToEvents()
    {
        OnInputPressed = null;
        GameEnded = null;
    }

    public void InvokeOnToggleSound()
    {
        if (OnToggleSound != null)
            OnToggleSound.Invoke();
    }

    internal void InvokeCollectGeneratedItems()
    {
        if (CollectGeneratedItems != null)
            CollectGeneratedItems.Invoke();
    }

    internal void StartCoroutineInAbstract(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    internal void StopCoroutineInAbstract(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }
    
    public void OnExplosionOccur(bool isPlayerAffected)
    {
        if (ExplosionOccured != null)
        {
            ExplosionOccured.Invoke(isPlayerAffected);
        }
    }
    #endregion
}
