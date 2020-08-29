using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fightable : TransportationFeatures {

    float power;

    public Fightable()
    {
        FeatureString = "Fighting Power: " + power.ToString();
        transportationFeaturesEnum = TransportationFeaturesEnum.Fightable;
    }

    public override void SetProperities(List<string> proerities)
    {
        base.SetProperities(proerities);
    }

    public override void PreformFeature()
    {
        base.PreformFeature();
    }
}
