using UnityEngine;

public class OnScreenNearMissCanvasAnimation : MonoBehaviour, ICanvasCellAnimation
{
    [SerializeField] private NearMissTrigger nearMissTrigger;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform placeToSpawn;
    [SerializeField] GameObject scoreHolder;


    private void OnEnable()
        => PlayAnimation();

    public void PlayAnimation()
    {
        ResetAnimation();

        Vector3 placeToSpawn = nearMissTrigger.GetTrafficCar().position;
        Vector3 spawnPosition = mainCamera.WorldToScreenPoint(placeToSpawn);
        transform.position = spawnPosition;

        LeanTween.scale(this.gameObject, Vector3.one, 0.55f).setEaseInOutSine();
        LeanTween.moveLocalY(this.gameObject, 100, 0.55f).setEaseInOutSine().setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void ResetAnimation()
    {
        transform.localScale = Vector3.zero;
    }
}
