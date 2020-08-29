using UnityEngine;

public enum ZoomType
{
    In,
    Out
}

public class Utilities
{

    private static Camera camera = Camera.main;
    /// <summary>
    /// Enable zooming script on camera.
    /// </summary>
    /// <param name="zoomType"> Zoom in or out </param>
    public static void ZoomCamera(ZoomType zoomType, Transform clickableObject)
    {
        if (camera.GetComponent<Zoom>() == null)
        {
            camera.gameObject.AddComponent<Zoom>().ZoomType = zoomType;
            camera.gameObject.GetComponent<Zoom>().LookAt = clickableObject;
        }
        else
        {
            var zoom = camera.GetComponent<Zoom>();
            zoom.ZoomType = zoomType;
            zoom.LookAt = clickableObject;
            zoom.enabled = true;
        }
    }

    /// <summary>
    /// Reset camera FOV and LookAt vector.
    /// </summary>
    public static void ResetCameraState()
    {
        var zoom = camera.gameObject.GetComponent<Zoom>();
        camera.fieldOfView = zoom.prevFOV;
        camera.transform.LookAt(zoom.prevLookAt);
    }

    internal static void BlurScreen(bool blur)
    {
        // get blur plane which is the 1st child of camera and activate it.
        camera.gameObject.transform.GetChild(0).gameObject.SetActive(blur);
    }
}
