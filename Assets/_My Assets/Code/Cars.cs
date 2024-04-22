using UnityEngine;

public class Cars : MonoBehaviour
{
    [SerializeField] CarsClass myClass;
    [SerializeField] private int myCarIndex;

    public CarsClass GetMyClass()
    {
        return myClass; 
    }

    public int GetCarIndex()
    {
        return myCarIndex;    
    }
}
