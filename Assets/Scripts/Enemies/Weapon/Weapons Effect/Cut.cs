using UnityEngine;
using System.Collections;

public class Cut : WeaponEffect
{
    public override void GenerateEffect(GameObject objectCollidedWith)
    {
        Debug.Log("cutting");
        throw new System.NotImplementedException();
    }
}
