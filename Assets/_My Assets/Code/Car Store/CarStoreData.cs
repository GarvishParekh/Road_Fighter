using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Car Store Data", menuName = "Car Store Data")]

public class CarStoreData : ScriptableObject
{
    public List <CarsData> S_cars = new List<CarsData>();
    public List <CarsData> A_cars = new List<CarsData>();
    public List <CarsData> B_cars = new List<CarsData>();
    public List <CarsData> C_cars = new List<CarsData>();
    public List <CarsData> battlepassCars = new List<CarsData>();

    [Space]
    public string selectedClass = "C";
}

[System.Serializable]
public class CarsData
{
    public string carName;    
    public Sprite carImage;
    public int carPrice;
    public CarState carState;
    public string playerPrefTag = "";
}

public enum CarsClass
{
    S_CLASS,
    A_CLASS,
    B_CLASS,
    C_CLASS,
}


public enum CarState
{
    LOCKED,
    UNLOCKED,
    EQUIPPED
}
