using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelItem))]
public class EnterLevelEditor : Editor
{
    int levelIndex;
    string[] levels;
    private void OnEnable()
    {
        int levelsCount = SceneManager.sceneCountInBuildSettings;
        levels = new string[levelsCount];
        for (int i = 0; i < levelsCount; i++)
        {
            levels[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }
    }
    public override void OnInspectorGUI()
    {
        var enterLevel = target as LevelItem;
        levelIndex = EditorGUILayout.Popup("Level Name: ", levelIndex, levels);
        enterLevel.LevelName = levels[levelIndex];
    }
}
