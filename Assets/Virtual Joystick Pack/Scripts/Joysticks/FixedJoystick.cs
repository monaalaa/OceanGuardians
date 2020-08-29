using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    [Header("Fixed Joystick")]
    Vector2 joystickPosition = Vector2.zero;

  
    private Camera cam = new Camera();
    Vector2 direction;
    WaitForEndOfFrame wait = new WaitForEndOfFrame();

    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);

        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
       // PlayerController.Instance.MovePlayer(direction.normalized, inputVector);

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // OnDrag(eventData);
        //background.gameObject.SetActive(true);
        //background.position = eventData.position;
        //handle.anchoredPosition = Vector2.zero;
        // joystickPosition = eventData.position;
        InvokeRepeating("InvokeOnInputPress", 0, 0.001f);
        //PlayerController.Instance.MovePlayer(direction.normalized, inputVector);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke();
        //background.gameObject.SetActive(false);

        PlayerManager.Instance.InvokeOnPlayerStopMoving();
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    void InvokeOnInputPress()
    {
        GameManager.Instance.InvokeOnInputPressed(direction.normalized, inputVector);
    }
}