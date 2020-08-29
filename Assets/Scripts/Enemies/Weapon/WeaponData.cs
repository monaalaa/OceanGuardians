using UnityEngine;

public enum WeaponType
{
    None,
    Bomb,
    Litter,
    Ink,
    Bottle,
    Trap,
    Cannon,
    Knife
}

public enum WeaponEffectType
{
    None,
    Cut,
    Blind,
    Explode,
    Pollute,
    Trap
}

[System.Serializable]
public class WeaponData
{
    public float Damage;
    public WeaponType WeaponType;
    public float WeaponRange;
    public WeaponEffect WeaponEffect;
    public LayerMask CollisionLayer;
    public bool IsCollectable;
}
