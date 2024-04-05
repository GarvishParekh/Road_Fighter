using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSwitcher : MonoBehaviour
{
    [SerializeField] private List<Cars> carList = new List<Cars>();
    [SerializeField] private TMP_Text carIndexTxt;

    private int listCount;

    private void Start()
    {
        listCount = carList.Count;
        carIndexTxt.text = $"0 out of {listCount}";
    }

    public void ChangeNextCar()
    {
        Cars carToRemove = carList[0];
        carToRemove.gameObject.SetActive(false);    
        carList.Remove(carToRemove);

        Cars CarToSpawn = carList[0];
        CarToSpawn.gameObject.SetActive(true);
        carIndexTxt.text = $"{carToRemove.GetCarIndex().ToString("00")} out of {listCount}";
        carList.Add(carToRemove);
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            ChangeNextCar();
        }
    }
}
