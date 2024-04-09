using UnityEngine;

[CreateAssetMenu(fileName = "Building Data", menuName = "Building Data")]
public class BuildingData : ScriptableObject
{
    public GameObject[] leftBuildings;
    public GameObject[] rightBuildings;

    [Space]
    public Vector3 leftRotation = new Vector3(0, -90, 0);
    public Vector3 rightRotation = new Vector3(0, 90, 0);
}
