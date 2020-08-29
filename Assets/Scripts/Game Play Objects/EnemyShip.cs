using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public float MovingSpeed;
    public bool ReachedEnd;
    public Transform EndPoint;
    public Transform[] Wheels;

    void Start()
    {

    }

    private void Update()
    {
        if (!ReachedEnd)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(EndPoint.position.x, transform.position.y, transform.position.z), Time.deltaTime * MovingSpeed);
        }

        for (int i = 0; i < Wheels.Length; i++)
        {
            Wheels[i].Rotate(-transform.forward * 4);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("EndPoint"))
        {
            ReachedEnd = true;
        }
    }
}
