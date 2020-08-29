using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    UP_Down,
    Right_Left
}
public class Elevator : MonoBehaviour {

    [SerializeField]
    float Speed;

    [SerializeField]
    Direction Direction;

    WaitForEndOfFrame wait =new WaitForEndOfFrame();

    public int movingDirection = 1;

    private void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return wait;
        if (Direction == Direction.UP_Down)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (Speed * movingDirection), transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x+(Speed * movingDirection), transform.localPosition.y , transform.localPosition.z);
        }
        StartCoroutine(Move());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Edge")
        {
            movingDirection *= -1;
        }
    }
}