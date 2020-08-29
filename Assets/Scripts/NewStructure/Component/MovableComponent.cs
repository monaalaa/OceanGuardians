using UnityEngine;

[DisallowMultipleComponent]
public class MovableComponent : Component
{
    [SerializeField]
    MovingData movingData;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (movingData.Direction == Direction.UP_Down)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (movingData.Speed * movingData.MovingDirection), transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x + (movingData.Speed * movingData.MovingDirection), transform.localPosition.y, transform.localPosition.z);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (movingData.MoveTolimite)
        {
            if (collision.gameObject.tag == "Edge")
            {
                movingData.MovingDirection *= -1;
            }
        }
    }
}

[System.Serializable]
public class MovingData
{
    public float Speed;
    public Direction Direction;
    public int MovingDirection = 1;
    public bool MoveTolimite;
}
