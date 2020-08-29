using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Dragable : MonoBehaviour
{
    public static Action<Position> PositionChanged;
    [SerializeField]
    Mesh objectShapeWhileDraging;

    Mesh objectNormalShape;

    private void Start()
    {
        objectNormalShape = GetComponent<MeshFilter>().mesh;
    }

    void OnMouseDrag()
    {
        float distanceToScrean = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScrean));
        ChangeShapeOnDrag(objectShapeWhileDraging);
    }

    void OnMouseUp()
    {
        ChangeShapeOnDrag(objectNormalShape);
        if (PositionChanged != null)
        {
            Position tempPos = new Position(transform.position.x,transform.position.y,transform.position.z);
            PositionChanged.Invoke(tempPos);
        }
    }

    void ChangeShapeOnDrag(Mesh changeShape)
    {
        GetComponent<MeshFilter>().mesh = changeShape;
    }
}
