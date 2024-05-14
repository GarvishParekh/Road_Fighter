using System;
using UnityEngine;

public class NearMissTrigger : MonoBehaviour, ICanvasCellAnimation
{
    SFXPlayer sfxPlayer;

    public static Action<NearMissSide> NearMiss;
    [SerializeField] private GameObject outerGlowObj;
    [SerializeField] private float animationSpeed = 0.35f;

    private void Start()
    {
        sfxPlayer = SFXPlayer.instance;
    }

    public void PlayAnimation()
    {
        ResetAnimation();
        LeanTween.scale(outerGlowObj, Vector3.one, animationSpeed).setEaseInOutSine().setOnComplete(()=>
        {
            LeanTween.scale(outerGlowObj, Vector3.one * 2, animationSpeed).setEaseInOutSine().setDelay(0.1f); ;
        });

    }

    public void ResetAnimation()
    {
        outerGlowObj.transform.localScale = Vector3.one * 2;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag ("NearMiss"))
        {
            Debug.Log("Near Miss");
            collider.gameObject.SetActive(false);

            NearMissTriggerIdentity nearMissIdentity = collider.GetComponent<NearMissTriggerIdentity>();
            NearMiss?.Invoke(nearMissIdentity.GetMySide());

            PlayAnimation();

            sfxPlayer.PlayWooshSound();
        }
    }
}
