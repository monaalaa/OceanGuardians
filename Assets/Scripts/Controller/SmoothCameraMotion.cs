using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMotion : MonoBehaviour {
    public GameObject player;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 targetCamPos = player.transform.position + offset;
        if (player.transform.position.y > 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(targetCamPos.x, transform.position.y, transform.position.z), 3f * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, targetCamPos, 3f * Time.deltaTime);
        }

    }
}
