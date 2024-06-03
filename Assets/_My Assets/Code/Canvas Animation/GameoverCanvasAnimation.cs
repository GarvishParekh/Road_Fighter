using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;

public class GameoverCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [Header(" <size=15> [CAMERA CAPTURE] ")]
    [SerializeField] private Camera ssCamera;
    [SerializeField] private RawImage ssImage;

    [Header (" <size=15> [SCRIPTABLE OBJECT] ")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private CarData carData;

    [Header(" <size=15> [SCRIPTS] ")]
    [SerializeField] private InGameCoinCollection inGameCoinCollection;
    
    [Header (" <size=15> [GAME OVER CANVAS] ")]
    [SerializeField] private TMP_Text finalScoreTxt;
    [SerializeField] private TMP_Text finalCoinTxt;

    [Header (" <size=15> [CANVAS ANIMATION] ")]
    [SerializeField] GameObject bgImage;
    [SerializeField] GameObject mainHolder;
    [SerializeField] GameObject crashTittle;
    [SerializeField] GameObject scoreHolder;
    [SerializeField] GameObject coinHolder;
    [SerializeField] GameObject photoHolder;
    [SerializeField] GameObject homeButtom;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject retyrIcon;
    [SerializeField] GameObject doubleCoinsButton;
    [SerializeField] GameObject doubleCoinsHolder;

    WaitForSeconds pointOne = new WaitForSeconds(0.01f);
    float scoreCounter = 0;
    float coinCounter = 0;
    Vector3 defaultTransform = new Vector3 (0,1,1);
    
    public void PlayAnimation()
    {
        StartCoroutine(nameof(GameoverFlow));
    }

    public void ResetAnimation()
    {
        bgImage.transform.localScale = Vector3.zero;
        mainHolder.transform.localScale = Vector3.zero;
        retyrIcon.transform.rotation = Quaternion.Euler(Vector3.zero);

        crashTittle.transform.localScale = defaultTransform;
        scoreHolder.transform.localScale = defaultTransform;
        coinHolder.transform.localScale = defaultTransform;
        photoHolder.transform.localScale = defaultTransform;
        LeanTween.rotate(photoHolder, defaultTransform, 0);
        homeButtom.transform.localScale = defaultTransform;
        doubleCoinsButton.transform.localScale = Vector3.zero;
        retryButton.transform.localScale = defaultTransform;
    }

    private IEnumerator GameoverFlow()
    {
        ResetAnimation();
        StartCoroutine(nameof(CalculateScoreCounter));
        StartCoroutine(nameof(CalculateCoinCounter));
        LeanTween.scale(bgImage, Vector3.one * 10, 0.2f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(mainHolder, Vector3.one, 0.15f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.scale(crashTittle, Vector3.one, 0.2f).setEaseInOutSine();
                LeanTween.scale(scoreHolder, Vector3.one, 0.2f).setEaseInOutSine();
                LeanTween.scale(coinHolder, Vector3.one, 0.2f).setEaseInOutSine();
            });
        });

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator CalculateScoreCounter()
    {
        while (scoreCounter != scoreData.scoreCount)
        {
            scoreCounter = Mathf.MoveTowards(scoreCounter, scoreData.scoreCount, scoreData.scoreCount / 2.6f * Time.deltaTime);
            finalScoreTxt.text = scoreCounter.ToString("#,##0", CultureInfo.InvariantCulture);
            yield return null;

            Debug.Log("Calculating score");
        }
        
        Debug.Log("Calculation completed");
        CaptureSS(); 
        yield return new WaitForSeconds(0.5f);

        LeanTween.scale(photoHolder, Vector3.one, 0.5f);
        LeanTween.rotateAround(photoHolder, Vector3.forward, 1808, 0.5f).setOnComplete(() =>
        {
            LeanTween.scale(retryButton, Vector3.one, 0.2f).setEaseInOutSine().setOnComplete(()=>
            {

                LeanTween.scale(doubleCoinsButton, Vector3.one, 0.2f).setEaseInOutSine().setOnComplete(() =>
                { 
                    LeanTween.scale(homeButtom, Vector3.one, 0.2f).setEaseInOutSine();
                    LeanTween.rotateAround(retyrIcon, Vector3.forward, 360, 2f).setEaseOutBounce().setLoopCount(-1);
                    LeanTween.scale(doubleCoinsButton, Vector3.one * 1.2f, 0.2f).setEaseInOutSine().setLoopPingPong(-1);
                });
            });
        });
    }

    private IEnumerator CalculateCoinCounter()
    {
        int coinsCollected = inGameCoinCollection.GetCollectedCoins();
        float collectionSpeed = coinsCollected / 1.6f * Time.deltaTime;
        
        if (coinsCollected > 1000)
        {
            doubleCoinsHolder.SetActive(true);
        }
        else
        {
            doubleCoinsHolder.SetActive(false);
        }

        while (coinCounter != coinsCollected)
        {
            coinCounter = Mathf.MoveTowards(coinCounter, coinsCollected, collectionSpeed);
            finalCoinTxt.text = coinCounter.ToString("#,##0", CultureInfo.InvariantCulture);
            yield return null;

            Debug.Log("Calculating coins");
        }
        Debug.Log("Calculation completed");
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
