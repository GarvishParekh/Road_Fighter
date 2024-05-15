using UnityEngine;
using System.Collections.Generic;

public class EnableSelectedCar : MonoBehaviour
{
    [SerializeField] private CarStoreData carStoreData;
    [SerializeField] private List<Cars> carsList = new List<Cars>();
    [SerializeField] private GameObject carHolder;

    public bool foundEquippedCar = false;

    private void Start()
    {
        //PlayerPrefs.GetInt(ConstantKeys.car)
        if (carHolder != null)
        {
            carHolder.transform.localScale = Vector3.zero;
            Invoke(nameof(ChangeCarDelay), 2f);
        }
        else
        {
            CarsClass carClass = carStoreData.equippedCarData.equippedCarclass;
            int carIndex = carStoreData.equippedCarData.equippedCarIndex;
            ChangeCar(carClass, carIndex);
        }
    }

    private void ChangeCarDelay()
    {
        LeanTween.scale(carHolder, Vector3.one, 0.5f).setEaseInOutBounce();

        CarsClass carClass = carStoreData.equippedCarData.equippedCarclass;
        int carIndex = carStoreData.equippedCarData.equippedCarIndex;
        ChangeCar(carClass, carIndex);
    }

    public void ChangeCar (CarsClass _myCarClass, int _myCarIndex)
    {
        foreach (Cars car in carsList) 
        {
            if (_myCarClass == car.GetMyClass() && _myCarIndex == car.GetCarIndex())
            { 
                car.gameObject.SetActive(true);
            }
            else
            {
                car.gameObject.SetActive(false);
            }
        }
    }
}
