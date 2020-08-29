using UnityEngine;

public class DataMapper : MonoBehaviour
{

    private string RawEnemyData
    {
        set
        {
            DataParser.ParseEnemyData(value);
        }
    }
    private string RawWeaponData
    {
        set
        {
            DataParser.ParseWeaponData(value);
        }
    }
    private string RawSkinData
    {
        set
        {
            DataParser.ParseSkinData(value);
        }
    }

    PlayerData PlayerData
    {
        set
        {
          //  PlayerDataManager.Instance.CurrentPlayerData = value;
        }
    }

    private void Start()
    {
        // get all data
        GetDataFromFirebase();
    }

    private void GetDataFromFirebase()
    {
        StartCoroutine(FirebaseManager.Instance.GetOneElement("Weapons", "", result => RawWeaponData = result, ""));
        StartCoroutine(FirebaseManager.Instance.GetOneElement("Enemies", "", result => RawEnemyData = result, ""));
        StartCoroutine(FirebaseManager.Instance.GetOneElement("Skins", "", result => RawSkinData = result, ""));
        StartCoroutine(FirebaseManager.Instance.GetOneElement("PlayerData", new PlayerData(), result => PlayerData = result, "0"/*PlayerPrefs.GetString("PlayerID")*/));
    }
}
