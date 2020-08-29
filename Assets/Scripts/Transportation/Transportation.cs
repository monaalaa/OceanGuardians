using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The transportation structure is based on Factory Pattern,
/// it's main goal to give function the ingreadeant to easly and efficiantly create instance of it Dynamically, 
/// which inprove the open close principle by reduce the dependancies between classes
/// Refrance: https://www.tutorialspoint.com/design_pattern/factory_pattern.htm
/// </summary>

public enum TransportationFeaturesEnum
{
    MovingOnSurface,
    Fly,
    Fightable,
    Containable
}

[RequireComponent(typeof(Dragable))]
public class Transportation : MonoBehaviour
{
    #region Variables

    TransportationUI trnsportationUI;
    public TransportationData transportationData;
    // TransportationFeatures, Like it can Move, container,.... etc
    public List<TransportationFeatures> TransportationFeaturesList = new List<TransportationFeatures>();

    #endregion

    private void Start()
    {
        trnsportationUI = GetComponentInChildren<TransportationUI>();
    }

    private void OnMouseDown()
    {
        trnsportationUI.ToggleOptionalPanel(true);
    }

    public void PreformBehaviour(TransportationFeaturesEnum type)
    {
        for (int i = 0; i < transportationData.TransportationProperties.Count; i++)
        {
            //Get Object Feature to preform
            if (transportationData.TransportationProperties[i].type == type)
            {
                TransportationFeaturesList[i].PreformFeature();
                break;
            }
        }
    }
}

/// <summary>
/// To get Data FromDatabase
/// </summary>
[Serializable]
public class TransportationData
{
    public string Name;
    public int ValidLevel;
    public int Cost;
    public List<TransportationFeaturesData> TransportationProperties = new List<TransportationFeaturesData>();
    public GameObject TransportationPrefab;// mesh oe FBX as what we find that database can handle
    public Position TransportationPosition;
    TransportationData(string name, int validLevel, int cost, List<TransportationFeaturesData> transportationProperties, GameObject transportationPrefab, Position transportationPosition)
    {
        Name = name;
        ValidLevel = validLevel;
        Cost = cost;
        TransportationProperties = transportationProperties;
        TransportationPrefab = transportationPrefab;
        TransportationPosition = transportationPosition;
    }
}

