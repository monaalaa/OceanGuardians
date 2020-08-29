using System;
using UnityEngine;
using UnityEngine.UI;

public class FactoryViewManager : HomeItemViewManager
{
    public GameObject CollectablePanel;
    public Image CollectableFillImage;

    public override void Start()
    {
        base.Start();
        GetComponent<SpriteRenderer>().sprite = ((FactoryManager)HomeItem).CurrentFactory.FactorySprite;
    }

    internal void UpdateCollectableView()
    {
        CollectableFillImage.fillAmount = ((float)((FactoryManager)HomeItem).GeneratedItemsCount / ((FactoryManager)HomeItem).CurrentFactory.FactoryCapacity);
    }

    internal void ShowCollectable(bool show)
    {
        CollectablePanel.SetActive(show);
    }

    override public void ShowBaseUI(bool show)
    {
        base.ShowBaseUI(show);
        ShowCollectable(!show);
    }

    override public void ChangeBaseUI(bool overlap)
    {
        base.ChangeBaseUI(overlap);
    }

    internal void UpdateFactoryView()
    {
        if (((FactoryManager)HomeItem) != null)
        {
            ((FactoryManager)HomeItem).GetComponent<SpriteRenderer>().sprite = ((FactoryManager)HomeItem).CurrentFactory.FactorySprite;
        }
    }
}
