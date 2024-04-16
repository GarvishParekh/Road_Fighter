using UnityEngine;

[CreateAssetMenu(fileName = "Car Store Data", menuName = "Car Store Data")]

public class CarStoreData : ScriptableObject
{
    public string carName;    
    public Sprite carImage;
    public int carPrice;
    public CarState carState;
}

public enum CarState
{
    LOCKED,
    UNLOCKED,
    EQUIPPED
}
