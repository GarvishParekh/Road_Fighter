using UnityEngine;
using UnityEngine.UI;

public class GarageCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] public GameObject coinHolder;
    [SerializeField] public GameObject garageButton;
    [SerializeField] public GameObject storeMainHolder;
    [SerializeField] public GameObject carCard;
    [SerializeField] public GameObject classHolder;
    [SerializeField] public GameObject gridHolder;
    [SerializeField] public ScrollRect scrollGrid;

    public void PlayAnimation()
    {
        scrollGrid.enabled = false;
        ResetAnimation();

        LeanTween.scale(coinHolder, Vector3.one, 0.25f).setEaseInOutSine();
        LeanTween.scale(garageButton, Vector3.one, 0.25f).setEaseInOutSine();
        LeanTween.scale(storeMainHolder, Vector3.one, 0.25f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(carCard, Vector3.one, 0.25f).setEaseInOutSine();
            LeanTween.moveLocal(carCard, Vector3.zero, 0.25f).setEaseInOutSine();

            LeanTween.scale(classHolder, Vector3.one, 0.25f).setEaseInOutSine();
            LeanTween.moveLocal(classHolder, Vector3.zero, 0.25f).setEaseInOutSine();

            LeanTween.moveLocal(gridHolder, Vector3.zero, 0.30f).setEaseInOutSine();
            LeanTween.scale(gridHolder, Vector3.one, 0.30f).setEaseInOutSine().setOnComplete(()=>
            {
                scrollGrid.enabled = true;

            });
        });
    }

    public void ResetAnimation()
    {
        coinHolder.transform.localScale = Vector3.zero;
        garageButton.transform.localScale = Vector3.zero;
        storeMainHolder.transform.localScale = Vector3.zero;
        carCard.transform.localScale = Vector3.zero;
        classHolder.transform.localScale = Vector3.zero;
        gridHolder.transform.localScale = Vector3.zero;

        LeanTween.moveLocalY(carCard, -1000, 0);
        LeanTween.moveLocalY(classHolder, -1000, 0);
        LeanTween.moveLocalY(gridHolder, -1000, 0);
    }
}
