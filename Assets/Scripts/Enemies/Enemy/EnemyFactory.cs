using UnityEngine;

public class EnemyFactory
{
    public static GameObject CreateEnemy(EnemyType enemyType)
    {
        var enemyGO = Object.Instantiate(((GameObject)Resources.Load("Enemies/" + enemyType.ToString())));
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        if (enemy == null)
        {
            enemy = enemyGO.AddComponent<Enemy>();
        }
        // get enemy data by the type of enemy specified from the enemy database. 
        enemy.EnemyData = DataManager.EnemyData[enemyType];
        if (enemy.EnemyData.attackBehaviour != null)
        {
            enemy.EnemyData.attackBehaviour.Enemy = enemy.gameObject;
        }
        if (enemy.EnemyData.moveBehaviour != null)
        {
            enemy.EnemyData.moveBehaviour.Enemy = enemy.gameObject;
        }
        return enemyGO;
    }
}


