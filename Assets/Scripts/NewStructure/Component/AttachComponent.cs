using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttacheType
{
    AttachAsChild,
    AttachToJoint
}

[DisallowMultipleComponent]
public class AttachComponent : Component
{
    [SerializeField]
    AttachData componentData;

    private void Start()
    {
        PlayerManager.Instance.OnPlayeJump += Deattach;
    }
    public override void PreformComponent()
    {
        if (componentData.attacheType == AttacheType.AttachAsChild)
        {
            AttachChild();
        }
        else if (componentData.attacheType == AttacheType.AttachToJoint)
        {
            AttachJoint();
            //Call AttachToJoint from Player
            ObjectToPreform.ExcuteAttachToJoint();
        }
    }

    void AttachChild()
    {
        ObjectToPreform.transform.SetParent(this.transform);
    }

    void AttachJoint()
    {
        AttachChild();
        componentData.Rig.drag = 0;
        foreach (Rigidbody item in componentData.RigidBodies)
        {
            item.drag = 0;
        }
        ObjectToPreform.ExcuteAttachToJoint();
        componentData.End.connectedBody = ObjectToPreform.GetComponent<Rigidbody>();
    }

    void Deattach()
    {
        if (ObjectToPreform != null)
        {
            componentData.End.connectedBody = null;
            componentData.End.connectedBody = componentData.End.transform.parent.GetComponent<Rigidbody>();
            ObjectToPreform.ExcuteDeAttachFromJoint();
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForEndOfFrame();
        componentData.Rig.drag = 100000;
        foreach (Rigidbody item in componentData.RigidBodies)
        {
            item.drag = 3;
        }
        yield return new WaitForEndOfFrame();
        GetComponent<BoxCollider>().enabled = true;
    }
}

[System.Serializable]
public class AttachData
{
    public AttacheType attacheType;

    [ConditionalField("attacheType", AttacheType.AttachToJoint)]
    public CharacterJoint End;

    [ConditionalField("attacheType", AttacheType.AttachToJoint)]
    public Rigidbody Rig;

    [ConditionalField("attacheType", AttacheType.AttachToJoint)]
    public Rigidbody[] RigidBodies;
}
