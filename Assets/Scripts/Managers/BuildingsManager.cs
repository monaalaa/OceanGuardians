using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public static BuildingsManager Instance;

    public Action<int, Generators> FactoryFinishedWorking;

    private void Awake()
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
        FactoryFinishedWorking += CheckCapacity;
        //InstantiateAllPlayerBuildings
    }

    public void InstantiateBuilding(BuildingData currentBuildingData)
    {
        // To get building data from database
       // Building currentBuildingData = PlayerDataManager.Instance.PlayerBuildings.Find(x => x.BuildingType == type);

        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Type myTypeObj = Type.GetType(currentBuildingData.BuildingType.ToString());
        building.AddComponent(myTypeObj);
        //Just to test
        // Building building = (Building)GameObject.Instantiate(Resources.Load("Buildings/Building Prefab/" + type.ToString(), typeof(Building)));

        building.GetComponent<Building>().InitializeBuilding(currentBuildingData);
        building.GetComponent<Building>().InstantiateBuildingObjectInScene();
    }


    /// <summary>
    /// Instantiat the saved player building
    /// </summary>
    /// <param name="playerBuildings"></param>
    public void InstantiateAllPlayerBuildings(List<BuildingData> playerBuildings)
    {
        for (int i = 0; i < playerBuildings.Count; i++)
        {
            InstantiateBuilding(playerBuildings[i]);
        }
    }

    void CheckCapacity(int capacity, Generators generator)
    {
        string type = generator.ItemToGenerate.ItemType;
        //if (PlayerDataManager.Instance.CurrentPlayerData.ItemsCapacity.ContainsKey(type))
        //{
        //    if (PlayerDataManager.Instance.CurrentPlayerData.ItemsCapacity[type] + capacity <= PlayerDataManager.Instance.CurrentPlayerData.ItemsCapacity[type])
        //    {
        //        PlayerDataManager.Instance.CurrentPlayerData.ItemsCapacity[type] += capacity;
        //        generator.State = BuildingState.Hibernate;
        //        generator.RegenerateItems();
        //    }
        //    else
        //    {
        //        //Call UIManager to show msg that there is ni capacity
        //    }
        //}
    }
}
