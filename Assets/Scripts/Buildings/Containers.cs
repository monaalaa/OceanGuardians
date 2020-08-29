using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Containers : Building
{
    #region UI Parameters
    [SerializeField]
    Text buildingTypeTxt;
    [SerializeField]
    Text capacityTxt;
    #endregion

    public override void FillInfoPannel()
    {
        buildingTypeTxt.text = BuildingType.ToString();
        capacityTxt.text = MaxCapacity.ToString();
    }
}
