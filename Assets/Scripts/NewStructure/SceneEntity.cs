using System;
using System.Collections.Generic;
using UnityEngine;

public enum ComponentsEnum
{
    None,
    DeathComponent,
    BounceComponent,
    AttachComponent,
    DeAttachComponent,
    MovableComponent,
    ClimbComponent,
    EdgeComponent
}
[ExecuteInEditMode]
public class SceneEntity : MonoBehaviour
{
    private ComponentsEnum componentList = new ComponentsEnum();
    Action<string> addComponent;

    public ComponentsEnum ComponentList
    {
        get
        {
            return componentList;
        }

        set
        {
            if (componentList != value)
            {
                addComponent.Invoke(value.ToString());
            }

            componentList = value;
        }
    }
  
    private void OnEnable()
    {
        addComponent += AddComponent;
    }

    private void AddComponent(string componentType)
    {
        gameObject.AddComponent(Type.GetType(componentType));
    }
}
