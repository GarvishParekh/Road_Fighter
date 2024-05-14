using UnityEngine;

public enum MovingCars
{
    STATIC,
    MOVING
}

[CreateAssetMenu(fileName = "Traffic Data", menuName = "Traffic Data")]
public class TrafficData : ScriptableObject
{
    public enum TrafficStatus
    {
        MOVING,
        STATIC
    }
    public TrafficStatus trafficStatus;
    public float[] trafficSpeed;
    public GameObject[] allCars;

    [Space]
    public float minTrafficSpawnDistance;
    public float maxTrafficSpawnDistance;

    [Space]
    public float positionOffCamera = -5;
}
