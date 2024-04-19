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
    public EquippedCarData equippedCarData;
    public SelectedCarData selectedCarData;
}

[System.Serializable]
public class CarsData
{
    public string carName;    
    public Sprite carImage;
    public int carPrice;
    public CarLockState carLockState;
    public CarEquipState carEquipState;
    public string lockPlayerPref = "";
    public string equipPlayerPref = "";
}

public enum CarsClass
{
    S_CLASS,
    A_CLASS,
    B_CLASS,
    C_CLASS,
}


public enum CarLockState
{
    LOCKED,
    UNLOCKED,
}

public enum CarEquipState
{
    UNEQUIPPED,
    EQUIPPED
}

[System.Serializable]
public class EquippedCarData
{
    public CarsClass equippedCarclass;
    public int equippedCarIndex;
}

[System.Serializable]
public class SelectedCarData
{
    public CarsClass equippedCarclass;
    public int equippedCarIndex;
}
