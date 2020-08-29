using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData WeaponData;
    public bool Collided;
    private Rigidbody body;
    
    // TODO: delete
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        switch (WeaponData.WeaponType)
        {
            case WeaponType.None:
                break;
            case WeaponType.Bomb:
                WeaponData.WeaponEffect = new Explode(this, 2);
                break;
            case WeaponType.Litter:
                WeaponData.WeaponEffect = new Pollute(this);
                break;
            case WeaponType.Ink:
                break;
            case WeaponType.Bottle:
                break;
            case WeaponType.Trap:
                break;
            case WeaponType.Cannon:
                break;
            case WeaponType.Knife:
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (((1 << other.gameObject.layer) & WeaponData.CollisionLayer) != 0)
        {
            Collided = true;
            WeaponData.WeaponEffect.GenerateEffect(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & WeaponData.CollisionLayer) != 0)
        {
            Collided = true;
            WeaponData.WeaponEffect.GenerateEffect(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, WeaponData.WeaponRange);
    }
}
