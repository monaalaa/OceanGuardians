using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionControllerBTN : MonoBehaviour {

    Button myButton;
    void Start()
    {
        myButton = GetComponent<Button>();
        PlayerManager.Instance.OnActionCreated += ChangeAction;
    }

    void ChangeAction(string actionName, GameObject ObjectToApplyActionOn = null)
    {
        //Change Image Depand On Action Name
        myButton.image.sprite = (Sprite)Resources.Load("UI/ActionButton/" + actionName, typeof(Sprite));
        myButton.GetComponentInChildren<Text>().text = actionName;
        //Remove Old listener and instantiate new one with action name
        myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(() => PlayerController.Instance.LocalPlayerInstance.PreformAction(actionName));

        //create action based on it's type
        Type type = Type.GetType(actionName);
        PlayerActions action = Activator.CreateInstance(type) as PlayerActions;
        action.ObjectToPreformActionOn = ObjectToApplyActionOn;

        //add created action to action List
        PlayerController.Instance.LocalPlayerInstance.Actions.Add(action);
    }
}
