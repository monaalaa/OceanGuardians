using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containable : TransportationFeatures {

    int currentCapacity;
    internal int maxCapacity;

    bool Contain;

    GeneratedItem item;

    public Containable()
    {
        FeatureString = "Capacity: " + maxCapacity.ToString();
        transportationFeaturesEnum = TransportationFeaturesEnum.Containable;
        TransportationManager.CanPreform += CanContain;
    }

    public override void SetProperities(List<string> properities)
    {
        currentCapacity = int.Parse(properities[0]);
        maxCapacity = int.Parse(properities[1]);
    }

    public override void PreformFeature()
    {
        //check For Capacity
    }

    public void CanContain(GameObject myTrnsform, TransportationFeaturesEnum featuresEnum)
    {
        if (MyTransportation == myTrnsform && transportationFeaturesEnum == featuresEnum)
        {
            TransportationManager.Instance.InvokeCanPreform(MyTransportation, TransportationFeaturesEnum.MovingOnSurface);
        }
    }
}
