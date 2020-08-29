using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField]
    private float distanceV, distanceH, timeV, timeH;

    private float startV, endV, startH, endH;

    private Vector3 pos;

    private void Start()
    {
        initSlide();
    }

    private void initSlide()
    {
        startV = transform.position.y + (distanceV / 2);
        endV = transform.position.y - (distanceV / 2);

        startH = transform.position.x + (distanceH / 2);
        endH = transform.position.x - (distanceH / 2);

        StartCoroutine(Slide());
    }

    private IEnumerator Slide()
    {
        yield return null;
        float targetV = startV;
        float targetH = startH;

        float speedV = Mathf.Abs(startV - endV) / timeV;
        float speedH = Mathf.Abs(startH - endH) / timeH;

        float xPos, yPos;

        while (true)
        {
            yield return new WaitForFixedUpdate();

            float deltaTime = Time.deltaTime;

            xPos = Mathf.MoveTowards(transform.position.x, targetH, speedH * deltaTime);
            yPos = Mathf.MoveTowards(transform.position.y, targetV, speedV * deltaTime);

            transform.position = new Vector3(xPos, yPos, transform.position.z);

            if (System.Math.Round(transform.position.x, 2) == System.Math.Round(targetH, 2))
            {
                if (targetH == startH)
                    targetH = endH;
                else
                    targetH = startH;
            }

            if (System.Math.Round(transform.position.y, 2) == System.Math.Round(targetV, 2))
            {
                if (targetV == startV)
                    targetV = endV;
                else
                    targetV = startV;
            }
        }
    }
}
