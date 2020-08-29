using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraZoom : MonoBehaviour
{
    [Header("Zoom Variables")]

    [SerializeField]
    private float zoomOffset;

    [SerializeField]
    private float zoomTime;

    public void StartZoom(Transform zoomTarget)
    {
        StartCoroutine(Zoom(zoomTarget));
        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, Resources.Load<AudioClip>("Sounds/LevelSelectButton"));
    }

    private IEnumerator Zoom(Transform zoomTarget)
    {
        Vector3 targetPosition = zoomTarget.position - new Vector3(0, 0, zoomOffset);

        float distance = Vector3.Distance(transform.position, targetPosition);

        while (transform.position.z != targetPosition.z)
        {
            yield return new WaitForFixedUpdate();

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (distance / zoomTime) * Time.deltaTime);
        }
    }

}
