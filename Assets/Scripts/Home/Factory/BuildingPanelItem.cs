using UnityEngine;

public class BuildingPanelItem : MonoBehaviour
{
    public GameObject FactoryPrefab;
    public void OnClick()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        var audioClip = Resources.Load<AudioClip>("Audio/BuildingClick");
        HomeUIManager.Instance.ShowBuildingsPanel(false);
        // play sound
        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioClip, 1);
        Instantiate(FactoryPrefab, pos, Quaternion.identity);
    }
}

