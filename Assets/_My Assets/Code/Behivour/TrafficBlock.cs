using UnityEngine;
using Unity.Mathematics;

public class TrafficBlock : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private TrafficData trafficData;
    [SerializeField] private GameObject[] lanes;
    [SerializeField] private Transform trafficSpawnPoint;
    [SerializeField] private Transform playerCar;
    [SerializeField] private Transform laneHolder;

    float distance;
    int totalCarsList = 0;
    int carToSpawn = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        totalCarsList = trafficData.allCars.Length;
        laneHolder = transform.GetChild(0); 
    }

    private void Start()
    {
        SpawnCar();
        ResetLane();
    }

    private void FixedUpdate()
    {
        switch (trafficData.trafficStatus)
        {
            case TrafficData.TrafficStatus.MOVING:
                rb.velocity = Vector3.forward * trafficData.trafficSpeed;
                CalculateDistance();
                break;
            case TrafficData.TrafficStatus.STATIC:
                rb.velocity = Vector3.zero;
                break;
        }
    }

    private void SpawnCar()
    {
        for (int i = 0; i < lanes.Length; i++)
        {
            carToSpawn = UnityEngine.Random.Range(0, totalCarsList);
            Transform spawnedCar = Instantiate(trafficData.allCars[carToSpawn], lanes[i].transform.position, quaternion.identity, lanes[i].transform).transform;

            spawnedCar.localScale = Vector3.one * 0.85f;
        }
    }

    private void ResetLane()
    {
        int count = 0;
        count = UnityEngine.Random.Range (0, lanes.Length);
        
        CloseAllLanes();
        lanes[count].gameObject.SetActive (true);
    }

    private void CloseAllLanes()
    {
        foreach (GameObject lane in lanes) 
        {
            lane.SetActive (false);
        }
    }


    private void CalculateDistance()
    {
        distance = transform.position.z - playerCar.position.z; 
        if (distance < -5)
        {
            transform.position = trafficSpawnPoint.position;
            ResetLane();
        }
    }
}
