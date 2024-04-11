using UnityEngine;

public class InstructionCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject bgImg;
    [SerializeField] private GameObject instructionImg;
    [SerializeField] private GameObject okayBtn;
    public void PlayAnimation()
    {
        ResetAnimation();
        LeanTween.scale(bgImg, Vector3.one * 10, 0.5f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.scale(instructionImg, Vector3.one, 0.1f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.scale(okayBtn, Vector3.one, 0.1f).setEaseInOutSine().setDelay(5);
            }); 
        });
    }

    public void ResetAnimation()
    {
        bgImg.transform.localScale = Vector3.zero;
        instructionImg.transform.localScale = Vector3.zero;
        okayBtn.transform.localScale = Vector3.zero;
    }
}
