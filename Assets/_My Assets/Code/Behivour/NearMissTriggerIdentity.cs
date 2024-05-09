using UnityEngine;

public class NearMissTriggerIdentity : MonoBehaviour
{
    [SerializeField] private NearMissSide myNearMissSide;

    public NearMissSide GetMySide()
    {
        return myNearMissSide;
    }
}
