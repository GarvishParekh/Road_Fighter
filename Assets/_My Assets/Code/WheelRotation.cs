using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    [SerializeField] private CarData carData;
    [SerializeField] private float rotationSpeed = 12;

    private void Update()
    {
        switch (carData.carEngine)
        {
            case CarEngine.ON:
                transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
            break;
        }
    }
}
