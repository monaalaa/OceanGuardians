using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : WeaponEffect
{
    public override void GenerateEffect(GameObject objectCollidedWith)
    {
        Debug.Log("Blinding");
        throw new System.NotImplementedException();
    }
}
