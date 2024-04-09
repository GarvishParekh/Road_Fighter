using UnityEngine;

public class Cars : MonoBehaviour
{
    [SerializeField] private int carIndex;

    private void Start()
    {
        carIndex = transform.GetSiblingIndex();
    }

    public int GetCarIndex()
    {
        return carIndex;    
    }
}
