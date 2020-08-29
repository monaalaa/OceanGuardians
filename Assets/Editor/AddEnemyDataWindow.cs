using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

enum AttackBehaviour
{
    Biter,
    Thrower,
    TrapCreator
}

enum MovingBehaviour
{
    None,
    Chaser
}

public class AddEnemyDataWindow : EditorWindow
{
    EnemyType enemyType = EnemyType.Shark;
    float health;
    int validLevel;
    AttackBehaviour attackBehaviour;
    MovingBehaviour movingBehaviour;
    Transform throwTransform;
    Weapon throwWeapon;
    float throwingRepeatRate;


    [MenuItem("Window/Enemy")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AddEnemyDataWindow));
    }

    private void OnGUI()
    {
        GUILayout.Label("Enemy Data", EditorStyles.boldLabel);
        enemyType = (EnemyType)EditorGUILayout.EnumPopup("Enemy Type: ", enemyType);
        health = EditorGUILayout.FloatField("Health: ", health);
        validLevel = EditorGUILayout.IntField("Valid Level: ", validLevel);
        attackBehaviour = (AttackBehaviour)EditorGUILayout.EnumPopup("Attack Behaviour: ", attackBehaviour);
        if (attackBehaviour == AttackBehaviour.Thrower)
        {

        }
        movingBehaviour = (MovingBehaviour)EditorGUILayout.EnumPopup("Moving Behaviour: ", movingBehaviour);
        //GUILayout.Butto
    }

}
