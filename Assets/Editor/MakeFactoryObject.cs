using UnityEngine;
using UnityEditor;

public class MakeFactoryObject
{
    [MenuItem("Assets/Create/Make Factory Object")]
    public static void CreateWindow()
    {
        FactoryObject asset = ScriptableObject.CreateInstance<FactoryObject>();
        AssetDatabase.CreateAsset(asset, "Assets/Scriptable Objects/Factory/NewFactory.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
