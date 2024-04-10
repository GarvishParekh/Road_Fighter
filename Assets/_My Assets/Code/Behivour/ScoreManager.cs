using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header(" [SCRIPTS] ")]
    [SerializeField] private NosSystem nosSystem;
    
    [Header(" [SCRIPTABLE OBJECT] ")]
    [SerializeField] private ScoreData scoreData;

    [Header (" [USER INTERFACE] ")]
    [SerializeField] private TMP_Text scoreMultiplierCountTxt;
    [SerializeField] private TMP_Text scoreMultiplierTxt;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private Image scoreMutliplierImg;
    [SerializeField] private RectTransform scoreMutliplierMainHolder;

    private void Start()
    {
        ScoreReset();
    }

    private void FixedUpdate()
    {
        scoreData.scoreCount += Time.deltaTime * scoreData.scoreMultiplier[scoreData.currentScoreLevel].value;
        scoreMultiplierCountTxt.text = scoreData.scoreMultiplier[scoreData.currentScoreLevel].levelDisplayName;

        scoreTxt.text = $"SCORE <size=45>{scoreData.scoreCount.ToString("0")}";
    }

    private void Update()
    {
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
        
        if (scoreData.scoreMultiplierValue >= 1 && scoreData.currentScoreLevel < scoreData.scoreMultiplier.Length - 1) 
        {
            scoreData.scoreMultiplierValue = 0;
            scoreData.currentScoreLevel++;
            LeanTween.scale(scoreMultiplierCountTxt.gameObject, Vector3.one * 2, 0.15f).setEaseInOutSine().setLoopPingPong(1);
        }
       
        if (scoreData.scoreMultiplierValue >= 1 && scoreData.currentScoreLevel == scoreData.scoreMultiplier.Length - 1)
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
}
