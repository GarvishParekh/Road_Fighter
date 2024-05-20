using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.Concurrent;

public class CarToggle : MonoBehaviour
{
    Toggle myToggle;
    [Header("<size=15>[SCRIPT</SIZE> <size=10><color=#FF0000>REFERENCE</color><SIZE=15>]")]
    [SerializeField] private EnableSelectedCar enableSelectedCar;
    [SerializeField] private CarStoreManager carStoreManager;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private CarStoreData carStoreData;
    [SerializeField] private List<CarsData> myCarList = new List<CarsData>();

    [SerializeField] private CarsClass myCarClass;

    [Header ("<size=15>[TOGGLE DATA]")]
    [SerializeField] private int myCarIndex = 0;

    [Space]
    [SerializeField] private Image carIcon;
    [SerializeField] private Image lockImge;
    [SerializeField] private int myCarPrice = 0;
    [SerializeField] private string myCarName = "";
    [SerializeField] private string myClass = "";

    int lockStatus = 0;
    int equipStatus = 0;

    string lockPlayerPref;
    string equipPlayerPref;

    private void Awake()
    {
        myToggle = GetComponent<Toggle>();  
        // get the list from which data can be restrived
        SetMyCatList();

        // get car information and lock unlock information
        SetAllValues();
        LockIdentifier();

        myToggle.onValueChanged.AddListener(delegate
        {
            OnSelectToggle(myToggle);
        });
    }

    private void SetMyCatList()
    {
        switch (myCarClass)
        {
            case CarsClass.S_CLASS:
                myCarList = carStoreData.S_cars;
                myClass = "S CLASS";
                break;
            case CarsClass.A_CLASS:
                myCarList = carStoreData.A_cars;
                myClass = "A CLASS";
                break;
            case CarsClass.B_CLASS:
                myCarList = carStoreData.B_cars;
                myClass = "B CLASS";
                break;
            case CarsClass.C_CLASS:
                myCarList = carStoreData.C_cars;
                myClass = "C CLASS";
                break;
        }
    }

    private void SetAllValues()
    {
        carIcon.sprite = myCarList[myCarIndex].carImage;
        myCarName = myCarList[myCarIndex].carName;
        myCarPrice = myCarList[myCarIndex].carPrice;

        // set player perf values
        lockPlayerPref = myCarList[myCarIndex].lockPlayerPref = $"{myCarName}_LOCK_PLAYER_PREF";
        equipPlayerPref = myCarList[myCarIndex].equipPlayerPref = $"{myCarName}_EQUIP_PLAYER_PREF";
    }

    public void LockIdentifier()
    {
        switch (myCarClass)
        {
            case CarsClass.C_CLASS:
                switch (myCarIndex)
                {
                    case 0:
                        lockStatus = 1;
                        equipStatus = PlayerPrefs.GetInt(equipPlayerPref, 1);
                    break;

                    default:
                        lockStatus = PlayerPrefs.GetInt(lockPlayerPref, 0);
                        equipStatus = PlayerPrefs.GetInt(equipPlayerPref, 0);
                    break;
                }
                break;
            default:
                lockStatus = PlayerPrefs.GetInt(lockPlayerPref, 0);
                equipStatus = PlayerPrefs.GetInt(equipPlayerPref, 0);
            break;
        }

        switch (lockStatus)
        {
            case 0:
                lockImge.enabled = true;
                break;
            case 1:
                lockImge.enabled = false;
                break;
        }

        /*
        if (equipStatus == 1)
        {
            EquipMe();
            UpdateCardFromHere();
            myToggle.isOn = true;
        }
        */

        Invoke(nameof(DefaultEquip), 1);

    }

    private void DefaultEquip()
    {
        switch (equipStatus)
        {
            case 1:
                EquipMe();
                UpdateCardFromHere();
                myToggle.isOn = true;
            break;
        }
    }

    public void OnSelectToggle(Toggle _myToggle)
    {
        if (_myToggle.isOn)
        {
            UpdateCardFromHere();
        }
    }

    public CarsClass GetCarClass()
    {
        return myCarClass;
    }

    public int GetMyIndex()
    {
        return myCarIndex;
    }

    public void UpdateCardFromHere()
    {
        carStoreManager.UpdateCard
        (
            carIcon.sprite,
            myCarName,
            myCarPrice,
            myClass,
            (CarLockState)lockStatus,
            (CarEquipState)equipStatus
        );

        carStoreData.selectedCarData.equippedCarclass = myCarClass;
        carStoreData.selectedCarData.equippedCarIndex = myCarIndex;
    }

    public void UnEquipMe()
    {
        PlayerPrefs.SetInt(equipPlayerPref, 0);
        myCarList[myCarIndex].carEquipState = CarEquipState.UNEQUIPPED;
    }

    public void EquipMe()
    {
        PlayerPrefs.SetInt(equipPlayerPref, 1);
        myCarList[myCarIndex].carEquipState = CarEquipState.EQUIPPED;

        carStoreManager.SetEquippedCarClass(myCarClass);
        carStoreManager.SetEquippedCarIndex(myCarIndex);
    }

    public void UpdateUI()
    {
        // get car information and lock unlock information
        SetAllValues();
        LockIdentifier();
    }

    public void BuyMe()
    {
        PlayerPrefs.SetInt(lockPlayerPref, 1);
        PlayerPrefs.SetInt(equipPlayerPref, 1);
        myCarList[myCarIndex].carEquipState = CarEquipState.EQUIPPED;

        carStoreData.equippedCarData.equippedCarclass = myCarClass;
        carStoreData.equippedCarData.equippedCarIndex = myCarIndex;

        LockIdentifier();
        UpdateUI();
        UpdateCardFromHere();
        enableSelectedCar.ChangeCar(myCarClass, myCarIndex);

        EconomyManager.ConfirmPurchase?.Invoke(myCarList[myCarIndex].carPrice);
    }
}
