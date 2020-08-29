
using UnityEngine;
using System;

[RequireComponent(typeof(CreateUIDynamicaly))]
public class TransportationUI : GameComponentsUI
{
    [SerializeField]
    GameObject infoPanel;

    [SerializeField]
    GameObject optionsPanel;

    [SerializeField]
    GameObject itemsPanel;

    Transportation transportation;

    private void Start()
    {
        transportation = transform.parent.GetComponent<Transportation>();
    }

    public void ToggleInfoPannel()
    {
        //Fill info panel with data
        if(infoPanel.transform.childCount==0)
        {
            FillInformationLine();
        }
        infoPanel.SetActive(true);
    }

    public void ToggleOptionalPanel(bool toggle)
    {
        FillOptionPanelWithBehaviourBTNs();
        optionsPanel.SetActive(true);
    }

    void FillInformationLine()
    {
        for (int i = 0; i < transportation.TransportationFeaturesList.Count; ++i)
        {
            string featureInfo = transportation.TransportationFeaturesList[i].FeatureString;
            CreateUIDynamicaly.Instance.InstantiateTextAtRunTime(featureInfo, infoPanel.transform, "Transportation/Transportation Info Text");
        }
    }
   
    void FillOptionPanelWithBehaviourBTNs()
    {
        if (optionsPanel.transform.childCount == 0)
        {
            for (int i = 0; i < transportation.TransportationFeaturesList.Count;++i)
            {
                CreateUIDynamicaly.Instance.InstantiateButton(optionsPanel.transform, "transportation/Feature BTN",
                          this, transportation.transportationData.TransportationProperties[i].type.ToString(), transportation.TransportationFeaturesList[i].FeatureShowName, "OnCLickBTN");
            }
        }
    }

    //void FillItemsPanel()
    //{
    //    if (itemsPanel.transform.childCount == 0)
    //    {
    //        foreach (var item in PlayerDataManager.Instance.CurrentPlayerData.ItemsCapacity)
    //        {
    //            CreateUIDynamicaly.Instance.InstantiateButton(itemsPanel.transform, "transportation/Feature BTN", this, item.Key, item.Key, "OnItemClicked");
    //        }
    //    }
    //}

    public void OnCLickBTN(string type)
    {
        transportation.PreformBehaviour((TransportationFeaturesEnum)Enum.Parse(typeof(TransportationFeaturesEnum), type));
        optionsPanel.SetActive(false);
    }

    //public void OnItemClicked(string itemType)
    //{
    //    int managerCapacity = PlayerDataManager.Instance.CurrentPlayerData.ItemsCapacity.Find(x => x.Key == itemType).Value;
        
    //    //Get Capacity from cintaining feature
    //    int transportationCapacity = ((Containable)transportation.TransportationFeaturesList.Find
    //        (item => item.transportationFeaturesEnum == TransportationFeaturesEnum.Containable)).maxCapacity;

    //    if (managerCapacity == transportationCapacity)
    //    {
    //        TransportationManager.Instance.InvokeCanPreform(transportation.gameObject, TransportationFeaturesEnum.Containable);
    //        GameManager.Instance.InvokeCollectGeneratedItems();
    //    }
    //}
}
