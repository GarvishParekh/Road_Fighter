using UnityEngine;

public class InstructionCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    private enum AnimationType
    {
        TILT,
        HOLD,
        NONE
    }
    [SerializeField] private GameObject bgImg;
    [SerializeField] private GameObject instructionImg;
    [SerializeField] private GameObject okayBtn;

    [Header(" [ANIMATION] ")]
    [SerializeField] private AnimationType animationType;
    [SerializeField] private GameObject animationHolder;
    
    public void PlayAnimation()
    {
        ResetAnimation();
        switch (animationType)
        {
            case AnimationType.TILT:
                TiltAnimation();
                break;
            case AnimationType.HOLD:
                HoldAnimation();
                break;
        }

        LeanTween.scale(bgImg, Vector3.one * 10, 0.5f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(instructionImg, Vector3.one, 0.1f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.scale(okayBtn, Vector3.one, 0.1f).setEaseInOutSine().setDelay(5);
            }); 
        });
    }

    private void TiltAnimation()
    {
        animationHolder.transform.rotation = Quaternion.Euler(0, 0, -10);

        LeanTween.rotateZ(animationHolder, 10, 0.5f).setEaseInOutSine().setLoopPingPong(10).setOnComplete(() =>
        {
            LeanTween.rotateZ(animationHolder, 0, 0.5f).setEaseInOutSine();
        });
    }

    private void HoldAnimation()
    {
        animationHolder.transform.rotation = Quaternion.Euler(Vector3.zero);

        LeanTween.rotateX(animationHolder, 30, 0.5f).setEaseInOutSine().setLoopPingPong(10).setOnComplete(() =>
        {
            LeanTween.rotateX(animationHolder, 0, 0.5f).setEaseInOutSine();
        });
    }

    public void ResetAnimation()
    {
        bgImg.transform.localScale = Vector3.zero;
        instructionImg.transform.localScale = Vector3.zero;
        okayBtn.transform.localScale = Vector3.zero;
    }
}
