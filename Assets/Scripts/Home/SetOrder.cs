using UnityEngine;

public class SetOrder : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = transform.parent.parent.gameObject.GetComponent<Canvas>().sortingOrder;
    }
}
