using System;
using System.Collections;
using UnityEngine;

public class HomeItem : MonoBehaviour
{
    public HomeItemViewManager ViewManager;
    public bool Clickable = false;
    public bool Dragable = false;
    public bool IsAddedToPlayerList = false;
    protected bool placed;
    public bool Placed
    {
        get
        {
            return placed;
        }

        set
        {
            placed = value;
            if (value)
            {
                OnHomeItemPlaced();
            }
            else
            {
                OnHomeItemMoved();
            }
        }
    }
    public float OverlapRadius;
    private bool overlapping;
    public bool Overlapping
    {
        get
        {
            return overlapping;
        }

        set
        {
            if (Overlapping != value)
            {
                overlapping = value;
                if (HomeItemOverlap != null)
                {
                    HomeItemOverlap.Invoke(value);
                }
            }
        }
    }
    public HomeItemType HomeItemType;
    public Action<bool> HomeItemOverlap;
    protected Camera mainCamera;
    public Action<HomeItem> HomeItemPlaced;
    public Action HomeItemMoved;
    public Action<HomeItem> HomeItemRemoved;
    protected bool ShowInfoPanel;
    protected float clickDuration;
    private const float LONG_CLICK_TIME = 2;
    private const float SHORT_CLICK_TIME = 1f;

    virtual public void OnEnable()
    {
        mainCamera = Camera.main;
        HomeItemPlaced += HomeDataManager.Instance.OnHomeItemPlaced;
        HomeItemRemoved += HomeDataManager.Instance.OnHomeItemRemoved;
        HomeItemType = HomeItemType.House;
    }

    virtual public void FixedUpdate()
    {
        if (!Placed)
        {
            IsOverlapping();
        }
    }

    public virtual void IsOverlapping()
    {
        var hits = Physics2D.OverlapCircleAll(this.transform.position, OverlapRadius);
        if (hits.Length > 1)
        {
            Overlapping = true;
        }
        else
        {
            Overlapping = false;
        }
    }

    public void AttachItemToMousePosition()
    {
        var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    virtual protected void OnHomeItemPlaced()
    {
        HomeManager.Instance.HeldHomeItem = null;
        ViewManager.ShowBaseUI(false);
        IsAddedToPlayerList = true;
        if (HomeItemPlaced != null)
        {
            HomeItemPlaced.Invoke(this);
        }
    }

    virtual protected void OnHomeItemMoved()
    {
        ViewManager.ShowBaseUI(true);
        if (HomeItemMoved != null)
        {
            HomeItemMoved.Invoke();
        }
    }

    virtual protected void OnHomeItemRemoved()
    {
        if (HomeItemRemoved != null)
        {
            HomeItemRemoved.Invoke(this);
        }
    }

    public void PlaceHomeItem()
    {
        if (!Overlapping)
        {
            Placed = true;
            var audioClip = Resources.Load<AudioClip>("Audio/BuildingSound2");
            // play sound
            SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioClip, 1);
        }
    }

    public void DeleteHomeItem()
    {
        Destroy(this.gameObject);
        if (IsAddedToPlayerList)
        {
            OnHomeItemRemoved();
        }
    }

    public void OnDestroy()
    {
        HomeItemPlaced -= HomeDataManager.Instance.OnHomeItemPlaced;
    }

    virtual public void OnMouseDown()
    {
        ShowInfoPanel = false;
        clickDuration = 0;
        if (Placed)
        {
            // initialize start timer
            clickDuration = Time.time;
            StartCoroutine("StartTimer");
        }
    }

    IEnumerator StartTimer()
    {
       
        var tmpVal = 0f;
        while ((Time.time - clickDuration) < LONG_CLICK_TIME)
        {
            if ((Time.time - clickDuration) > SHORT_CLICK_TIME)
            {
                HomeUIManager.Instance.ShowFillMoveImg(true, Input.mousePosition);
                HomeUIManager.Instance.UpdateFillMoveImg(tmpVal);
                tmpVal += Time.deltaTime;
            }
            yield return null;
        }
        Placed = false;
        HomeUIManager.Instance.ShowFillMoveImg(false);
    }

    virtual public void OnMouseDrag()
    {
        if (!Placed && Dragable)
        {
            AttachItemToMousePosition();
            HomeManager.Instance.HeldHomeItem = this.gameObject;
            HomeManager.Instance.ShowGrid(true);
        }
    }

    virtual public void OnMouseUp()
    {
        if (Placed)
        {
            if (Clickable)
            {
                // reset timer
                if ((Time.time - clickDuration) <= SHORT_CLICK_TIME)
                {
                    ShowInfoPanel = true;
                }
            }
            StopCoroutine("StartTimer");
            HomeUIManager.Instance.ShowFillMoveImg(false);
        }
        HomeManager.Instance.ShowGrid(false);
    }

    virtual public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, OverlapRadius);
    }
}
