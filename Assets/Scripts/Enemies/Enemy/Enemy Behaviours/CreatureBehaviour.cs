
using System;
using UnityEngine;

public abstract class CreatureBehaviour
{
    public Action OnSetEnemy;
    public bool HasError;
    protected Rigidbody EnemyBody;
    private GameObject enemy;
    public GameObject Enemy
    {
        set
        {
            enemy = value;
            EnemyBody = enemy.GetComponent<Rigidbody>();
            if (OnSetEnemy != null)
            {
                OnSetEnemy.Invoke();
            }
        }
        get
        {
            return enemy;
        }
    }
}
