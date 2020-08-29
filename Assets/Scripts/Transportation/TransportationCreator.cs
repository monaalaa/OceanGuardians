using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportationCreator 
{
    public Transportation TransportationInitializer(TransportationData transportationData)
    {
        //For now
        //In Future Code// OR just load Model if we able to save model
        // GameObject currentTransportation =GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
        //currentTransportation.GetComponent<MeshFilter>().mesh= transportationData.mesh;

        Transportation currentTransportation = (Transportation)GameObject.Instantiate(Resources.Load("Transportation", typeof(Transportation)));
        
        #region FillTransportationData
        currentTransportation.transportationData.Name = transportationData.Name;
        currentTransportation.transportationData.ValidLevel = transportationData.ValidLevel;
        currentTransportation.transportationData.TransportationProperties = transportationData.TransportationProperties;
        currentTransportation.transform.position = new Vector3(transportationData.TransportationPosition.X, transportationData.TransportationPosition.Y, transportationData.TransportationPosition.Z);
        #endregion

        currentTransportation.TransportationFeaturesList = DeclearTransportationFeatures(currentTransportation.transportationData.TransportationProperties,currentTransportation.gameObject);
       
        return currentTransportation;
    }

    private List<TransportationFeatures> DeclearTransportationFeatures(List<TransportationFeaturesData> transportationProperties, 
        GameObject cuurentTransportation)
    {
        List<TransportationFeatures> Transportation = new List<TransportationFeatures>();

        for (int i = 0; i < transportationProperties.Count; ++i)
        {
            //Reflection to get ObjectType an instantiate instance of it
            #region Reflecion
            Type t = Type.GetType(transportationProperties[i].type.ToString());
            TransportationFeatures tF = Activator.CreateInstance(t) as TransportationFeatures;
            #endregion

            tF.SetProperities(transportationProperties[i].PreformFunctionPrameters);

            tF.SetMyGameObjct(cuurentTransportation);
           
            //Fill Current transportation feature
            Transportation.Add(tF);
        }

        return Transportation;
    }
}
