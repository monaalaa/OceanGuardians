using UnityEngine;
using UnityEditor;

public class AddPlayerSkinWindow : EditorWindow
{
    public Texture2D SkinTexture;
    public int TextureCost;

    [MenuItem("Window/Skin")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AddPlayerSkinWindow));
    }

    private void OnGUI()
    {
        GUILayout.Label("Skin Texture", EditorStyles.boldLabel);
        SkinTexture = (Texture2D)EditorGUILayout.ObjectField("Texture", SkinTexture, typeof(Texture2D), false);
        TextureCost = EditorGUILayout.IntField("Cost", TextureCost);
        SkinData skinData = null;
        bool buttonClicked = GUILayout.Button("Save Skin");
        if (SkinTexture != null && TextureCost >= 0)
        {
            skinData = new SkinData(SkinTexture.name, SkinTexture, TextureCost);
            if (buttonClicked)
            {
                FirebaseManager.Instance.SaveSkinData(skinData);
            }
        }
    }
}
