using UnityEngine;
using System.Collections.Generic;

public class UpgradeCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject header;
    [SerializeField] private List<GameObject> upgradesCards = new List<GameObject>();

    [SerializeField] private float animationSpeed = 0.25f;

    public void PlayAnimation()
    {
        ResetAnimation();
        LeanTween.scale(mainCanvas, Vector3.one, animationSpeed).setEaseInOutSine().setOnComplete(()=>
        {
            LeanTween.scale(header, Vector3.one, animationSpeed).setEaseInOutSine();
            
            animationSpeed = 0.25f;
            for (int i = 0; i < upgradesCards.Count; i++)
            {
                LeanTween.moveLocal(upgradesCards[i], Vector3.zero, animationSpeed).setEaseInOutSine();
                LeanTween.scale(upgradesCards[i], Vector3.one, animationSpeed).setEaseInOutSine();
                animationSpeed += 0.1f;
            }
        });
    }

    public void ResetAnimation()
    {
        animationSpeed = 0.25f;
        mainCanvas.transform.localScale = Vector3.zero;
        header.transform.localScale = Vector3.zero;

        float carYPos = -1000f;
        for (int i = 0; i < upgradesCards.Count; i++)
        {
            LeanTween.moveLocalY(upgradesCards[i], carYPos, 0);
            upgradesCards[i].transform.localScale = Vector3.zero;
        }
    }
}
