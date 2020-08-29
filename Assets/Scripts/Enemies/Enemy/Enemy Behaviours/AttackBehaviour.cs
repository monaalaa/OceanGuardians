
using System.Collections;

public abstract class AttackBehaviour : CreatureBehaviour
{
    public abstract void Attack();
    public virtual void StopAttack() { }
}

