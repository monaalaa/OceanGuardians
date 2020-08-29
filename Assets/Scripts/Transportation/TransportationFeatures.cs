using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransportationFeatures
{
    internal GameObject MyTransportation;

    internal string FeatureString;

    internal string FeatureShowName;

    internal TransportationFeaturesEnum transportationFeaturesEnum;
    public virtual void SetProperities(List<string> proerities)
    {

    }
    
    public virtual void PreformFeature()
    {
        
    }

    public void SetMyGameObjct(GameObject myTransportation)
    {
        this.MyTransportation = myTransportation;
    }
}
