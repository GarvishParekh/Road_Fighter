using UnityEngine;

public class NearMissManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag ("NearMiss"))
        {
            Debug.Log("Near Miss");
            collider.enabled = false;   
        }
    }
}
