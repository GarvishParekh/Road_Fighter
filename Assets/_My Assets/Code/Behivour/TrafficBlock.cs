using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;

public class TrafficBlock : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private MovingCars movingCars;
    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private TrafficData trafficData;

    [Header ("<size=15>[COMPONENTS]")]
    [SerializeField] private GameObject[] lanes;
    [SerializeField] private Transform trafficSpawnPoint;
    [SerializeField] private Transform playerCar;

    [SerializeField] private List<GameObject> nearMissTriggerList = new List<GameObject>();
    [SerializeField] private List<EnemyCarMovement> enemyCarMovement = new List<EnemyCarMovement>();

    float nextSpawnPointDistance = 0;

    float distance;
    int totalCarsList = 0;
    int carToSpawn = 0;

    int speedLevel = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        totalCarsList = trafficData.allCars.Length;
    }

    private void Start()
    {
        SpawnCar();
        ResetLane();

        speedLevel = UnityEngine.Random.Range(0, trafficData.trafficSpeed.Length);
    }

    private void FixedUpdate()
    {
        switch (trafficData.trafficStatus)
        {
            case TrafficData.TrafficStatus.MOVING:
                rb.velocity = Vector3.forward * trafficData.trafficSpeed[speedLevel];
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
            Transform spawnedCar = Instantiate(trafficData.allCars[carToSpawn], lanes[i].transform.position, quaternion.identity, lanes[i].transform.GetChild(0)).transform;

            spawnedCar.localScale = Vector3.one * 0.85f;
        }
    }

    private void ResetLane()
    {
        int count = 0;
        count = UnityEngine.Random.Range (0, lanes.Length);
        
        CloseAllLanes();
        lanes[count].gameObject.SetActive (true);
        EnableNearMissColliders();
        RandomMovingStatus();
    }

    private void RandomMovingStatus()
    {
        int randomCount = UnityEngine.Random.Range(0, 2);
        movingCars = (MovingCars)randomCount;

        switch (movingCars) 
        {
            case MovingCars.MOVING:
                foreach (var carMovement in enemyCarMovement)
                {
                    carMovement.StartMoving();
                }
                break;
            case MovingCars.STATIC:
                foreach (var carMovement in enemyCarMovement)
                {
                    carMovement.StopMoving();
                }
                break;
        }
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
        if (GetDistanceFromPlayer() < trafficData.positionOffCamera)
        {
            transform.position = trafficSpawnPoint.position;
            
            SetNewSpawnPointPosition(transform);
            ResetLane();
        }
    }

    private float GetDistanceFromPlayer()
    {
        distance = transform.position.z - playerCar.position.z;
        return distance;    
    }

    /// <summary>
    /// Setting new position for spawn point ahead of newly spawn car
    /// </summary>
    private void SetNewSpawnPointPosition(Transform myTransform)
    {
        // set spawn point new position
        trafficSpawnPoint.SetParent(myTransform);
        trafficSpawnPoint.localPosition = trafficSpawnPoint.localPosition + Vector3.forward * GetNewSpawnPoition();
    }

    private float GetNewSpawnPoition()
    {
        float minDistance = trafficData.minTrafficSpawnDistance;
        float maxDistance = trafficData.maxTrafficSpawnDistance;
        nextSpawnPointDistance = UnityEngine.Random.Range(minDistance, maxDistance);

        return nextSpawnPointDistance;
    }


    private void EnableNearMissColliders()
    {
        foreach(GameObject nearMissCollider in nearMissTriggerList)
        {
            nearMissCollider.SetActive(true);
        }
    }
}
