using System.Collections;
using UnityEngine;

public class Explode : WeaponEffect
{
    private float timeToApplyEffect;
    private float destroyTime = 1f;
    private bool effectGenerated;

    public Explode(Weapon weapon, float timeToApplyEffect)
    {
        this.weapon = weapon;
        this.timeToApplyEffect = timeToApplyEffect;
    }

    public override void GenerateEffect(GameObject objectCollidedWith)
    {
        if (!effectGenerated)
        {
            weapon.StartCoroutine(this.ExplodeEffect());
            effectGenerated = true;
        }
    }

    private IEnumerator ExplodeEffect()
    {
        yield return new WaitForSeconds(timeToApplyEffect);
        // generate smoke
        GameObject effect = GameObject.Instantiate(Resources.Load<GameObject>("Weapons/Effect/Smoke"), weapon.transform.position, Quaternion.identity);
        weapon.GetComponent<MeshRenderer>().enabled = false;
        effect.transform.parent = weapon.transform;
        // do explosion effect on nearby bodies;
        FlyNearbyBodies();
        // destroy weapon after sometime
        weapon.StartCoroutine(this.DestroyEffect(effect));
    }

    private void FlyNearbyBodies()
    {
        var explosionPos = weapon.transform.position;
        Collider[] bodiesColliders = Physics.OverlapSphere(explosionPos, weapon.WeaponData.WeaponRange);
        foreach (Collider hit in bodiesColliders)
        {
            Rigidbody rb;
            bool isPlayer = false;
            if (hit.tag.Equals("Player"))
            {
                rb = hit.transform.parent.parent.parent.GetComponent<Rigidbody>();
                isPlayer = true;
            }
            else
            {
                rb = hit.GetComponent<Rigidbody>();
            }
            if (rb != null && !hit.tag.Equals("Sea"))
            {
                rb.AddExplosionForce(weapon.WeaponData.Damage, explosionPos, weapon.WeaponData.WeaponRange, 3);
                GameManager.Instance.OnExplosionOccur(isPlayer);
                var audioClip = Resources.Load<AudioClip>("Audio/BombExplosion");
                // play sound
                SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioClip, 1);
            }
        }
    }

    private IEnumerator DestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(destroyTime);
        GameObject.Destroy(weapon.gameObject);
    }

}
