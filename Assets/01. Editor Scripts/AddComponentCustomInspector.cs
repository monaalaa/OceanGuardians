using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneEntity))]
public class AddComponentCustomInspector : Editor
{
    SceneEntity addComponentToObject;

    private void OnEnable()
    {
        addComponentToObject = (SceneEntity)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Available Components");
        addComponentToObject.ComponentList = (ComponentsEnum)EditorGUILayout.EnumPopup(addComponentToObject.ComponentList);
    }
}
