using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Collections.Generic;

public class CarStoreManager : MonoBehaviour
{
    [Header("<size=15>[SCRIPT]")]
    [SerializeField] private EnableSelectedCar enableSelectedCar;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private CarStoreData carStoreData;

    [Header ("<size=15>[CARD INFORMATION]")]
    [SerializeField] private Image cardCarImage;
    [SerializeField] private TMP_Text cardCarName;
    [SerializeField] private TMP_Text cardCarPrice;
    [SerializeField] private TMP_Text cardCarClass;
    [Space]
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject equippedImage;

    [Header ("<size=15>[TOGGLE INFROMATION]")]
    [SerializeField] private List<CarToggle> allCarToggle = new List<CarToggle>();


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
            if (carStoreData.selectedCarData.equippedCarclass == carToggle.GetCarClass() &&
                carStoreData.selectedCarData.equippedCarIndex == carToggle.GetMyIndex())
            {
                carToggle.EquipMe();
                enableSelectedCar.ChangeCar(carStoreData.selectedCarData.equippedCarclass, carStoreData.selectedCarData.equippedCarIndex);
            }
            else
            {
                carToggle.UnEquipMe();
            }
            carToggle.UpdateUI();
        }
    }
}
