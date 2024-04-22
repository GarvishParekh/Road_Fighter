using UnityEngine;
using System.Collections.Generic;

public class EnableSelectedCar : MonoBehaviour
{
    [SerializeField] private CarStoreData carStoreData;
    [SerializeField] private List<Cars> carsList = new List<Cars>();

    private void Start()
    {
        ChangeCar(carStoreData.selectedCarData.equippedCarclass, carStoreData.selectedCarData.equippedCarIndex);
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
