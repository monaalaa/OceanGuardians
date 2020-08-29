using System;
using UnityEngine;

[Serializable]
public class SkinData
{
    private string name;
    private int cost;
    // TODO: URI is not important
    private string textureURI;
    private string textureLocalPath;
    // TODO: Texture is not important
    private Texture2D texture;
    private bool HasError;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            if (value != null)
            {
                name = value;
            }
            else
            {
                ShowError("Name");
            }
        }
    }
    public int Cost
    {
        get
        {
            return cost;
        }

        set
        {
            if (value >= 0)
            {
                cost = value;
            }
            else
            {
                ShowError("Cost");
            }
        }
    }
    public string TextureURI
    {
        get
        {
            return textureURI;
        }

        set
        {
            if (value != null)
            {
                textureURI = value;
            }
            else
            {
                ShowError("Texture URI");
            }
        }
    }
    public string TextureLocalPath
    {
        get
        {
            return textureLocalPath;
        }

        set
        {
            if (value != null)
            {
                textureLocalPath = value;
                if (SkinDataComplete != null && !HasError)
                {
                    SkinDataComplete.Invoke(this);
                }
            }
            else
            {
                ShowError("Texture Local Path");
            }
        }
    }
    public Texture2D Texture
    {
        get
        {
            return texture;
        }

        set
        {
            if (value != null)
            {
                texture = value;
            }
            else
            {
                ShowError("Texture");
            }
        }
    }


    public Action<SkinData> SkinDataComplete;

    public SkinData() { }
    public SkinData(string name, Texture2D image, int cost)
    {
        Name = name;
        Texture = image;
        Cost = cost;
    }

    private void ShowError(string propertyWithError)
    {
        HasError = true;
        Debug.LogError(propertyWithError + " has an invalid input. (null or value less than 0)");
    }
}
