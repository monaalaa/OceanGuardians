using UnityEngine;

public class Collectable : MonoBehaviour
{
    public FactoryManager FactoryManager;

    private void OnMouseDown()
    {
        FactoryManager.CollectItem();
    }
}
