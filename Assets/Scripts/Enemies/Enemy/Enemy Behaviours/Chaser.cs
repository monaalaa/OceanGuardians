using UnityEngine;
using System.Collections;
using System;

public class Chaser : MoveBehaviour
{
    private float speedOnChasing;

    public Chaser(float speedOnChasing)
    {
        this.speedOnChasing = speedOnChasing;
    }

    public override void Move()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        var desVelocity = (PlayerController.Instance.LocalPlayerInstance.transform.position - Enemy.transform.position).normalized;
        var force = (desVelocity - EnemyBody.velocity.normalized) * speedOnChasing;
        Enemy.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Enemy.transform.forward, desVelocity, 0.009f, 0f));
        EnemyBody.velocity += force;
    }

    internal void StopChasingPlayer()
    {
        EnemyBody.velocity = Vector3.zero;
    }
}
