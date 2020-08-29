using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    Camera mainCamera;
    public float Speed;
    public float MinFOV = 20;
    public float MaxFOV = 60;
    public ZoomType ZoomType;
    public Transform LookAt;
    public float prevFOV;
    public Vector3 prevLookAt;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        prevFOV = mainCamera.fieldOfView;
        prevLookAt = mainCamera.transform.position + mainCamera.transform.forward * mainCamera.transform.position.z;
    }

    void Update()
    {
        LookAtObjectToZoomTo(LookAt);
        ZoomInOut();
    }

    private void LookAtObjectToZoomTo(Transform lookAt)
    {
        mainCamera.transform.LookAt(lookAt);
    }

    private void ZoomInOut()
    {
        if (ZoomType == ZoomType.In)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, MinFOV, Speed * Time.deltaTime);
        }
        else if (ZoomType == ZoomType.Out)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, MaxFOV, Speed * Time.deltaTime);
        }
    }
}
