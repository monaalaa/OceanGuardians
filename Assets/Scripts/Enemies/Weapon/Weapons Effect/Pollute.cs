using UnityEngine;

public class Pollute : WeaponEffect
{
    public Pollute(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public override void GenerateEffect(GameObject objectCollidedWith)
    {
        var other = objectCollidedWith;
        if (!other.tag.Equals("Basket"))
        {
            weapon.GetComponent<Rigidbody>().isKinematic = true;
            weapon.GetComponent<SphereCollider>().enabled = false;
            weapon.gameObject.transform.position = new Vector3(
                weapon.gameObject.transform.position.x,
                weapon.gameObject.transform.position.y,
                weapon.gameObject.transform.position.z - 0.09f);
        }
    }
}
