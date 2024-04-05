using UnityEngine;

public class RoadShifter : MonoBehaviour
{
    [SerializeField] private Transform mainRoad;
    [SerializeField] private Transform roadShifter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            roadShifter.position += Vector3.forward * 12;
            mainRoad.position = roadShifter.position;
        }
    }
}
