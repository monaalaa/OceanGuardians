using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class Contains The transportation Features Data
/// Feature Type and Paramerers that needed to preform this feature
/// </summary>

[System.Serializable]
public class TransportationFeaturesData
{
    public TransportationFeaturesEnum type;
    public List<string> PreformFunctionPrameters = new List<string>();
}
