using UnityEngine;

[DisallowMultipleComponent]
public class BounceComponent : Component
{
    [SerializeField]
    AddForceData componentData;

    public override void PreformComponent()
    {
        transform.parent.GetComponent<Animator>().Play("Play");
        Playsound();
        IncrementForce();
        //Add force to PreformObject
        ObjectToPreform.ExcuteAddForce(componentData.InitialForce, componentData.Direction);
        //after Time reset to intial force
    }

    void IncrementForce()
    {
        if (componentData.InitialForce < 650)
        {
            componentData.InitialForce += 50;
        }
    }

    void Playsound()
    {
       
    }
}

[System.Serializable]
public class AddForceData
{
    public float InitialForce;
    public float MaxForce;
    public Vector3 Direction;
}
