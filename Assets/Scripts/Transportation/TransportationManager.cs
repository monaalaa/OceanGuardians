using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportationManager : MonoBehaviour
{
    public static TransportationManager Instance;

    TransportationCreator transportationCreator = new TransportationCreator();

    public static Action<GameObject, TransportationFeaturesEnum> CanPreform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ProducePlayerTransportation();
    }

    public void InvokeCanPreform(GameObject gO, TransportationFeaturesEnum featuresEnum)
    {
        if (CanPreform != null)
            CanPreform.Invoke(gO,featuresEnum);
    }

    public void ProduceTransportation(TransportationData transportationData)
    {
        transportationCreator.TransportationInitializer(transportationData);
    }

    void ProducePlayerTransportation()
    {
        //should create for loop depends on list of transportation data of payer that come from database
    }
}
