using UnityEngine;

public class NearMissCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [Header ("<size=15>[COMPONENTS]")]
    [SerializeField] private GameObject nearMissImage;
    [SerializeField] private Transform endPosition;

    [Header ("<size=15>[VALUES]")]
    [Range (0.3f, 1f)]
    [SerializeField] private float animationIntroSpeed;
    [Range (0.3f, 1f)]
    [SerializeField] private float animationOutroSpeed;
    [SerializeField] private float onStayTime;
    [SerializeField] private float rotationMaxValue = 40;
    [SerializeField] private float scaleMaxValue = 2;

    private void OnEnable()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        ResetAnimation();

        LeanTween.moveLocal(nearMissImage, Vector3.zero, animationIntroSpeed).setEaseInOutElastic().setOnComplete(()=>
        {
            LeanTween.moveLocal(nearMissImage, endPosition.localPosition, 0.5f).setEaseInOutSine().setDelay(onStayTime).setOnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        });
    }

    public void ResetAnimation()
    {
        LeanTween.moveLocal(nearMissImage, endPosition.localPosition, 0);
        
        RandomizeRotation(rotationMaxValue, nearMissImage);
        RandomizeScale(scaleMaxValue, nearMissImage);
    }

    private void RandomizeScale(float _maxScale, GameObject _object)
    {
        _object.transform.localScale = Vector3.one * Random.Range(1, _maxScale); ;
    }

    private void RandomizeRotation(float _minMaxRotation, GameObject _object)
    {
        _object.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-_minMaxRotation, _minMaxRotation));
    }
}
