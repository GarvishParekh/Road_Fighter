using UnityEngine;

public class SettingsCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject mainHolder;
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject musicAndSFXHolder;
    [SerializeField] private GameObject graphicHolfrt;
    [SerializeField] private GameObject rateGameHolder;

    [Space]
    [SerializeField] private float animationSpeed = 0.35f;
    public void PlayAnimation()
    {
        ResetAnimation();


        LeanTween.scale(mainHolder, Vector3.one, animationSpeed).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(musicAndSFXHolder, Vector3.one, animationSpeed).setEaseInOutSine();
            LeanTween.scale(graphicHolfrt, Vector3.one, animationSpeed).setEaseInOutSine();
            LeanTween.scale(rateGameHolder, Vector3.one, animationSpeed).setEaseInOutSine();

            LeanTween.scale(header, Vector3.one, animationSpeed).setEaseInOutSine();
            LeanTween.moveLocal(musicAndSFXHolder, Vector3.zero, animationSpeed + 0.1f).setEaseInOutSine();
            LeanTween.moveLocal(graphicHolfrt, Vector3.zero, animationSpeed + 0.15f).setEaseInOutSine();
            LeanTween.moveLocal(rateGameHolder, Vector3.zero, animationSpeed + 0.25f).setEaseInOutSine();
        });
    }

    public void ResetAnimation()
    {
        mainHolder.transform.localScale = Vector3.zero;
        header.transform.localScale = Vector3.zero;

        musicAndSFXHolder.transform.localScale = Vector3.zero;
        graphicHolfrt.transform.localScale = Vector3.zero;
        rateGameHolder.transform.localScale = Vector3.zero;
        
        musicAndSFXHolder.transform.localPosition = new Vector3(0, -1000, 0);
        graphicHolfrt.transform.localPosition = new Vector3(0, -1000, 0);
        rateGameHolder.transform.localPosition = new Vector3(0, -1000, 0);
    }
}
