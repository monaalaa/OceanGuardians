using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BuildingState
{
    Productive,
    Hibernate,
    Filled
}
public enum BuildingType
{
    Generator,
    Container
}

[RequireComponent(typeof(Dragable))]
public class Building : MonoBehaviour
{
    [SerializeField]
    GameObject optionsPanel;

    [SerializeField]
    GameObject infoPannel;

    internal BuildingState State = BuildingState.Hibernate;
    internal BuildingType BuildingType;

    internal GeneratedItem ItemToGenerate;

    internal int Cost;
    internal int UnlockingLevel;
    internal int MaxCapacity;
    internal int CurrentLevel;
    internal int NumberOFSecondTakesToBuild;
    internal string BuildingBuildMesh;
    internal string BuildingMeshAfterBuild;
    Position BuildingPosition;
    //Material buildingMaterial;

    int collected;
    internal int Collected
    {
        get
        {
            return collected;
        }

        set
        {
            collected = value;
        }
    }

    public void InitializeBuilding(BuildingData building)
    {
        BuildingType = building.BuildingType;
        //ItemToGenerate = //Search in item List by type//building.ItemToGenerate;
        Cost = building.Cost;
        UnlockingLevel = building.UnlockingLevel;
        MaxCapacity = building.MaxCapacity;
        CurrentLevel = building.CurrentLevel;
        Collected = building.Collected;
        BuildingPosition = building.BuildingPosition;
        BuildingBuildMesh = building.BuildingBuildMesh;
        BuildingMeshAfterBuild = building.BuildingMeshAfterBuild;
    }

    public void UpGradeBuilding()
    {
        if (CurrentLevel + 1 < UnlockingLevel)
        {
            CurrentLevel += 1;
            //Get building upgraded data from data manager
            //Destroy this
            //Building manager. Instantiate
        }
    }

    /// <summary>
    /// Display building after instantiate it
    /// </summary>
    public void InstantiateBuildingObjectInScene()
    {
        //May be we save them as path or as object if path get from resources
        //GetComponent<MeshFilter>().mesh = BuildingBuildMesh;//Resources.Load("Buildings/BuildingMeshes/" + BuildingBuildMesh) as Mesh;
        transform.position = new Vector3(BuildingPosition.X, BuildingPosition.Y, BuildingPosition.Z);
        StartCoroutine(ChangeMeshAfterBuild());
    }

    /// <summary>
    /// wait for second to show building build effect befor it become fully builded
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeMeshAfterBuild()
    {
        //Note: Here we can play animation or something
        yield return new WaitForSeconds(0.4f);
        //GetComponent<MeshFilter>().mesh = BuildingMeshAfterBuild;//Resources.Load("Buildings/BuildingMeshes/" + BuildingMeshAfterBuild) as Mesh;
    }

    public void OnMouseDown()
    {
        optionsPanel.SetActive(true);
    }

    public void ShowInfo()
    {
        FillInfoPannel();
        infoPannel.SetActive(true);
    }

    /// <summary>
    /// Fill Building Data in info panel
    /// </summary>
    public virtual void FillInfoPannel() { }
}
[System.Serializable]
/// <summary>
/// This only to save building position in firebase as it wont able to save a vector3 var
/// </summary>


/// <summary>
/// For Connection With Database
/// </summary>
public class BuildingData
{
    public BuildingState State = BuildingState.Hibernate;
    public BuildingType BuildingType;

    public string ItemToGenerate;

    public int Cost;
    public int UnlockingLevel;
    public int MaxCapacity;
    public int CurrentLevel;
    public int NumberOFSecondTakesToBuild;
    public int Collected;
    public string BuildingBuildMesh;
    public string BuildingMeshAfterBuild;
    public Position BuildingPosition;

    //public BuildingData(BuildingState state, BuildingType buildingType, string iItemToGenerate, int cost, int unlockingLevel, int maxCapacity, int currentLevel, int numberOFSecondTakesToBuild,
    //   Mesh buildingBuildMesh, Mesh buildingMeshAfterBuild, Position buildingPosition, int collected)
    //{
    //    State = state;
    //    BuildingType = buildingType;
    //    ItemToGenerate = iItemToGenerate;
    //    Cost = cost;
    //    UnlockingLevel = unlockingLevel;
    //    MaxCapacity = maxCapacity;
    //    CurrentLevel = currentLevel;
    //    NumberOFSecondTakesToBuild = numberOFSecondTakesToBuild;
    //    BuildingBuildMesh = buildingBuildMesh;
    //    BuildingMeshAfterBuild = buildingMeshAfterBuild;
    //    BuildingPosition = buildingPosition;
    //    Collected = collected;
    //}
}
