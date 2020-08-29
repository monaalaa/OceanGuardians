using System;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    internal List<Building> PlayerBuildings;
    public static Dictionary<EnemyType, EnemyData> EnemyData { set; get; }
    public static Dictionary<WeaponType, WeaponData> WeaponData { set; get; }
    public static List<SkinData> SkinData { set; get; }
    public List<HomeItem> PlayerHomeItems = new List<HomeItem>();

    public Action<int, CollectableType> ScoreUpdated;

    private int goldScore;
    public int GoldScore
    {
        get
        {
            return goldScore;
        }

        set
        {
            goldScore += value;
            if (ScoreUpdated != null)
            {
                ScoreUpdated.Invoke(value, CollectableType.Gold);
            }
        }
    }
    private int fishScore;
    public int FishScore
    {
        get
        {
            return fishScore;
        }

        set
        {
            fishScore += value;
            if (ScoreUpdated != null)
            {
                ScoreUpdated.Invoke(value, CollectableType.Fish);
            }
        }
    }
    private int recycleScore = 20;
    public int RecycleScore
    {
        get
        {
            return recycleScore;
        }

        set
        {
            recycleScore += value;
            if (ScoreUpdated != null)
            {
                ScoreUpdated.Invoke(value, CollectableType.Recycle);
            }
        }
    }
    private int pearlScore;
    public int PearlScore
    {
        get
        {
            return pearlScore;
        }

        set
        {
            pearlScore += value;
            if (ScoreUpdated != null)
            {
                ScoreUpdated.Invoke(value, CollectableType.Pearl);
            }
        }
    }
    public JArray homeItems;

    public int RecycleBlockScore
    {
        get
        {
            return recycleBlock;
        }

        set
        {
            recycleBlock += value;
            if (ScoreUpdated != null)
            {
                ScoreUpdated.Invoke(value, CollectableType.RecycleBlock);
            }
        }
    }

    private int recycleBlock;

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
        GameManager.Instance.GetEnemyData += OnGetEnemyData;
        GameManager.Instance.GetWeaponData += OnGetWeaponData;
        GameManager.Instance.GetSkinData += OnGetSkinData;
        HomeDataController.GetPlayerHomeData();
    }

    public BuildingData GetBuildingBasedOnType(BuildingType type)
    {
        BuildingData c = new BuildingData();
        return c;
        //  return PlayerDataManager.Instance.CurrentPlayerData.PlayerBuildings.Find(x => x.BuildingType == type);
    }

    private void OnDestroy()
    {
        //SaveData
    }

    #region Weapon Data
    public static WeaponData GetWeaponDataByType(string weaponType)
    {
        return WeaponData[(WeaponType)Enum.Parse(typeof(WeaponType), weaponType)];
    }
    private void OnGetWeaponData(Dictionary<WeaponType, WeaponData> weaponData)
    {
        WeaponData = weaponData;
    }
    #endregion

    #region Enemy Data
    private void OnGetEnemyData(Dictionary<EnemyType, EnemyData> enemyData)
    {
        EnemyData = enemyData;
    }
    #endregion

    #region Skin Data
    private void OnGetSkinData(List<SkinData> skinData)
    {
        SkinData = skinData;
    }
    #endregion

    internal void AddToScore(int countCollected, CollectableType collectableType)
    {
        switch (collectableType)
        {
            case CollectableType.Gold:
                GoldScore = countCollected;
                break;
            case CollectableType.Fish:
                FishScore = countCollected;
                break;
            case CollectableType.RecycleBlock:
                RecycleBlockScore = countCollected;
                break;
            case CollectableType.Pearl:
                PearlScore = countCollected;
                break;
            case CollectableType.Recycle:
                RecycleScore = countCollected;
                break;
            default:
                break;
        }
    }

    private void OnApplicationQuit()
    {
        SaveHomeItem();
    }

    internal void SaveHomeItem()
    {
        HomeDataController.SavePlayerHomeData();
    }
}
