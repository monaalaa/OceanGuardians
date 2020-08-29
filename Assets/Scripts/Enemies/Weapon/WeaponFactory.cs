using UnityEngine;

public class WeaponFactory
{
    public static Weapon CreateWeapon(WeaponType weaponType)
    {
        var weaponGO = Object.Instantiate((GameObject)Resources.Load("Weapons/" + weaponType.ToString()));
        Weapon weapon = weaponGO.GetComponent<Weapon>();
        if (weapon == null)
        {
            weapon = weaponGO.AddComponent<Weapon>();
        }
        weapon.WeaponData = DataManager.GetWeaponDataByType(weaponType.ToString());
        return weapon;
    }
}
