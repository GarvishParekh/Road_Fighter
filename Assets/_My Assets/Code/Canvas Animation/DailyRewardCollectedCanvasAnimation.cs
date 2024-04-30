using UnityEngine;

public class DailyRewardCollectedCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject mainHolder;
    [SerializeField] private GameObject checkMark;
    public void PlayAnimation()
    {
        ResetAnimation();
        LeanTween.scale(mainHolder, Vector3.one, 0.15f).setEaseInOutSine().setOnComplete(()=>
        {
            LeanTween.scale(checkMark, Vector3.one, 0.5f).setEaseInOutSine().setLoopPingPong(3).setOnComplete(() =>
            {
                UiManager.instance.OpenCanvas(CanvasCellsName.MAIN_MENU);
            });
        });
    }

    public void ResetAnimation()
    {
        mainHolder.transform.localScale = Vector3.zero;
        checkMark.transform.localScale = Vector3.one * 0.95f;
    }
}
