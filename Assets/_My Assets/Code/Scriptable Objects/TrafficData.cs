using UnityEngine;

[CreateAssetMenu(fileName = "Traffic Data", menuName = "Traffic Data")]
public class TrafficData : ScriptableObject
{
    public float trafficSpeed = 2;
    public GameObject[] allCars;
}
