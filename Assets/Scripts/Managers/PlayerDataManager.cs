using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// its the manager that contais the Current player data
/// </summary>
/// 
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    PlayerData currentPlayerData;

    public PlayerData CurrentPlayerData
    {
        set
        {
            currentPlayerData = value;
            currentPlayerData.Weapon = GetPlayerWeaponData();
            PlayerManager.Instance.InstantiatePlayer(currentPlayerData);
        }
        get
        {
            return currentPlayerData;
        }
    }

    private UIEffects UIEffects;

    private int coins;
    private int litter;

    // Use this for initialization
    void Awake()
    {
        UIEffects = FindObjectOfType<UIEffects>();

        if (Instance == null)
        {
            Instance = this;
        }
    }

    Weapon GetPlayerWeaponData()
    {
        return new Weapon();
        //Datamanager.Instance.GetWeaponDataByType(currentPlayerData.WeaponName);
    }

    public void SavePlayerData()
    {
        //FirebaseManager.Instance.SetData("PlayerData", currentPlayerData, currentPlayerData.PlayerName); 
    }

    public void AddCoin()
    {
        coins++;
        UIEffects.CoinCollected(coins);
    }

    public void AddLitter()
    {
        litter++;
        UIEffects.LitterCollected(litter);
    }

    public void UncollectAllLitter()
    {
        litter = 0;
        UIEffects.LitterCollected(litter);
    }

    public void CalculateStars()
    {
        float scorePerc = (coins / 25f) * 100;

        scorePerc += (litter * 3);

        if (scorePerc >= 90)
            UIEffects.ShowStars(3);
        else if (scorePerc >= 60)
            UIEffects.ShowStars(2);
        else if (scorePerc >= 30)
            UIEffects.ShowStars(1);
        else
            UIEffects.ShowStars(0);

        SaveCollectedData();
    }

    void SaveCollectedData()
    {
        DataManager.Instance.AddToScore(coins, CollectableType.Gold);
        DataManager.Instance.AddToScore(litter, CollectableType.Recycle);
    }
}
