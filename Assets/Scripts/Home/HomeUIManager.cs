using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class HomeUIManager : MonoBehaviour
{
    public GameObject BuildingsPanel;
    public GameObject ConfirmationMsgToEnterLevel;
    public GameObject FactoryPanel;
    public GameObject UpgradePanel;
    public GameObject InfoPanel;
    public GameObject HomeCanvas;
    public GameObject MoveImage;
    public Image FillMoveImage;
    public Button UpgradeButton;
    public TextMeshProUGUI PearlScoreText;
    public TextMeshProUGUI GoldScoreText;
    public TextMeshProUGUI FishScoreText;
    public TextMeshProUGUI RecyclesScoreText;
    public TextMeshProUGUI RecyclesBlockScoreText;
    public Button YesConfirmationMsg;
    public Button NoConfirmationMsg;
    public GameObject StoreCanvas;
    public static HomeUIManager Instance;
    private WaitForSeconds waitTimeToCalculateScore = new WaitForSeconds(0.002f);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        GetScores();
        DataManager.Instance.ScoreUpdated += UpdateScore;
        HomeManager.Instance.FactoryUpgraded += PopulateFactoryDataInInfoPanel;
    }

    private void GetScores()
    {
        UpdateScore(DataManager.Instance.GoldScore, CollectableType.Gold);
        UpdateScore(DataManager.Instance.RecycleScore, CollectableType.Recycle);
        UpdateScore(DataManager.Instance.RecycleBlockScore, CollectableType.RecycleBlock);
        UpdateScore(DataManager.Instance.FishScore, CollectableType.Fish);
        UpdateScore(DataManager.Instance.PearlScore, CollectableType.Pearl);
    }

    #region Level
    private void OnLevelEntered(string levelName, Transform clickableObject)
    {
        // show confirmation msg
        ConfirmationMsgToEnterLevel.SetActive(true);
        YesConfirmationMsg.onClick.AddListener(delegate
        {
            Utilities.ZoomCamera(ZoomType.In, clickableObject);
            SceneController.Instance.LoadScene(levelName, 4);
            ConfirmationMsgToEnterLevel.SetActive(false);
        });
        NoConfirmationMsg.onClick.AddListener(delegate
        {
            ConfirmationMsgToEnterLevel.SetActive(false);
        });
    }
    #endregion

    #region Store
    private void OnShowStore()
    {
        StoreCanvas.SetActive(true);
        //UIManager.Instance.ScreenOverlay = ScreenOverlay.Overlay;
    }

    public void ShowChangeSkinMainPanel()
    {
        ShowMainPanelOfThisPanel(StoreCanvas.transform.GetChild(0), 1);
    }

    public void ShowUpgradeMainPanel()
    {
        ShowMainPanelOfThisPanel(StoreCanvas.transform.GetChild(1), 1);
    }

    public void ShowBuyMainPanel()
    {
        ShowMainPanelOfThisPanel(StoreCanvas.transform.GetChild(2), 1);
    }

    private void ShowMainPanelOfThisPanel(Transform panel, int childIndex)
    {
        // hide all unused panels
        HideChildrenOfThisPanel(StoreCanvas.transform);
        // show the wanted panel
        panel.gameObject.SetActive(true);
        // hide all children of the wanted panel
        HideChildrenOfThisPanel(panel);
        // show only the wanted child
        panel.GetChild(childIndex).gameObject.SetActive(true);
        //UIManager.Instance.ScreenOverlay = ScreenOverlay.Overlay;
    }

    private static void HideChildrenOfThisPanel(Transform panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            panel.GetChild(i).gameObject.SetActive(false);
        }
    }
    #endregion

    #region Factory
    public void ShowFactoryPanel(bool show)
    {
        FactoryPanel.SetActive(show);
    }

    public void PopulateFactoryData(FactoryManager factoryManager)
    {
        PopulateFactoryDataInUpgradePanel(factoryManager);
        PopulateFactoryDataInInfoPanel(factoryManager);
    }

    private void PopulateFactoryDataInInfoPanel(FactoryManager factoryManager)
    {
        // add image
        InfoPanel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = factoryManager.CurrentFactory.FactorySprite;
        InfoPanel.transform.GetChild(0).gameObject.GetComponent<Image>().preserveAspect = true;
        // add description
        InfoPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = factoryManager.Description;
    }

    private void PopulateFactoryDataInUpgradePanel(FactoryManager factoryManager)
    {
        if (factoryManager.CurrentFactoryLevel + 1 <= factoryManager.FactoriesUpgrades.Count - 1)
        {
            var nextFactoryUpgrade = factoryManager.FactoriesUpgrades[factoryManager.CurrentFactoryLevel + 1];
            // add sprite
            UpgradePanel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = nextFactoryUpgrade.FactorySprite;
            UpgradePanel.transform.GetChild(0).gameObject.GetComponent<Image>().preserveAspect = true;
            // add description 
            UpgradePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = factoryManager.GetDescriptionOfFactory(factoryManager.CurrentFactoryLevel + 1);
            // add cost
            UpgradePanel.transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = nextFactoryUpgrade.Cost.ToString();

            if (DataManager.Instance.GoldScore >= factoryManager.FactoriesUpgrades[factoryManager.CurrentFactoryLevel + 1].Cost)
            {
                UpgradeButton.interactable = true;
            }
            else
            {
                UpgradeButton.interactable = false;
            }
            UpgradeButton.onClick.AddListener(() =>
            {
                UpgradeButton.onClick.RemoveAllListeners();
                ShowFactoryPanel(false);
                factoryManager.UpgradeFactory();
            });
        }
        else
        {
            // add sprite
            UpgradePanel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
            // add description 
            UpgradePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = null;
            // add cost
            UpgradePanel.transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = null;
        }

    }

    public void ShowBuildingsPanel(bool show)
    {
        if (show)
        {
            if (!BuildingsPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Entrance"))
            {
                ExitBuildingPanel(false);
            }
            else
            {
                ExitBuildingPanel(true);
            }
        }
        else
        {
            if (!BuildingsPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Exit"))
            {
                ExitBuildingPanel(true);
            }
        }
    }

    private void ExitBuildingPanel(bool exit)
    {
        // play audio 
        var audioClip = Resources.Load<AudioClip>("Audio/Click1");
        // play sound
        SoundManager.Instance.PlayClip(SoundManager.Instance.efxSource, audioClip, 1);
        if (exit)
        {
            BuildingsPanel.GetComponent<Animator>().SetTrigger("Exit");
        }
        else
        {
            BuildingsPanel.GetComponent<Animator>().SetTrigger("Enter");
        }

    }

    public void ShowUpgradePanel(bool show)
    {
        UpgradePanel.SetActive(show);
        InfoPanel.SetActive(!show);
    }

    #endregion

    private void UpdateScore(int score, CollectableType collectableType)
    {
        switch (collectableType)
        {
            case CollectableType.Gold:
                StartCoroutine(CountScore(score, GoldScoreText));
                break;
            case CollectableType.Fish:
                StartCoroutine(CountScore(score, FishScoreText));
                break;
            case CollectableType.Recycle:
                StartCoroutine(CountScore(score, RecyclesScoreText));
                break;
            case CollectableType.RecycleBlock:
                StartCoroutine(CountScore(score, RecyclesBlockScoreText));
                break;
            case CollectableType.Pearl:
                StartCoroutine(CountScore(score, PearlScoreText));

                break;
            default:
                break;
        }
    }

    IEnumerator CountScore(int score, TextMeshProUGUI scoreText)
    {
        int currScore = int.Parse(scoreText.text);
        var scoreValueDiff = 0;
        while (scoreValueDiff < Mathf.Abs(score))
        {
            scoreValueDiff++;
            currScore = int.Parse(scoreText.text);
            currScore += (int)(Mathf.Sign(score));
            if (currScore <= 0)
            {
                currScore = 0;
                scoreText.text = currScore.ToString();
                break;
            }
            scoreText.text = currScore.ToString();
            yield return waitTimeToCalculateScore;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().name.Equals("Home"))
        {
            HomeManager.Instance.LevelEntered += OnLevelEntered;
            HomeManager.Instance.StoreClicked += OnShowStore;
        }
    }

    public void BackToMainMenu()
    {
        HideHomeCanvas();
        DataManager.Instance.SaveHomeItem();
        SceneController.Instance.LoadScene("MainMenu");
    }

    private void HideHomeCanvas()
    {
        HomeCanvas.SetActive(false);
    }

    public void UpdateFillMoveImg(float num)
    {
        FillMoveImage.fillAmount = num;
    }

    public void ShowFillMoveImg(bool show, Vector3 touchPos = default(Vector3))
    {
        MoveImage.transform.position = touchPos;
        MoveImage.SetActive(show);
    }
    private void OnDisable()
    {
        DataManager.Instance.ScoreUpdated -= UpdateScore;
    }
}
