using UnityEngine;
using System.Collections.Generic;

public enum CollectableType
{
    Gold,
    Fish,
    Recycle,
    Pearl,
    RecycleBlock
}

[RequireComponent(typeof(FactoryGenerator))]
[RequireComponent(typeof(FactoryViewManager))]
public class FactoryManager : HomeItem
{

    public GameObject particles;
    public FactoryObject CurrentFactory;
    public List<FactoryObject> FactoriesUpgrades;
    public FactoryGenerator Generator;
    private int generatedItemsCount;
    private int currentFactoryLevel;
    public int CurrentFactoryLevel
    {
        get
        {
            return currentFactoryLevel;
        }

        set
        {
            if (value <= FactoriesUpgrades.Count - 1)
            {
                currentFactoryLevel = value;
                ViewManager = GetComponent<FactoryViewManager>();
                CurrentFactory = FactoriesUpgrades[CurrentFactoryLevel];
                ((FactoryViewManager)ViewManager).UpdateFactoryView();
            }

        }
    }
    public string Description;
    public bool DependOnItem;
    public int GeneratedItemsCount
    {
        get
        {
            return generatedItemsCount;
        }

        set
        {
            generatedItemsCount = value;
            ((FactoryViewManager)ViewManager).UpdateCollectableView();
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        Generator = GetComponent<FactoryGenerator>();
        Description = GetDescriptionOfFactory(CurrentFactoryLevel);
        CurrentFactory = FactoriesUpgrades[CurrentFactoryLevel];
        HomeItemType = HomeItemType.Factory;
    }

    public void UpgradeFactory()
    {
        CurrentFactoryLevel++;
        ((FactoryViewManager)ViewManager).UpdateCollectableView();
        HomeManager.Instance.UpdateScore(-CurrentFactory.Cost, CollectableType.Gold);
        HomeManager.Instance.OnFactoryUpgraded(this);
        particles.SetActive(true);
        Invoke("HideParticles", 2);
    }

    internal void CollectItem()
    {
        Generator.Generate(false);
        HomeManager.Instance.UpdateScore(GeneratedItemsCount, CurrentFactory.CollectableType);
        GeneratedItemsCount = 0;
        // Start generation
        Generator.Generate(true);
    }

    protected override void OnHomeItemPlaced()
    {
        base.OnHomeItemPlaced();
        Generator.Generate(true);
    }

    protected override void OnHomeItemMoved()
    {
        base.OnHomeItemMoved();
        Generator.Generate(false);
    }

    public override void IsOverlapping()
    {
        var hits = Physics2D.OverlapCircleAll(this.transform.position, OverlapRadius);
        if (hits.Length > 1)
        {
            if (CurrentFactory.CollectableType == CollectableType.Fish)
            {
                foreach (var hit in hits)
                {
                    if (hit.transform.position != transform.position)
                    {
                        if (hit.tag.Equals("Sea"))
                        {
                            Overlapping = false;
                        }
                        else
                        {
                            Overlapping = true;
                        }
                    }
                }
            }
            else
            {
                Overlapping = true;
            }
        }
        else
        {
            if (CurrentFactory.CollectableType == CollectableType.Fish)
            {
                Overlapping = true;
            }
            else
            {
                Overlapping = false;
            }
        }
    }

    public string GetDescriptionOfFactory(int index)
    {
        string txt = "";
        if (FactoriesUpgrades[index].CollectableType == CollectableType.RecycleBlock)
        {
            txt = "This factory is used to generate recycle blocks from the litter you collect.\n";
        }
        else if (FactoriesUpgrades[index].CollectableType == CollectableType.Fish)
        {
            txt = "This factory is used to generate fish which you can store or buy in the marketplace.\n";
        }
        var desc = txt + "Capacity: " + FactoriesUpgrades[index].FactoryCapacity + "\n" +
                       "Time Taken to Generate an Item: " + FactoriesUpgrades[index].TimeTakenToGenerateAnItem;
        return desc;
    }

    void HideParticles()
    {
        particles.SetActive(false);
    }

    override public void OnMouseUp()
    {
        base.OnMouseUp();
        if (ShowInfoPanel)
        {
            HomeManager.Instance.OnFactoryClicked(this);
        }
    }


}
