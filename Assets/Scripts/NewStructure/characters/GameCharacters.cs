using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterStatus
{
    Jump,
    Hang,
    Grounded,
    Climbing,
    Died,
    Pushing
}

public class GameCharacters : MonoBehaviour
{
    public virtual void ExcuteDeath(string placeOfDeath)
    { }

    public virtual void ExcuteAddForce(float forceValue, Vector3 direction)
    {
    }

    public virtual void ExcuteClimb()
    { }

    public virtual void DeExcuteClimb()
    { }

    public virtual void ExcuteClimbEdge()
    { }

    public virtual void DeExcuteClimbEdge()
    { }

    public virtual void ExcuteAttachToJoint()
    {

    }

    public virtual void ExcuteDeAttachFromJoint()
    {
    }
}

