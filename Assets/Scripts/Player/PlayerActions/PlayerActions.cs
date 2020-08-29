using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerActions
{
    public GameObject ObjectToPreformActionOn;

    protected bool ActionPreformed;

    public PlayerActions()
    {
        
    }

    public abstract void DoAction();
}
