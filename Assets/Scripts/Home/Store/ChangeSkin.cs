using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ChangeSkin : MonoBehaviour
{
    public GameObject DummyPlayerToChangeSkinFor;
    public Text CostTextField;
    public List<SkinData> SkinData;
    private int currIndex;
    private Material dummyPlayerMatl;

    private void Start()
    {
        dummyPlayerMatl = DummyPlayerToChangeSkinFor.GetComponent<MeshRenderer>().sharedMaterial;
        SkinData = DataManager.SkinData;
        UpdateSkinOnDummyPlayer();
    }

    private void AddSkinData(List<SkinData> skinData)
    {
        SkinData = skinData;
        UpdateSkinOnDummyPlayer();
    }

    public void ChangePlayerSkin(bool next)
    {
        if (SkinData != null)
        {
            DecideIndexOfNextSkin(next);
            UpdateSkinOnDummyPlayer();
        }
    }

    private void UpdateSkinOnDummyPlayer()
    {
        dummyPlayerMatl.mainTexture = SkinData[currIndex].Texture;
        CostTextField.text = "$" + SkinData[currIndex].Cost.ToString();
    }

    private void DecideIndexOfNextSkin(bool next)
    {
        if (next)
        {
            if (currIndex >= SkinData.Count - 1)
            {
                currIndex = SkinData.Count - 1;
            }
            else
            {
                currIndex++;
            }
        }
        else
        {
            if (currIndex <= 0)
            {
                currIndex = 0;
            }
            else
            {
                currIndex--;

            }
        }
    }

    public void SetPlayerSkin()
    {
        PlayerController.Instance.LocalPlayerInstance.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = dummyPlayerMatl.mainTexture;
        // TODO: deduct from player coins
    }

}
