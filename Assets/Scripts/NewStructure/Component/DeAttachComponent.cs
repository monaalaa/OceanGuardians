using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class DeAttachComponent : Component
{
    [SerializeField]
    AttachData componentData;

    // Use this for initialization
    void Start()
    {
        //SubscripeToAttach
        PlayerManager.Instance.OnPlayeJump += Deattach;

    }

    void Deattach()
    {
        componentData.End.connectedBody = null;
        componentData.End.connectedBody = componentData.End.transform.parent.GetComponent<Rigidbody>();
        StartCoroutine(Wait());
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
