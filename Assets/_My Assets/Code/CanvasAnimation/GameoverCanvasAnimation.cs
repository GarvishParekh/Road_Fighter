using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameoverCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [Header(" <size=15> [CAMERA CAPTURE] ")]
    [SerializeField] private Camera ssCamera;
    [SerializeField] private RawImage ssImage;

    [Header (" <size=15> [SCRIPTABLE OBJECT] ")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private CarData carData;
    
    [Header (" <size=15> [GAME OVER CANVAS] ")]
    [SerializeField] private Slider finalScoreSlider;
    [SerializeField] private TMP_Text finalScoreTxt;
    [SerializeField] private TMP_Text scoreLevelTxt;

    [Header (" <size=15> [CANVAS ANIMATION] ")]
    [SerializeField] GameObject bgImage;
    [SerializeField] GameObject crashTittle;
    [SerializeField] GameObject scoreHolder;
    [SerializeField] GameObject photoHolder;
    [SerializeField] GameObject homeButtom;
    [SerializeField] GameObject retryButton;

    WaitForSeconds pointOne = new WaitForSeconds(0.01f);
    float counter = 0;
    
    public void PlayAnimation()
    {
        StartCoroutine(nameof(GameoverFlow));
    }

    public void ResetAnimation()
    {
        bgImage.transform.localScale = Vector3.zero;
        crashTittle.transform.localScale = Vector3.zero;
        scoreHolder.transform.localScale = Vector3.zero;
        LeanTween.moveLocalY(photoHolder, -1000f, 0);
        homeButtom.transform.localScale = Vector3.zero;
        retryButton.transform.localScale = Vector3.zero;
    }

    private IEnumerator GameoverFlow()
    {
        ResetAnimation();
        StartCoroutine(nameof(CalculateScoreCounter));
        LeanTween.scale(bgImage, Vector3.one * 10, 0.2f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(crashTittle, Vector3.one, 0.2f).setEaseInOutSine();
            LeanTween.scale(scoreHolder, Vector3.one, 0.2f).setEaseInOutSine();
            LeanTween.scale(homeButtom, Vector3.one, 0.2f).setEaseInOutSine();
            LeanTween.scale(retryButton, Vector3.one, 0.2f).setEaseInOutSine();
        });

        yield return new WaitForSeconds(1f);
        CaptureSS();
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveLocalY(photoHolder, 0, 1.5f).setEaseOutElastic();


    }

    private IEnumerator CalculateScoreCounter()
    {
        int speedLevel = (int)carData.currentSpeedLevel + 1;
        scoreLevelTxt.text = speedLevel.ToString("0");

        float maxScore = carData.carSpeedLevel[(int)carData.currentSpeedLevel].maxValue;
        finalScoreSlider.maxValue = maxScore;   

        while (counter <= scoreData.scoreCount)
        {
            counter = Mathf.MoveTowards(counter, scoreData.scoreCount, scoreData.scoreCount / 3 * Time.deltaTime);
            finalScoreTxt.text = counter.ToString("0");
            finalScoreSlider.value = counter;
            yield return null;
        }
    }

    private void CaptureSS()
    {
        ssCamera.gameObject.SetActive(true);
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = ssCamera.targetTexture;

        ssCamera.Render();

        Texture2D image = new Texture2D(ssCamera.targetTexture.width, ssCamera.targetTexture.height, TextureFormat.RGBAFloat, false);
        image.ReadPixels(new Rect(0, 0, ssCamera.targetTexture.width, ssCamera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();

        ssImage.texture = image;
        //Destroy(image);
        ssCamera.gameObject.SetActive(false);
    }
}
