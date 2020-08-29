using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEffect
{
    public Weapon weapon;
    public abstract void GenerateEffect(GameObject objectCollidedWith);
}
