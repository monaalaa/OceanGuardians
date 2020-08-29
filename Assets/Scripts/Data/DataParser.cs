using System;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class DataParser
{
    static List<SkinData> ParsedSkinDataList = new List<SkinData>();

    public static void ParseEnemyData(string rawEnemyData)
    {
        var enemyArray = JArray.Parse(rawEnemyData);
        Dictionary<EnemyType, EnemyData> enemyList = new Dictionary<EnemyType, EnemyData>();
        for (int i = 0; i < enemyArray.Count; i++)
        {
            EnemyData data = new EnemyData();
            data.EnemyType = (EnemyType)Enum.Parse(typeof(EnemyType), enemyArray[i]["Enemy Type"].ToString());
            data.Health = float.Parse(enemyArray[i]["Health"].ToString());
            data.ValidLevel = int.Parse(enemyArray[i]["Valid Level"].ToString());
            if (enemyArray[i]["Attack Behaviour"] != null)
            {
                var attackType = enemyArray[i]["Attack Behaviour"]["Type"].ToString();
                data.attackBehaviour = GetAttackBehaviourObject(attackType, enemyArray[i]["Attack Behaviour"]);
            }
            if (enemyArray[i]["Move Behaviour"] != null)
            {
                var moveType = enemyArray[i]["Move Behaviour"]["Type"].ToString();
                data.moveBehaviour = GetMoveBehaviourObject(moveType, enemyArray[i]["Move Behaviour"]);
            }
            enemyList.Add(data.EnemyType, data);
        }
        GameManager.Instance.OnGetEnemyData(enemyList);
    }

    private static MoveBehaviour GetMoveBehaviourObject(string moveType, JToken moveBehaviour)
    {
        EnemyMoveBehaviourType moveEnumType;
        try
        {
            moveEnumType = (EnemyMoveBehaviourType)Enum.Parse(typeof(EnemyMoveBehaviourType), moveType);
        }
        catch (Exception)
        {
            moveEnumType = EnemyMoveBehaviourType.None;
            Debug.Log("Can't parse the enemy move data.");
            return null;
        }
        switch (moveEnumType)
        {
            case EnemyMoveBehaviourType.Chaser:
                return new Chaser(float.Parse(moveBehaviour["Speed On Chasing"].ToString()));
            default:
                break;
        }
        return null;
    }

    private static AttackBehaviour GetAttackBehaviourObject(string attackType, JToken attackBehaviour)
    {
        EnemyAttackBehaviourType attackEnumType;
        try
        {
            attackEnumType = (EnemyAttackBehaviourType)Enum.Parse(typeof(EnemyAttackBehaviourType), attackType);
        }
        catch (Exception e)
        {
            attackEnumType = EnemyAttackBehaviourType.None;
            Debug.LogError("Can't parse the enemy attack type.");
            return null;
        }
        switch (attackEnumType)
        {
            case EnemyAttackBehaviourType.Thrower:
                var weaponType = (WeaponType)(Enum.Parse(typeof(WeaponType), attackBehaviour["Weapon"].ToString()));
                return new Thrower(float.Parse(attackBehaviour["Throwing Repeat Rate"].ToString()), weaponType);
            case EnemyAttackBehaviourType.Biter:
                break;
            case EnemyAttackBehaviourType.TrapCreator:
                break;
            default:
                break;
        }
        return null;

    }

    public static void ParseWeaponData(string rawWeaponData)
    {
        Dictionary<WeaponType, WeaponData> weaponList = new Dictionary<WeaponType, WeaponData>();
        var weaponArray = JArray.Parse(rawWeaponData);
        for (int i = 0; i < weaponArray.Count; i++)
        {
            WeaponType weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), weaponArray[i]["Weapon Type"].ToString());
            WeaponData weapon = new WeaponData();
            weapon.WeaponType = weaponType;
            weapon.Damage = float.Parse(weaponArray[i]["Damage"].ToString());
            weapon.WeaponEffect = GetWeaponEffectObject(weaponArray[i]["Weapon Effect"]["Type"].ToString());
            weaponList.Add(weapon.WeaponType, weapon);
        }
        GameManager.Instance.OnGetWeaponData(weaponList);
    }

    private static WeaponEffect GetWeaponEffectObject(string effectType)
    {
        WeaponEffectType effectTypeEnum;
        try
        {
            effectTypeEnum = (WeaponEffectType)(Enum.Parse(typeof(WeaponEffectType), effectType));

        }
        catch (Exception)
        {
            effectTypeEnum = WeaponEffectType.None;
            Debug.Log("Can't parse the weapon effect.");
            return null;
        }
        switch (effectTypeEnum)
        {
            case WeaponEffectType.Cut:
                return new Cut();
            case WeaponEffectType.Blind:
                break;
            case WeaponEffectType.Explode:
                break;
            case WeaponEffectType.Pollute:
               //return new Pollute();
            case WeaponEffectType.Trap:
                break;
            default:
                break;
        }
        return null;
    }

    public static void ParseSkinData(string rawSkinData)
    {
        var skinArray = JObject.Parse(rawSkinData);
        foreach (var skin in skinArray)
        {
            SkinData skinData = new SkinData();
            // skin data complete is fired when all the skin data is completed WITHOUT errors. 
            skinData.SkinDataComplete += AddSkinDataToParsedList;
            skinData.Name = skin.Value["Name"].ToString();
            skinData.Cost = int.Parse(skin.Value["Cost"].ToString());
            skinData.TextureURI = skin.Value["TextureURI"].ToString();
            FirebaseManager.Instance.GetTextureLocalPathByName(skinData.Name, result => skinData.TextureLocalPath = result);
            FirebaseManager.Instance.GetTextureByUri(skinData.TextureURI, result => skinData.Texture = result);
        }
        Task.Delay(10000).ContinueWith(_ =>
        {
            GameManager.Instance.OnGetSkinData(ParsedSkinDataList);
        });
    }

    private static void AddSkinDataToParsedList(SkinData skinData)
    {
        ParsedSkinDataList.Add(skinData);
    }
}



