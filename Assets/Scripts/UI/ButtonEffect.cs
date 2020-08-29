using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ButtonScale(Vector3.one * 0.5f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(ButtonScale(Vector3.one));
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(ButtonScale(Vector3.zero));
    }

    private IEnumerator ButtonScale(Vector3 target)
    {
        float distance = Mathf.Abs(target.x - transform.localScale.x);
        float speed = distance / 0.125f;

        while (transform.localScale != target)
        {
            yield return new WaitForFixedUpdate();
            transform.localScale = Vector3.MoveTowards(transform.localScale, target, speed * Time.deltaTime);
        }
    }
}