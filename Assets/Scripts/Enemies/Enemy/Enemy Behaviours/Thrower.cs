using UnityEngine;
using System.Collections;

public class Thrower : AttackBehaviour
{
    private float throwingRepeatRate;
    private WeaponType weaponType;
    private Transform throwTransform;
    private Enemy enemyScript;
    private float hitRange;

    // TODO: delete
    private Weapon[] weapons;
    private Weapon currentWeapon;
    // TODO: delete
    private Transform target;

    public Thrower(float throwingRepeatRate, WeaponType weaponType)
    {
        this.throwingRepeatRate = throwingRepeatRate;
        this.weaponType = weaponType;
        OnSetEnemy += GetThrowPosition;
    }

    // TODO: delete
    public Thrower(float throwingRepeatRate, Weapon[] weapons, Transform throwTransform, float hitRange, Enemy enemyScript, bool targetPlayer)
    {
        this.throwingRepeatRate = throwingRepeatRate;
        this.weapons = weapons;
        this.throwTransform = throwTransform;
        this.hitRange = hitRange;
        this.enemyScript = enemyScript;
        if (targetPlayer)
        {
            this.target = PlayerController.Instance.LocalPlayerInstance.transform;
        }
        else
        {
            this.target = null;
        }
    }

    private void GetThrowPosition()
    {
        var enemyTransform = Enemy.transform;
        enemyScript = Enemy.GetComponent<Enemy>();
        for (int i = 0; i < enemyTransform.childCount; i++)
        {
            if (enemyTransform.GetChild(i).tag.Equals("ThrowPosition"))
            {
                throwTransform = enemyTransform.GetChild(i);
                return;
            }
        }
        HasError = true;
        Debug.LogError("Throw position in the thrower enemy is not specified..");
    }

    public override void Attack()
    {
        GenerateWeapon();
    }

    private void GenerateWeapon()
    {
        // TODO: uncomment line 66 and delete line 67
        // var weapon = WeaponFactory.CreateWeapon(weaponType);
        if (PlayerController.Instance.LocalPlayerInstance.playerCharacter == CharacterStatus.Grounded)
        {
            int randIndex = Random.Range(0, weapons.Length - 1);
            currentWeapon = weapons[randIndex];
            var weaponGo = GameObject.Instantiate(currentWeapon.gameObject);
            weaponGo.transform.position = throwTransform.position;
            enemyScript.StartCoroutine(this.Throw(weaponGo));
        }
    }

    private IEnumerator Throw(GameObject weaponGo)
    {
        GameObject indicator;
        var target = GetRandomPointsAroundTarget();
        var body = weaponGo.GetComponent<Rigidbody>();
        var weaponScript = weaponGo.GetComponent<Weapon>();
        indicator = CreateIndicator();
        indicator.transform.position = GetPositionFromRaycast(body, target);
        indicator.transform.position = new Vector3(indicator.transform.position.x, indicator.transform.position.y + 0.5f, indicator.transform.position.z);
        while (!weaponScript.Collided)
        {
            body.velocity = (target - body.position).normalized * body.mass;
            body.position = new Vector3(body.position.x, body.position.y, target.z);
            yield return new WaitForFixedUpdate();
        }
        GameObject.Destroy(indicator);
    }

    private GameObject CreateIndicator()
    {
        GameObject indicator;
        if (currentWeapon.WeaponData.IsCollectable)
        {
            //instantiate decal
            indicator = GameObject.Instantiate(Resources.Load<GameObject>("Gameplay Effect/Green_Target_Zone"));
        }
        else
        {
            //instantiate decal
            indicator = GameObject.Instantiate(Resources.Load<GameObject>("Gameplay Effect/Red_Target_Zone"));
        }

        return indicator;
    }

    private Vector3 GetPositionFromRaycast(Rigidbody body, Vector3 target)
    {
        Ray ray = new Ray(body.position, target - body.position);
        RaycastHit hit;
        float maxDistance = 40;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if ((hit.collider != null))
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }

    private Vector3 GetRandomPointsAroundTarget()
    {
        if (target != null)
        {
            // throw on target
            return new Vector3(target.position.x + Random.Range(-hitRange, hitRange), 0, target.position.z);
        }
        else
        {
            // throw randomly
            return new Vector3(enemyScript.gameObject.transform.position.x + Random.Range(-hitRange, hitRange), 0,
                PlayerController.Instance.LocalPlayerInstance.transform.position.z);
        }
    }
}
