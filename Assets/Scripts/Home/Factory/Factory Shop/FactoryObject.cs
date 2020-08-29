using UnityEngine;

public enum FactoryType
{
    A, B, C
}

[System.Serializable]
public class FactoryObject : ScriptableObject
{
    public string Name;
    public FactoryType FactoryType;
    public Sprite FactorySprite;
    public CollectableType CollectableType;
    public float TimeTakenToGenerateAnItem;
    public int FactoryCapacity;
    public int Cost;
    public int AmountNeededToGenerate;
}
