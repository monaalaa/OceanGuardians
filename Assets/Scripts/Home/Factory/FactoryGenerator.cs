using System;
using System.Collections;
using UnityEngine;

public class FactoryGenerator : MonoBehaviour
{
    public FactoryManager Manager;
    public DateTime DateTimeToFinishGeneration;
    public DateTime ClosingGameDateTime;
    private WaitForSeconds waitTimeToGenerateItem;
    private float remainder;
    private WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

    void Start()
    {
        GenerateItemSinceLastDateTime();
    }

    private void GenerateItemSinceLastDateTime()
    {
        if (Manager.Placed)
        {
            if (Manager.GeneratedItemsCount < Manager.CurrentFactory.FactoryCapacity && DateTimeToFinishGeneration != default(DateTime))
            {
                GenerateMissedItems();
                // Start generation
                Generate(true);
            }
            else
            {
                CalculatEstimatedTimeToFinish();
            }
        }

    }

    private void CalculatEstimatedTimeToFinish()
    {
        // get the total time to generate the capacity of collectables in the factory
        var timeNeededToFinishGenerationInSec = Manager.CurrentFactory.FactoryCapacity * Manager.CurrentFactory.TimeTakenToGenerateAnItem;
        // get the time in HH:MM:SS to finish generation.
        DateTimeToFinishGeneration = DateTime.Now.AddSeconds(timeNeededToFinishGenerationInSec);
    }

    private bool IsFactoryFilled()
    {
        if (Manager.GeneratedItemsCount >= Manager.CurrentFactory.FactoryCapacity)
        {
            Manager.GeneratedItemsCount = Manager.CurrentFactory.FactoryCapacity;
            return true;
        }
        return false;
    }

    private IEnumerator GenerateCollectibles()
    {
        while (!IsFactoryFilled())
        {
            if (Manager.CurrentFactory.CollectableType == CollectableType.RecycleBlock)
            {
                if (DataManager.Instance.RecycleScore > Manager.CurrentFactory.AmountNeededToGenerate)
                {
                    DataManager.Instance.RecycleScore = -Manager.CurrentFactory.AmountNeededToGenerate;
                    Generate();
                }
                else
                {
                    if (Manager.GeneratedItemsCount == 0)
                    {
                        ((FactoryViewManager)Manager.ViewManager).ShowCollectable(false);
                    }
                }
            }
            else
            {
                Generate();
            }
            yield return waitTimeToGenerateItem;
        }
    }

    void Generate()
    {
        if (waitTimeToGenerateItem == null)
        {
            waitTimeToGenerateItem = new WaitForSeconds(Manager.CurrentFactory.TimeTakenToGenerateAnItem);
        }
        else
        {
            if (remainder > 0)
            {
                waitTimeToGenerateItem = new WaitForSeconds(Manager.CurrentFactory.TimeTakenToGenerateAnItem - remainder);
                remainder = 0;
            }
        }
        Manager.GeneratedItemsCount += 1;
    }

    internal void Generate(bool generate)
    {
        if (generate)
        {
            CalculatEstimatedTimeToFinish();
            StartCoroutine("GenerateCollectibles");
        }
        else
        {
            StopCoroutine("GenerateCollectibles");
        }
    }

    void GenerateMissedItems()
    {
        var timeNow = DateTime.Now;
        TimeSpan diffInTimeInSec = (timeNow - DateTimeToFinishGeneration);

        // add to items generated
        if (diffInTimeInSec.Seconds < 0)
        {
            var closingTime = Convert.ToDateTime(PlayerPrefs.GetString("ClosingGameDateTime"));
            // calculate the elapsed time in sec.
            var timePassed = ((timeNow - closingTime).Hours * 3600) + ((timeNow - closingTime).Minutes * 60) + (timeNow - closingTime).Seconds;
            Manager.GeneratedItemsCount += (int)(timePassed / Manager.CurrentFactory.TimeTakenToGenerateAnItem);
            remainder = timePassed % Manager.CurrentFactory.TimeTakenToGenerateAnItem;
        }
        else
        {
            Manager.GeneratedItemsCount = Manager.CurrentFactory.FactoryCapacity;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            ClosingGameDateTime = DateTime.Now;
            PlayerPrefs.SetString("ClosingGameDateTime", ClosingGameDateTime.ToString());
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            GenerateItemSinceLastDateTime();
        }
        else
        {
            ClosingGameDateTime = DateTime.Now;
            PlayerPrefs.SetString("ClosingGameDateTime", ClosingGameDateTime.ToString());
        }
    }
}
