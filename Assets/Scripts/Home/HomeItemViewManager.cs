using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeItemViewManager : MonoBehaviour
{

    public HomeItem HomeItem;
    public Button YesButton;
    public GameObject ConfirmationPanel;
    public GameObject BaseGo;
    public SpriteRenderer BaseSprite;
    public Image FillMoveImage;
    public GameObject FillMovePanel;

    public virtual void Start()
    {
        BaseGo = transform.GetChild(0).gameObject;
        BaseSprite = BaseGo.GetComponent<SpriteRenderer>();
        HomeItem.HomeItemOverlap += ChangeBaseUI;
        HomeItem.HomeItemPlaced += OnHomeItemPlaced;
    }

    public void EnableYesButton(bool enable)
    {
        YesButton.interactable = enable;
    }

    public void ChangeBaseColor(bool overlapped)
    {
        if (overlapped)
        {
            BaseSprite.color = new Color(1, 0, 0, 107f / 255f);
        }
        else
        {
            BaseSprite.color = new Color(0, 1, 0, 107f / 255f);
        }
    }

    public void ChangeOverlapColor(bool overlap)
    {
        if (overlap)
        {
            ChangeBaseColor(true);
            EnableYesButton(false);
        }
        else
        {
            ChangeBaseColor(false);
            EnableYesButton(true);
        }
    }

    virtual public void ShowBaseUI(bool show)
    {
        BaseGo.SetActive(show);
        ConfirmationPanel.SetActive(show);
    }

    private void OnHomeItemPlaced(HomeItem arg1)
    {
        ShowBaseUI(false);
    }

    virtual public void ChangeBaseUI(bool overlap)
    {
        if (!HomeItem.Placed)
        {
            ChangeOverlapColor(overlap);
        }
    }

}
