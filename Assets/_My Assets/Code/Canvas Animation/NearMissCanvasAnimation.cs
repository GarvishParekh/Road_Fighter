using UnityEngine;

public class NearMissCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private GameObject nearMissImge;

    private void OnEnable()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        LeanTween.moveLocal(nearMissImge, Vector3.zero, 0.5f).setEaseOutBounce();
    }

    public void ResetAnimation()
    {
        
    }
}
