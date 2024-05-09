using System;
using UnityEngine;

public class NearMissTrigger : MonoBehaviour
{
    public static Action<NearMissSide> NearMiss;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag ("NearMiss"))
        {
            Debug.Log("Near Miss");
            collider.gameObject.SetActive(false);

            NearMissTriggerIdentity nearMissIdentity = collider.GetComponent<NearMissTriggerIdentity>();
            NearMiss?.Invoke(nearMissIdentity.GetMySide());
        }
    }
}
