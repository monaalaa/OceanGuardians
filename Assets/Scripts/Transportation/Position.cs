using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class allow us to add position in database
/// </summary>

[Serializable]
public class Position
{
    public float X;
    public float Y;
    public float Z;
    public Position(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
