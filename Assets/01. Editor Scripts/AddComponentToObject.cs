using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum ComponentsEditor
{
    Death = 0,
    Push = 1,
    Climb = 2,
    Hang = 3
}

[ExecuteInEditMode]
public class AddComponentToObject : MonoBehaviour
{
    ComponentsEditor componentList = new ComponentsEditor();

    [SerializeField]
    bool death;
    [SerializeField]
    bool push;
    [SerializeField]
    bool climb;
    [SerializeField]
    bool hang;

    DeathComponentEditor deathComponent = new DeathComponentEditor();
    PushComponentEditor pushComponent = new PushComponentEditor();
    ClimbComponentEditor climbComponent = new ClimbComponentEditor();
    HangComponentEditor hangComponent = new HangComponentEditor();

    Action<string> addComponent;

    public bool Death
    {
        get
        {
            return death;
        }

        set
        {
            if (death != value)
            {
                addComponent.Invoke("Death");
            }

            death = value;
        }
    }

    public bool Push
    {
        get
        {
            return push;
        }

        set
        {
            if (push != value)
            {
                addComponent.Invoke("Push");
            }

            push = value;
        }
    }

    public bool Climb
    {
        get
        {
            return climb;
        }

        set
        {
            if (climb != value)
            {
                addComponent.Invoke("Climb");
            }

            climb = value;
        }
    }

    public bool Hang
    {
        get
        {
            return hang;
        }

        set
        {
            if (hang != value)
            {
                addComponent.Invoke("Hang");
            }

            hang = value;
        }
    }

    public ComponentsEditor ComponentList
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
        switch (componentType)
        {
            case "Death":
                if (!death)
                {
                    deathComponent = gameObject.AddComponent<DeathComponentEditor>();
                }
                else
                {
                    DestroyImmediate(deathComponent);
                }
                break;
            case "Push":
                if (!push)
                {
                    pushComponent = gameObject.AddComponent<PushComponentEditor>();
                }
                else
                {
                    DestroyImmediate(pushComponent);
                }
                break;
            case "Climb":
                if (!climb)
                {
                    climbComponent = gameObject.AddComponent<ClimbComponentEditor>();
                }
                else
                {
                    DestroyImmediate(climbComponent);
                }
                break;
            case "Hang":
                if (!hang)
                {
                    hangComponent = gameObject.AddComponent<HangComponentEditor>();
                }
                else
                {
                    DestroyImmediate(hangComponent);
                }
                break;
            default:
                break;
        }
    }
}
