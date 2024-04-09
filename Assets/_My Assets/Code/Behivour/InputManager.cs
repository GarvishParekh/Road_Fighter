using UnityEngine;

public class InputManager : MonoBehaviour
{
    private enum InputMethord
    {
        KEYBOARD,
        GYRO
    }
    [SerializeField] private InputMethord inputMethord;

    [SerializeField] private CarData carData;
    [SerializeField] private GameObject touchCanvas;

    float direction;
    float lerpedDirection;

    private void Update()
    {
        switch (inputMethord)
        {
            case InputMethord.KEYBOARD:
                direction = Input.GetAxisRaw("Horizontal");
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (inputMethord)
        {
            case InputMethord.GYRO:
                direction = Input.acceleration.x * carData.gyroSensitivity * Time.deltaTime;
                direction = Mathf.Clamp(direction, -carData.gyrpMaxVelocity, carData.gyrpMaxVelocity);
                lerpedDirection = Mathf.Lerp(lerpedDirection, direction, carData.gyroResponse);
                break;
        }
    }

    public float GetLerpedDirection()
    {
        return lerpedDirection;
    }
}
