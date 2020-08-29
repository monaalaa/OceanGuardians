using System;
using UnityEngine;

public class HomeManager : MonoBehaviour
{

    private GameObject heldHome;
    public GameObject HeldHomeItem
    {
        get
        {
            return heldHome;
        }

        set
        {
            if (heldHome != value && value != null)
            {
                Destroy(heldHome);
            }
            heldHome = value;
            UpdateSortingLayerOrder(100);
        }
    }
    public GameObject Grid;
    public Action StoreClicked;
    public Action<string, Transform> LevelEntered;
    public Action<FactoryManager> FactoryUpgraded;
    public static HomeManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void UpdateSortingLayerOrder(int order)
    {
        if (HeldHomeItem != null)
        {
            HeldHomeItem.GetComponent<SpriteRenderer>().sortingOrder = order;
            HeldHomeItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order - 1;
            HeldHomeItem.transform.GetChild(1).GetComponent<Canvas>().sortingOrder = order + 1;
        }
    }

    internal void OnFactoryClicked(FactoryManager nextFactoryUpgrade)
    {
        HomeUIManager.Instance.ShowFactoryPanel(true);
        HomeUIManager.Instance.PopulateFactoryData(nextFactoryUpgrade);
    }

    internal void UpdateScore(int countCollected, CollectableType scoreType)
    {
        DataManager.Instance.AddToScore(countCollected, scoreType);
    }

    internal void OnFactoryUpgraded(FactoryManager factoryManager)
    {
        if (FactoryUpgraded != null)
        {
            FactoryUpgraded.Invoke(factoryManager);
        }
    }

    internal void OnStoreClicked()
    {
        if (StoreClicked != null)
        {
            StoreClicked.Invoke();
        }
    }

    internal void OnLevelEntered(string levelName, Transform clickableObject)
    {
        if (LevelEntered != null)
        {
            LevelEntered.Invoke(levelName, clickableObject);
        }
    }

    internal void ShowGrid(bool show)
    {
        Grid.SetActive(show);
    }
}
