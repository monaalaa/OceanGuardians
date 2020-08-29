using UnityEngine;

public class RotateMe : MonoBehaviour
{
    public float Speed = 100;
    private Vector3 axis = new Vector3(0, 0, 1);

    void Update()
    {
        transform.Rotate(axis, Speed * Time.deltaTime);
    }
}
