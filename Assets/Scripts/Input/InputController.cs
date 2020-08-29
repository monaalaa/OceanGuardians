using UnityEngine;

public class InputController : MonoBehaviour
{
    #region Vars
    bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, tapped;
    Vector2 startTouch, swipeDelta;
    bool isDragging;

    bool mobileEnabled = false;
    #endregion

    #region Methods
    void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = tapped = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (GameManager.Instance.Tapped != null)
                GameManager.Instance.Tapped.Invoke();
        }
        #endregion
#elif UNITY_IOS || UNITY_ANDROID
        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = false;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (GameManager.Instance.Tapped != null)
                GameManager.Instance.Tapped.Invoke();
        }
        #endregion
#endif

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
#elif UNITY_IOS || UNITY_ANDROID
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
#endif
        }

        if (swipeDelta.magnitude > 125)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > 0)
                {
                    if (GameManager.Instance.SwipedLeft != null)
                        GameManager.Instance.SwipedLeft.Invoke();
                }
                else if (x < 0)
                {
                    if (GameManager.Instance.SwipedRight != null)
                        GameManager.Instance.SwipedRight.Invoke();
                }
            }

            Reset();
        }
    }
   
    private void Reset()
    {
        isDragging = false;
        tap = false;
        startTouch = swipeDelta = Vector2.zero;
    }
    #endregion
}