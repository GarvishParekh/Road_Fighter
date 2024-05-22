using UnityEngine;

public class NewSettingsCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject mainHolder;

    public void PlayAnimation()
    {
        ResetAnimation();
        LeanTween.moveLocalY(mainHolder, 0, 0.25f).setEaseInOutSine();
    }

    public void ResetAnimation()
    {
        LeanTween.moveY(mainHolder, -500f, 0);
    }
}
