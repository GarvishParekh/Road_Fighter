using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CarToggle : MonoBehaviour
{
    Toggle myToggle;
    [Header("<size=15>[SCRIPTS]")]
    [SerializeField] private CarStoreManager carStoreManager;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private CarStoreData carStoreData;
    [SerializeField] private List<CarsData> myCarList = new List<CarsData>();

    [SerializeField] private CarsClass myCarClass;

    [Header ("<size=15>[TOGGLE DATA]")]
    [SerializeField] private int myCarIndex = 0;

    [Space]
    [SerializeField] private Image carIcon;
    [SerializeField] private int myCarPrice = 0;
    [SerializeField] private string myCarName = "";
    [SerializeField] private string myClass = "";

    private void Awake()
    {
        myToggle = GetComponent<Toggle>();  
    }

    private void Start()
    {
        SetMyCatList();
        SetAllValues();

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
        myCarList[myCarIndex].playerPrefTag = $"{myCarName}_PLAYER_PREF";
    }

    public void OnSelectToggle(Toggle _myToggle)
    {
        if (_myToggle.isOn)
        {
            carStoreManager.UpdateCard
            (
                carIcon.sprite,
                myCarName,
                myCarPrice,
                myClass,
                myCarList[myCarIndex].carState
            );
        }
    }

    public CarsClass GetCarClass()
    {
        return myCarClass;
    }
}
