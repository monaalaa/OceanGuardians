public enum EnemyType
{
    None,
    Shark,
    Octopus,
    BombThrower,
    Drunk,
    Crow,
    PirateCannon,
    Boss
}

public enum EnemyAttackBehaviourType
{
    None,
    Thrower,
    Biter,
    TrapCreator
}

public enum EnemyMoveBehaviourType
{
    None,
    Chaser
}

[System.Serializable]
public class EnemyData
{
    public EnemyType EnemyType;
    public float Health;
    public int ValidLevel;
    public float SightRange;
    public AttackBehaviour attackBehaviour;
    public MoveBehaviour moveBehaviour;
}
