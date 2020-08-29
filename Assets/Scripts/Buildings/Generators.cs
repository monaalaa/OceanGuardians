using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Generators : Building
{
    #region UI Parameters
    [SerializeField]
    Text numberOfGeneratedItemsText;
    [SerializeField]
    Text buildingTypeTxt;
    [SerializeField]
    Text fillPersentage;
    [SerializeField]
    Text totalCapacityTxt;
    #endregion

    WaitForSeconds wait = new WaitForSeconds(1f);
    System.DateTime endTime;

    int minutesNeededToFinish;
    int tempMinute;
    int itemPerMinute;

    private void Start()
    {
        GameManager.Instance.CollectGeneratedItems += RegenerateItems;
        minutesNeededToFinish = ItemToGenerate.TimeTakesToGenerate * MaxCapacity;
        endTime = DateTime.Now.AddMinutes(minutesNeededToFinish);
        itemPerMinute = MaxCapacity / minutesNeededToFinish;
    }

    //void Generate()
    //{
    //    if (State == BuildingState.Hibernate)
    //    {
    //        //Check First IF it needs spacific material or something to work
    //        if (ItemToGenerate.CheckForAvilableMaterialsToGenerate(ItemToGenerate.NumberOfMaterialneedePerItem*MaxCapacity))
    //        {
    //            endTime = System.DateTime.Now.AddMinutes(minutesNeededToFinish);
    //            StartCoroutine(GeneratingItemsTimer());
    //            tempMinute = (endTime - DateTime.Now).Minutes;
    //        }
    //    }
    //}

    public void CollectGeneratedItems()
    {
        if (BuildingsManager.Instance.FactoryFinishedWorking != null)
            BuildingsManager.Instance.FactoryFinishedWorking.Invoke(Collected, this);
    }

    IEnumerator GeneratingItemsTimer()
    {
        TimeSpan tempTime = endTime - DateTime.Now;
        #region If Minute Passed
        if (tempTime.Minutes < tempMinute)
        {
            tempMinute = tempTime.Minutes;
            Collected += itemPerMinute;
            numberOfGeneratedItemsText.text = Collected.ToString();
        }
        #endregion

        #region Check if Factory is filled or not
        if (tempTime.Minutes == 0)
        {
            State = BuildingState.Filled;
            //Change Above Icone
            StopCoroutine(GeneratingItemsTimer());
        }
        #endregion
        yield return wait;
        StartCoroutine(GeneratingItemsTimer());
    }

    public override void FillInfoPannel()
    {
        buildingTypeTxt.text = BuildingType.ToString();
        totalCapacityTxt.text = MaxCapacity.ToString();
        fillPersentage.text = ((float)(Collected / MaxCapacity) * 100).ToString();
    }

    public void RegenerateItems()
    {
        if (State == BuildingState.Hibernate)
        {
            Collected = 0;
            //Generate();
            State = BuildingState.Productive;
        }
    }
}

