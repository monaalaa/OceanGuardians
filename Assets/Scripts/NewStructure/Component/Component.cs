using UnityEngine;

public class Component : MonoBehaviour
{
    [SerializeField]
    protected bool DestroyObject;
    protected GameCharacters ObjectToPreform;

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<GameCharacters>()!=null)
        {
            ObjectToPreform = collision.gameObject.GetComponent<GameCharacters>();
            PreformComponent();
        }
    }

    public virtual void PreformComponent()
    { }
}
