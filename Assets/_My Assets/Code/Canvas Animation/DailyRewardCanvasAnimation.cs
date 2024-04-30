using UnityEngine;
using System.Collections.Generic;

public class DailyRewardCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject dailyRewardCanvas;
    [SerializeField] private GameObject tittleHolder;
    [SerializeField] private List<GameObject> rewards = new List<GameObject>();
    [SerializeField] private GameObject collectButton;

    float animationSpeed = 0.15f;

    public void PlayAnimation()
    {
        ResetAnimation();
        LeanTween.scale(dailyRewardCanvas, Vector3.one, 0.25f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(tittleHolder, Vector3.one, 0.25f).setEaseInOutSine();
            for (int i = 0; i < rewards.Count; i++)
            {
                LeanTween.scale(rewards[i], Vector3.one, animationSpeed).setEaseInOutSine();
                animationSpeed += 0.03f;
            }
        });
        LeanTween.scale(collectButton, Vector3.one, 0.25f).setEaseInOutSine();
    }

    public void ResetAnimation()
    {
        dailyRewardCanvas.transform.localScale = Vector3.zero;
        tittleHolder.transform.localScale = Vector3.zero;
        collectButton.transform.localScale = Vector3.zero;

        foreach (GameObject item in rewards)
        {
            item.transform.localScale = Vector3.zero;    
        }
        animationSpeed = 0.15f;
    }
}
