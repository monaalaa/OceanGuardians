using System.Collections;
using UnityEngine;

public class CollectLitter : MonoBehaviour
{
    public GameObject Target;
    public float Speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Litter"))
        {

            // collect
            Debug.Log("collect..");
            PlayerDataManager.Instance.AddLitter();
            other.GetComponent<SphereCollider>().enabled = false;
            Destroy(other.gameObject);
            //StartCoroutine(Move(other.gameObject));
        }
    }

    IEnumerator Move(GameObject other)
    {
        // move toward basket
        other.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, Target.transform.position, Speed);
        yield return new WaitUntil(() => other.transform.position == Target.transform.position);
        Destroy(other.gameObject);
    }
}
