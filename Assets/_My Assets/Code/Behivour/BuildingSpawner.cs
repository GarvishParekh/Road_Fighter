using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    private enum SpawnSide
    {
        LEFT,
        RIGHT
    }
    [SerializeField] private SpawnSide spawnSide;
    [SerializeField] private BuildingData buildingData; 
   
    void Start()
    {
        int rand = 0;
        Transform spawnedBuilding;
        switch (spawnSide)
        {
            case SpawnSide.LEFT:
                rand = Random.Range(0, buildingData.leftBuildings.Length);
                spawnedBuilding = Instantiate(buildingData.leftBuildings[rand], transform.position, Quaternion.Euler(buildingData.leftRotation)).transform;
                spawnedBuilding.SetParent(transform);
                break;
            case SpawnSide.RIGHT:
                    rand = Random.Range(0, buildingData.rightBuildings.Length);
                spawnedBuilding = Instantiate(buildingData.rightBuildings[rand], transform.position, Quaternion.Euler(buildingData.rightRotation)).transform;
                spawnedBuilding.SetParent(transform);
                break;
        }
    }
}
