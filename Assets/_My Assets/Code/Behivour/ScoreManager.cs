using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class ScoreManager : MonoBehaviour
{
    GameStatus gameStatus;
    [Header(" [SCRIPTS] ")]
    [SerializeField] private NosSystem nosSystem;
    
    [Header(" [SCRIPTABLE OBJECT] ")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private UpgradesData upgradeData;
    Upgrades scoreUpgrade;
    float maxScoreLevel;

    [Header (" [USER INTERFACE] ")]
    [SerializeField] private TMP_Text scoreMultiplierCountTxt;
    [SerializeField] private TMP_Text scoreMultiplierTxt;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private Image scoreMutliplierImg;
    [SerializeField] private RectTransform scoreMutliplierMainHolder;

    private void OnEnable()
    {
        GameStatus.GameOverAction += NoteScore;
    }

    private void OnDisable()
    {
        GameStatus.GameOverAction -= NoteScore;
    }

    private void Start()
    {
        ScoreReset();
        gameStatus = GameStatus.instance;
        SetMaxScoreLevel();
    }

    private void SetMaxScoreLevel()
    {
        scoreUpgrade = upgradeData.upgrades[0];
        maxScoreLevel = scoreUpgrade.levelValues[scoreUpgrade.upgradeLevel];
    }

    private void FixedUpdate()
    {
        if (gameStatus.GetGameState() == GameState.GAMEOVER)
        {
            return;
        }
        scoreData.scoreCount += Time.deltaTime * scoreData.scoreMultiplier[scoreData.currentScoreLevel].value;
        scoreMultiplierCountTxt.text = scoreData.scoreMultiplier[scoreData.currentScoreLevel].levelDisplayName;

        scoreTxt.text = $"SCORE <size=45>{scoreData.scoreCount.ToString("#,##0", CultureInfo.InvariantCulture)}";
    }

    private void Update()
    {
        if (gameStatus.GetGameState() == GameState.GAMEOVER)
        {
            return;
        }
        switch (nosSystem.boostingStatus)
        {
            case BoostingStatus.IsBoosting:
                InscreaseMutliplier();
                scoreMultiplierTxt.color = scoreData.scoreMultiplier[scoreData.currentScoreLevel].textColor;
                break;
            case BoostingStatus.NotBoosting:
                ResetMultiplier();
                scoreMultiplierTxt.color = scoreData.textDefaultColor;
                break;
        }
        scoreMutliplierImg.fillAmount = scoreData.scoreMultiplierValue;
        scoreMutliplierImg.color = scoreData.scoreMultiplier[scoreData.currentScoreLevel].barColor;
    }

    private void InscreaseMutliplier()
    {
        scoreData.scoreMultiplierValue += scoreData.increasingValue * Time.deltaTime;
        
        if (scoreData.scoreMultiplierValue >= 1 && scoreData.currentScoreLevel < maxScoreLevel)
        {
            scoreData.scoreMultiplierValue = 0;
            scoreData.currentScoreLevel++;
            LeanTween.scale(scoreMultiplierCountTxt.gameObject, Vector3.one * 2, 0.15f).setEaseInOutSine().setLoopPingPong(1);
        }
       
        if (scoreData.scoreMultiplierValue >= 1 && scoreData.currentScoreLevel == maxScoreLevel)
            scoreMutliplierMainHolder.transform.localPosition = (Random.insideUnitCircle * scoreData.shakeEffect) * Time.deltaTime;
        else
            scoreMutliplierMainHolder.transform.localPosition = Vector3.zero;
    }

    private void ResetMultiplier()
    {
        scoreData.currentScoreLevel = 0;
        scoreData.scoreMultiplierValue = 0;
    }

    private void ScoreReset()
    {
        scoreData.currentScoreLevel = 0;
        scoreData.scoreMultiplierValue = 0;
        scoreData.scoreCount = 0;
    }

    private void NoteScore()
    {
        // note score
        int currentHighscore = PlayerPrefs.GetInt(ConstantKeys.HIGHSCORE_COUNT, 0);
        int currentScore = (int)scoreData.scoreCount;

        // update the highscore
        if (currentScore > currentHighscore)
        {
            PlayerPrefs.SetInt(ConstantKeys.HIGHSCORE_COUNT, currentScore);
        }
    }
}
