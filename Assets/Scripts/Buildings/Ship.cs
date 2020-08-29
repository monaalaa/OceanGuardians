using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    //[SerializeField]
    //int MinutesNeededToFinish;

    //WaitForSeconds wait = new WaitForSeconds(1f);

    //private void Start()
    //{
    //    EndTime = DateTime.Now.AddMinutes(MinutesNeededToFinish);
    //    BuildingsManager.Instance.OnBuildingFilled += BuildingFilled;
    //    BuildingType = BuildingType.Ship;
    //}

    //public IEnumerator Timer()
    //{
    //    TimeSpan tempTime = EndTime - DateTime.Now;
    //    NumberOfGeneratedItemsText.text = tempTime.Hours.ToString() + ":" + tempTime.Minutes.ToString() + ":" + tempTime.Seconds.ToString();

    //    #region CheckForTimeOut
    //    if (tempTime.Minutes == 0)
    //    {
    //        State = BuildingState.Hibernate;
    //        //Reset it's position in map
    //        StopCoroutine(Timer());
    //    }
    //    #endregion
    //    yield return wait;
    //}

    //void BuildingFilled(Building building)
    //{
    //    if (building == this)
    //    {
    //        //Move animation in map
    //        StartCoroutine(Timer());
    //    }
    //}
    public void CalculateCpacity()
    {
        throw new NotImplementedException();
    }
}
