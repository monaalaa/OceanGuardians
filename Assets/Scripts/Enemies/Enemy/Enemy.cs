using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData;
    // TODO: to be deleted.
    public Transform LocationOfThrow;
    // TODO: to be deleted.
    public Weapon[] Weapons;
    //TODO: to be deleted.
    public Transform Target;
    // TODO: delete 
    public float HitRange;
    private Animator animator;

    private void Start()
    {
        // TODO: to be deleted and uncomment line 21
        GetAttackBehaviour();
        animator = GetComponent<Animator>();
        //if (EnemyData.attackBehaviour != null)
        //{
        //    EnemyData.attackBehaviour.Attack();
        //}
    }

    // TODO: to be deleted.
    private void GetAttackBehaviour()
    {
        switch (EnemyData.EnemyType)
        {
            case EnemyType.None:
                break;
            case EnemyType.Shark:
                break;
            case EnemyType.Octopus:
                break;
            case EnemyType.BombThrower:
                EnemyData.attackBehaviour = new Thrower(3.8f, Weapons, LocationOfThrow, HitRange, this, true);
                break;
            case EnemyType.Drunk:
                break;
            case EnemyType.Crow:
                break;
            case EnemyType.PirateCannon:
                break;
            case EnemyType.Boss:
                EnemyData.attackBehaviour = new Thrower(0.5f, Weapons, LocationOfThrow, HitRange, this, false);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (EnemyData.moveBehaviour != null)
        {
            EnemyData.moveBehaviour.Move();
        }
    }

    private void Attack()
    {
        if (EnemyData.attackBehaviour != null && IsTargetWithinRange())
        {
            EnemyData.attackBehaviour.Attack();
        }
    }

    private bool IsTargetWithinRange()
    {
        if (EnemyData.SightRange > 0)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, EnemyData.SightRange);
            foreach (Collider hit in hits)
            {
                if (hit.tag.Equals(Target.tag))
                {
                    return true;
                }
            }
            return false;
        }
        return true;
    }

    // called within animation timeline
    private void ChangeAnimation()
    {
        // get 80% chance of changing animation
        float rand = Random.Range(0f, 10f);

        if (IsTargetWithinRange() && rand <= 8)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    private void OnDrawGizmos()
    {
        if (Target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Target.transform.position, HitRange);
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, EnemyData.SightRange);
    }
}
