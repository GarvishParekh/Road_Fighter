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
    [SerializeField] private TMP_Text finalScoreTxt;

    [Header (" <size=15> [CANVAS ANIMATION] ")]
    [SerializeField] GameObject bgImage;
    [SerializeField] GameObject crashTittle;
    [SerializeField] GameObject scoreHolder;
    [SerializeField] GameObject coinHolder;
    [SerializeField] GameObject photoHolder;
    [SerializeField] GameObject homeButtom;
    [SerializeField] GameObject retryButton;

    WaitForSeconds pointOne = new WaitForSeconds(0.01f);
    float counter = 0;
    Vector3 defaultTransform = new Vector3 (0,1,1);
    
    public void PlayAnimation()
    {
        StartCoroutine(nameof(GameoverFlow));
    }

    public void ResetAnimation()
    {
        bgImage.transform.localScale = defaultTransform;
        crashTittle.transform.localScale = defaultTransform;
        scoreHolder.transform.localScale = defaultTransform;
        coinHolder.transform.localScale = defaultTransform;
        photoHolder.transform.localScale = defaultTransform;
        LeanTween.rotate(photoHolder, defaultTransform, 0);
        homeButtom.transform.localScale = defaultTransform;
        retryButton.transform.localScale = defaultTransform;
    }

    private IEnumerator GameoverFlow()
    {
        ResetAnimation();
        StartCoroutine(nameof(CalculateScoreCounter));
        LeanTween.scale(bgImage, Vector3.one * 10, 0.2f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(crashTittle, Vector3.one, 0.2f).setEaseInOutSine();
            LeanTween.scale(scoreHolder, Vector3.one, 0.2f).setEaseInOutSine();
            LeanTween.scale(coinHolder, Vector3.one, 0.2f).setEaseInOutSine();
        });

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator CalculateScoreCounter()
    {
        while (counter != scoreData.scoreCount)
        {
            counter = Mathf.MoveTowards(counter, scoreData.scoreCount, scoreData.scoreCount / 2.6f * Time.deltaTime);
            finalScoreTxt.text = counter.ToString("0");
            yield return null;

            Debug.Log("Calculating");
        }
        
        Debug.Log("Calculation completed");
        CaptureSS(); 
        yield return new WaitForSeconds(0.5f);

        LeanTween.scale(photoHolder, Vector3.one, 0.5f);
        LeanTween.rotateAround(photoHolder, Vector3.forward, 1808, 0.5f).setOnComplete(() =>
        {
            LeanTween.scale(retryButton, Vector3.one, 0.2f).setEaseInOutSine().setOnComplete(()=>
            {
                LeanTween.scale(homeButtom, Vector3.one, 0.2f).setEaseInOutSine();
            });
        });
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
