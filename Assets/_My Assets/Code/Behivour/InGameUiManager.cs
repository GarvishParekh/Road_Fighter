using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Data;

public enum NearMissSide
{
    LEFT,
    RIGHT
}

public class InGameUiManager : MonoBehaviour
{
    AdsManager adsManager;
    UiManager uiManager;
    SFXPlayer sfxPlayer;

    [Header("<size=15>[ SCRIPTABLE OBEJCT ]")]
    [SerializeField] private AdsData adsData;
    [SerializeField] private ScoreData scoreData;

    [Header("<size=15>[ UI ]")]
    [SerializeField] private Button doubleCoinsBtn;
    [SerializeField] private GameObject notAvailablePanel;
    [SerializeField] private TMP_Text coinCounTxt;
    [SerializeField] private TMP_Text timeSpentCountTxt;
    [SerializeField] private GameObject newHighscoreCrossed;

    [Space]
    [SerializeField] private List<GameObject> nearMissCanvasListRight = new List<GameObject>();
    [SerializeField] private List<GameObject> nearMissCanvasListLeft = new List<GameObject>();
    [SerializeField] private List<GameObject> onScreenNearMiss = new List<GameObject>();

    int canvasSpawnCount;

    private void Start()
    {
        uiManager = UiManager.instance;
        adsManager = AdsManager.instance;
        sfxPlayer = SFXPlayer.instance;
        InvokeRepeating(nameof(AdsAvailableCheck), 0, 5);
    }

    private void OnEnable()
    {
        NearMissTrigger.NearMiss += OnNearMiss;
    }

    private void OnDisable()
    {
        NearMissTrigger.NearMiss -= OnNearMiss;
    }

    private void Update()
    {
        UpdateTimeSpent(scoreData.timeSpent);
    }

    private void OnNearMiss(NearMissSide nearMissSide)
    {
        switch (nearMissSide)
        {
            case NearMissSide.LEFT:
                //SpawnNearMissCanvas(nearMissCanvasListLeft[0], nearMissCanvasListLeft);
                OnScreenNearMiss();
                break;
            case NearMissSide.RIGHT:
                //SpawnNearMissCanvas(nearMissCanvasListRight[0], nearMissCanvasListRight);
                OnScreenNearMiss();
                break;
        }
    }

    private void SpawnNearMissCanvas(GameObject canvasToSpawn, List<GameObject> listToRefleftIn)
    {
        canvasToSpawn.transform.SetSiblingIndex(nearMissCanvasListRight.Count); 
        canvasToSpawn.SetActive(true);

        GameObject spawnedCanvas = canvasToSpawn;

        listToRefleftIn.Remove(spawnedCanvas);
        listToRefleftIn.Add(spawnedCanvas);
    }

    private void OnScreenNearMiss()
    {
        onScreenNearMiss[0].transform.SetSiblingIndex(nearMissCanvasListRight.Count);
        onScreenNearMiss[0].SetActive(true);

        GameObject spawnedCanvas = onScreenNearMiss[0];

        onScreenNearMiss.Remove(spawnedCanvas);
        onScreenNearMiss.Add(spawnedCanvas);
    }

    public void UpdateCoinsCountTxt(int _coinsToAdd)
    {
        coinCounTxt.text = _coinsToAdd.ToString("#,##0", CultureInfo.InvariantCulture);
    }

    public void DoubleUpCoins()
    {
        adsManager.ShowRewardedAdDoubleUp();
    }

    public void AdsAvailableCheck()
    {
        switch (adsData.rewardAvailability)
        {
            case RewardAvailability.AVAILABLE:
                doubleCoinsBtn.interactable = true;
                notAvailablePanel.SetActive(false);
                break;
            case RewardAvailability.UNAVAILABLE:
                doubleCoinsBtn.interactable = false;
                notAvailablePanel.SetActive(true);
                break;
        }
    }

    bool turnItOff = false;
    public void Btn_PauseButton()
    {
        GameObject engineSource = sfxPlayer.GetEngineAudoiSource().gameObject;
        if (engineSource.activeInHierarchy)
        {
            engineSource.SetActive(false);
            turnItOff = true;
        }
        uiManager.OpenCanvas(CanvasCellsName.PAUSE_CANVAS);
        Time.timeScale = 0;
    }
    public void Btn_ResumeButton()
    {
        if (turnItOff)
        {
            sfxPlayer.GetEngineAudoiSource().gameObject.SetActive(true);
            sfxPlayer.GetEngineAudoiSource().Play();
        }
        uiManager.OpenCanvas(CanvasCellsName.GAMEPLAY);
        Time.timeScale = 1;
    }
    public void Btn_HomeButton()
    {
        sfxPlayer.gameObject.SetActive(true);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void UpdateTimeSpent(float currentTime)
    {
        timeSpentCountTxt.text = currentTime.ToString("#,##0.<size=25>00", CultureInfo.InvariantCulture);
    }

    public void HighscoreCrossedPopup()
    {
        LeanTween.scaleX(newHighscoreCrossed, 1, 0.25f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scaleX(newHighscoreCrossed, 0, 0.25f).setEaseInOutSine().setDelay(2);
        });
    }
}
