using UnityEngine;

public enum BoostingStatus
{
    IsBoosting,
    NotBoosting
}

public class NosSystem : MonoBehaviour
{

    public BoostingStatus boostingStatus;
    [SerializeField] private float nosValue = 1;
    [SerializeField] private ParticleSystem nosParticles;

    [ContextMenu("NOS")]
    public void BoostButtonDown()
    {
        boostingStatus = BoostingStatus.IsBoosting;
        nosValue = 15;
        nosParticles.Play();
    }

    public void BoostButtonUp()
    {
        boostingStatus = BoostingStatus.NotBoosting;
        nosValue = 1;
        nosParticles.Stop();
    }

    float returnNosValue = 1;
    public float GetNosValue()
    {
        returnNosValue = Mathf.MoveTowards (returnNosValue, nosValue, Time.deltaTime * 10);    
        return returnNosValue;
    }

}
