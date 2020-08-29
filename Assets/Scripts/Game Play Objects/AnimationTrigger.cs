using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
