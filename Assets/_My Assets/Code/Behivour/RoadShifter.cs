using UnityEngine;

public class RoadShifter : MonoBehaviour
{
    [SerializeField] private Transform mainRoad;
    [SerializeField] private Transform roadShifter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            mainRoad.position = roadShifter.position;
            roadShifter.position += Vector3.forward * 24;
        }
    }
}
