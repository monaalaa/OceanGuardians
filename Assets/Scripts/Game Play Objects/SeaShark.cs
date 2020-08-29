using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaShark : MonoBehaviour {

    [SerializeField]
    GameObject positionsParent;

    [SerializeField]
    float Speed;

    List<GameObject> positions = new List<GameObject>(); 

    // Use this for initialization
    void Start()
    {
        FillPositionsList();
        GetRandomPosition();
        GetComponent<Animator>().Play("Shark Swim");
    }

    void FillPositionsList()
    {
        foreach (Transform item in positionsParent.transform)
        {
            positions.Add(item.gameObject);
        }
    }

    void GetRandomPosition()
    {
        int random = Random.Range(0, positions.Count);
        iTween.MoveTo(gameObject, iTween.Hash("oncomplete", "GetRandomPosition", "position", positions[random].transform
            , "orienttopath",true, "easetype",iTween.EaseType.linear,"time", Speed));
    }
}
