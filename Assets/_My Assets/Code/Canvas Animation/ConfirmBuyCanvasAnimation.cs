using UnityEngine;

public class ConfirmBuyCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject backgroundImg;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject mainHeader;
    [SerializeField] private GameObject confirmTxt;
    [SerializeField] private GameObject buttonHolder;


    public void PlayAnimation()
    {
        ResetAnimation();

        LeanTween.scale(backgroundImg, Vector3.one * 10, 0.25f).setEaseInOutSine().setOnComplete(()=>
        {
            LeanTween.scale(mainCanvas, Vector3.one, 0.25f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.scale(mainHeader, Vector3.one, 0.25f).setEaseInOutSine();
                LeanTween.scale(confirmTxt, Vector3.one, 0.25f).setEaseInOutSine();
                LeanTween.scale(buttonHolder, Vector3.one, 0.25f).setEaseInOutSine();
            });
        });
    }

    public void ResetAnimation()
    {
        backgroundImg.transform.localScale = Vector3.zero;
        mainCanvas.transform.localScale = Vector3.zero;
        mainHeader.transform.localScale = Vector3.zero;
        confirmTxt.transform.localScale = Vector3.zero;
        buttonHolder.transform.localScale = Vector3.zero;
    }
}
