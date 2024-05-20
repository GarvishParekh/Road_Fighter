using UnityEngine;

public class MainMeniCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject coinHolder;
    [SerializeField] private GameObject highscoreHolder;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject startTxt;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject garageButton;
    [SerializeField] private GameObject gameNameImg;

    Vector3 defaultScale = new Vector3(0, 1, 1);

    public void PlayAnimation()
    {
        ResetAnimation();

        LeanTween.scale(coinHolder, Vector3.one, 0.18f).setEaseInOutSine();
        LeanTween.scale(highscoreHolder, Vector3.one, 0.18f).setEaseInOutSine();
        LeanTween.scale(settingsButton, Vector3.one, 0.18f).setEaseInOutSine();
        LeanTween.scale(garageButton, Vector3.one, 0.18f).setEaseInOutSine();

        LeanTween.scale(startButton, Vector3.one, 0.18f).setEaseInOutSine().setDelay(0.18f).setOnComplete(()=>
        {
            LeanTween.moveLocal(gameNameImg, Vector3.zero, 0.85f).setEaseInOutElastic();
            LeanTween.scale(startTxt, new Vector3(1.15f, 1, 1), 0.25f).setEaseInOutSine().setLoopPingPong(-1);
        });
    }

    public void ResetAnimation()
    {
        coinHolder.transform.localScale = defaultScale;
        highscoreHolder.transform.localScale = defaultScale;
        startButton.transform.localScale = defaultScale;
        settingsButton.transform.localScale = defaultScale;
        garageButton.transform.localScale = defaultScale;
        startTxt.transform.localScale = Vector3.one;
        LeanTween.moveLocalY(gameNameImg, 500, 0);
    }
}
