using System;
using System.Collections.Generic;
using UnityEngine;

public enum HomeItemType
{
    House,
    Factory
}

public class HomeDataManager : MonoBehaviour
{
    HomeDataController dataController;
    public static HomeDataManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        HomeDataController.GetHomeItems();
     //   HomeDataController.HomeDataGeted += OrderLayers;
        OrderLayers();
    }

    public void OnHomeItemPlaced(HomeItem homeItem)
    {
        DataManager.Instance.PlayerHomeItems.Add(homeItem);
        OrderLayers();
    }

    public void OnHomeItemRemoved(HomeItem homeItem)
    {
        DataManager.Instance.PlayerHomeItems.Remove(homeItem);
    }

    private void OrderLayers()
    {
        List<HomeItem> playerHomeItems = DataManager.Instance.PlayerHomeItems;
        // order Y position in ascending order.
        playerHomeItems.Sort((a, b) => a.gameObject.transform.position.y.CompareTo(b.gameObject.transform.position.y));

        // order layers {x = 3, 5, 7..}  
        int order = 3;
        float minZ = 1;
        float maxZ = Camera.main.transform.position.z;
        float zPos = minZ;
        float zOffset = (minZ + maxZ) / (playerHomeItems.Count);
        for (int i = playerHomeItems.Count - 1; i >= 0; i--)
        {
            zPos += zOffset;
            var go = playerHomeItems[i].gameObject;
            go.GetComponent<SpriteRenderer>().sortingOrder = order;
            // get base object layer
            go.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = order - 1;
            // get canvas object layer
            go.transform.GetChild(1).gameObject.GetComponent<Canvas>().sortingOrder = order;
            order += 2;
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, Mathf.Clamp(zPos, maxZ + 0.4f, zPos));
        }
    }
   
}
