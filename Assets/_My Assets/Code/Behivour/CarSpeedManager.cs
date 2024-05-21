using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarSpeedManager : MonoBehaviour
{
    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private CarData carData;

    [Header("<size=15>[UI]")]
    [SerializeField] private GameObject speedLevelBarHolder;
    [SerializeField] private Image speedLevelBar;
    [SerializeField] private TMP_Text speedLevelNameTxt;
    [SerializeField] private ParticleSystem boostingUpParticles;

    private void Start()
    {
        StartCoroutine (nameof(SpeedCalculation));  
    }

    private void ReserScoreValues()
    {
        scoreData.scoreCount = 0;
        carData.currentSpeedLevel = 0;
        speedLevelNameTxt.text = carData.carSpeedLevel[0].levelName;
    }

    private IEnumerator SpeedCalculation()
    {
        ReserScoreValues();
        
        while ((int)carData.currentSpeedLevel < carData.carSpeedLevel.Length - 1)
        {
            if (scoreData.scoreCount <= carData.carSpeedLevel[(int)carData.currentSpeedLevel].maxValue)
            {
                // calculate and update speedLevel bar value 
                carData.speedLevelValue = BinaryConverter
                (
                    scoreData.scoreCount,
                    carData.carSpeedLevel[(int)carData.currentSpeedLevel].minValue,
                    carData.carSpeedLevel[(int)carData.currentSpeedLevel].maxValue
                );
                speedLevelBar.fillAmount = carData.speedLevelValue;
            }
            else
            {
                // increase the speedLevel level and update in UI
                carData.currentSpeedLevel += 1;
                speedLevelNameTxt.text = carData.carSpeedLevel[(int)carData.currentSpeedLevel].levelName;
                
                // speeding up animation
                LeanTween.scale(speedLevelNameTxt.gameObject, Vector3.one * 2, 0.15f).setEaseInOutSine().setLoopPingPong(1);
                boostingUpParticles.Play();
                Actions.SpeedLevelIncreased?.Invoke();
            }
            yield return null;  
        }
        
        // final animation
        LeanTween.scaleY(speedLevelBarHolder, 0.2f, 0.5f).setEaseInOutSine();
        LeanTween.scale(speedLevelNameTxt.gameObject, Vector3.one * 1.8f, 0.5f).setEaseInOutSine();
    }

    private float BinaryConverter(float originalValue, float minValue, float maxValue)
    {
        return (originalValue - minValue) / (maxValue - minValue);
    }
}
