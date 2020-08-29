using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingOnSurface : TransportationFeatures {

    int speed;
    int velocity;
    int distance;

    bool canMove;

    Position targetPosition;
    Position initialPosition;

    public MovingOnSurface()
    {
        TransportationManager.CanPreform += ToggleCanMove;

        Dragable.PositionChanged += SetPosition;

        FeatureString = "Moving Speed: " + speed.ToString();
        transportationFeaturesEnum = TransportationFeaturesEnum.MovingOnSurface;
    }

    public override void SetProperities(List<string> proerities)
    {
        speed = int .Parse(proerities[0]);
        velocity = int.Parse(proerities[1]);
        distance = int.Parse(proerities[2]);
    }

    public override void PreformFeature()
    {
        if(canMove)
        {
            iTween.MoveTo(MyTransportation, new Vector3(targetPosition.X, targetPosition.Y, targetPosition.Z), 0.5f);
            //Note: need to handle when player come back from the journy and when he should come back
            //when back set canMove=false;
        }
    }

    void ToggleCanMove(GameObject go, TransportationFeaturesEnum featuresEnum)
    {
        if (go == MyTransportation && transportationFeaturesEnum == featuresEnum)
        {
            canMove = true;
            Debug.Log("Can Move Toggle");
        }
    }

    void SetPosition(Position position)
    {
        initialPosition = position;
    }
}
