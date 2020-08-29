using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CreateUIDynamicaly : MonoBehaviour
{
    public static CreateUIDynamicaly Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void InstantiateTextAtRunTime(string text, Transform parent, string textToInstantiatePath)
    {
        Text tempText = (Text)GameObject.Instantiate(Resources.Load(textToInstantiatePath, typeof(Text)));
        tempText.transform.SetParent(parent);
        tempText.text = text;
    }

    public void InstantiateButton(Transform parent, string BTNPrefbPath, GameComponentsUI objectlistener, string type, string BTNName, string FunctionName)
    {
       // Type myType = (typeof(TransportationUI));
        Type myType = objectlistener.GetType();
        MethodInfo myMethodInfo = myType.GetMethod(FunctionName);

        Button tempBTN = (Button)GameObject.Instantiate(Resources.Load(BTNPrefbPath, typeof(Button)));
        tempBTN.transform.SetParent(parent);
        tempBTN.GetComponent<Button>().onClick.AddListener(() => myMethodInfo.Invoke(objectlistener, new object[] { type }));
        tempBTN.GetComponentInChildren<Text>().text = BTNName;
    }
}
