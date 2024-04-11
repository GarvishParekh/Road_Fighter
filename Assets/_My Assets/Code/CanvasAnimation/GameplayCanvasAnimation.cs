using UnityEngine;

public class GameplayCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    public GameObject healthBar;
    public GameObject socreHolder;
    public GameObject speedLevelHolder;
    public GameObject scoreMultiplier;

    public void PlayAnimation()
    {
        ResetAnimation();

        LeanTween.scale(healthBar, Vector3.one, 0.2f).setEaseInOutSine();
        LeanTween.scale(socreHolder, Vector3.one, 0.2f).setEaseInOutSine();
        LeanTween.scale(speedLevelHolder, Vector3.one, 0.2f).setEaseInOutSine();
        LeanTween.scale(scoreMultiplier, Vector3.one, 0.2f).setEaseInOutSine();
    }

    public void ResetAnimation()
    {
        healthBar.transform.localScale = Vector3.zero;
        socreHolder.transform.localScale = Vector3.zero;
        speedLevelHolder.transform.localScale = Vector3.zero;
        scoreMultiplier.transform.localScale = Vector3.zero;
    }
}
