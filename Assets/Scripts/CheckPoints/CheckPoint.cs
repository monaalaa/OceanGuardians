using UnityEngine;
public class CheckPoint : MonoBehaviour {

    public CheckPointType pointType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CheckPointManager.InvokeProgressSaver(this);
        }
    }
}
