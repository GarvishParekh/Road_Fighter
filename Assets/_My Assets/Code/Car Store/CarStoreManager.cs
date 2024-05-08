using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Collections.Generic;

public class CarStoreManager : MonoBehaviour
{
    [Header("<size=15>[SCRIPT</SIZE> <size=10><color=#7DAA1B>RUNTIME</color><SIZE=15>]")]
    [SerializeField] private UiManager uiManager;
    [SerializeField] private EconomyManager economyManager;

    [Header("<size=15>[SCRIPT</SIZE> <size=10><color=#FF0000>REFERENCE</color><SIZE=15>]")] 
    [SerializeField] private EnableSelectedCar enableSelectedCar;
    [SerializeField] private MainMenuUIManager mainMenuUIManager;

    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private CarStoreData carStoreData;
    [SerializeField] private EconomyData economyData;

    [Header("<size=15>[CARD INFORMATION]")]
    [SerializeField] private Image cardCarImage;
    [SerializeField] private TMP_Text cardCarName;
    [SerializeField] private TMP_Text cardCarPrice;
    [SerializeField] private TMP_Text cardCarClass;
    [Space]
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equippedImage;

    [Header("<size=15>[TOGGLE INFROMATION]")]
    [SerializeField] private List<CarToggle> allCarToggle = new List<CarToggle>();

    private void Start()
    {
        uiManager = UiManager.instance;
        economyManager = EconomyManager.instance;
    }

    public void UpdateCard(Sprite carIcon, string _carName, int _carPrice, string _carClass, CarLockState _carState, CarEquipState _carEquipState)
    {
        cardCarImage.sprite = carIcon;
        cardCarName.text = _carName;
        cardCarPrice.text = _carPrice.ToString("#,##0", CultureInfo.InvariantCulture);
        cardCarClass.text = _carClass;

        switch (_carState)
        {
            case CarLockState.LOCKED:
                UpdateButtons(buyButton);
                break;
            case CarLockState.UNLOCKED:
                switch (_carEquipState)
                {
                    case CarEquipState.UNEQUIPPED:
                        UpdateButtons(equipButton);
                        break;
                    case CarEquipState.EQUIPPED:
                        UpdateButtons(equippedImage);
                        break;
                }
                break;
        }
    }

    private void UpdateButtons(GameObject desireObject)
    {
        buyButton.SetActive(false);
        equipButton.SetActive(false);
        equippedImage.SetActive(false);

        desireObject.SetActive(true);
    }

    public void FilterCars(string className)
    {
        switch (className)
        {
            case "s":
                foreach (CarToggle carToggle in allCarToggle)
                {
                    if (carToggle.GetCarClass() == CarsClass.S_CLASS)
                        carToggle.gameObject.SetActive(true);
                    else
                        carToggle.gameObject.SetActive(false);
                }
                break;
            case "a":
                foreach (CarToggle carToggle in allCarToggle)
                {
                    if (carToggle.GetCarClass() == CarsClass.A_CLASS)
                        carToggle.gameObject.SetActive(true);
                    else
                        carToggle.gameObject.SetActive(false);
                }
                break;
            case "b":
                foreach (CarToggle carToggle in allCarToggle)
                {
                    if (carToggle.GetCarClass() == CarsClass.B_CLASS)
                        carToggle.gameObject.SetActive(true);
                    else
                        carToggle.gameObject.SetActive(false);
                }
                break;
            case "c":
                foreach (CarToggle carToggle in allCarToggle)
                {
                    if (carToggle.GetCarClass() == CarsClass.C_CLASS)
                        carToggle.gameObject.SetActive(true);
                    else
                        carToggle.gameObject.SetActive(false);
                }
                break;
            case "all":
                foreach (CarToggle carToggle in allCarToggle)
                {
                    carToggle.gameObject.SetActive(true);
                }
                break;
        }
    }

    public void EquipButton()
    {
        foreach (CarToggle carToggle in allCarToggle)
        {
            if (GetSelectedCarClass() == carToggle.GetCarClass() &
                GetSelectedCarIndex() == carToggle.GetMyIndex())
            {
                carToggle.EquipMe();
                enableSelectedCar.ChangeCar(GetSelectedCarClass(), GetSelectedCarIndex());
            }
            else
            {
                carToggle.UnEquipMe();
            }
            carToggle.UpdateUI();
        }
    }

    public void BuyButton()
    {
        int carPrice = 0;
        int selectedCarIndex = GetSelectedCarIndex();
        string selectedCarName = string.Empty;
        
        switch (GetSelectedCarClass())
        {
            case CarsClass.S_CLASS:
                carPrice = carStoreData.S_cars[selectedCarIndex].carPrice;
                selectedCarName = carStoreData.S_cars[selectedCarIndex].carName;
                break;
            case CarsClass.A_CLASS:
                carPrice = carStoreData.A_cars[selectedCarIndex].carPrice;
                selectedCarName = carStoreData.A_cars[selectedCarIndex].carName;
                break;
            case CarsClass.B_CLASS:
                carPrice = carStoreData.B_cars[selectedCarIndex].carPrice;
                selectedCarName = carStoreData.B_cars[selectedCarIndex].carName;
                break;
            case CarsClass.C_CLASS:
                carPrice = carStoreData.C_cars[selectedCarIndex].carPrice;
                selectedCarName = carStoreData.C_cars[selectedCarIndex].carName;
                break;
            default:
                Debug.Log("Wrong class");
                break;
        }

        if (economyManager.IsMoneyAvilable(carPrice))
        {
            Button buyButton = economyManager.GetConfirmBuyButton(carPrice, selectedCarName, PurchaseType.BUY);
            foreach (CarToggle carToggle in allCarToggle)
            {
                if (GetSelectedCarClass() == carToggle.GetCarClass() &&
                    GetSelectedCarIndex() == carToggle.GetMyIndex())
                {
                    buyButton.onClick.AddListener(carToggle.BuyMe);
                    buyButton.onClick.AddListener(EquipButton);
                }

                uiManager.OpenPopupCanvas(CanvasCellsName.CONFIRM_BUY_POPUP);
            }
        }
        else
        {
            mainMenuUIManager.NoEnoughCanvas(carPrice - economyData.availableCoins);
        }
    }

    public void SetEquippedCarClass(CarsClass _carClass)
        => carStoreData.equippedCarData.equippedCarclass = _carClass;
    public void SetEquippedCarIndex(int _carIndex)
        => carStoreData.equippedCarData.equippedCarIndex = _carIndex;

    private CarsClass GetSelectedCarClass()
    {
        return carStoreData.selectedCarData.equippedCarclass;
    }
    private int GetSelectedCarIndex()
    {
        return carStoreData.selectedCarData.equippedCarIndex;
    }
    
}

