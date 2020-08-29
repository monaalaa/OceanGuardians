using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class PlayerData
{
    public float Speed;
    public float JumpSpeed;

    public Position StartPosition;
    public string WeaponName;

    public Weapon Weapon;
    public string Skin;

    public PlayerData() { }

    public PlayerData(float Speed, float JumpSpeed, Position StartPosition, string Skin)
    {
        this.Speed = Speed;
        this.JumpSpeed = JumpSpeed;
        this.StartPosition = StartPosition;
        this.Skin = Skin;
    }
}

[Serializable]
public class DataAsDictionary
{
    public string Key;
    public int Value;

    public DataAsDictionary(string key, int value)
    {
        Key = key;
        Value = value;
    }
}