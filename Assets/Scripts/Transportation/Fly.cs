using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : TransportationFeatures {

    int speed;
    int distanceUp;

    bool canFly;

    public Fly()
    {
        TransportationManager.CanPreform += ToggleCanFly;
        FeatureString = "Fling Speed: " + speed.ToString();
        transportationFeaturesEnum = TransportationFeaturesEnum.Fly;
    }

    public override void SetProperities(List<string> proerities)
    {
        speed = int.Parse(proerities[0]);
        distanceUp = int.Parse(proerities[1]);
    }

    public override void PreformFeature()
    {
        if(canFly)
        Debug.Log("I Can fly");
        //Move
    }

    void ToggleCanFly(GameObject go,TransportationFeaturesEnum featuresEnum)
    {
        if (go == MyTransportation && transportationFeaturesEnum == featuresEnum)
        {
            canFly = true;
            Debug.Log("hi i can fly");
        }
    }
}
