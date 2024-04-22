using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private CarData carData;
    [SerializeField] private GameObject touchCanvas;

    float direction;
    float lerpedDirection;

    private void Update()
    {
        switch (carData.controlSystem)
        {
            case ControlSystem.KEYBOARD:
                direction = Input.GetAxisRaw("Horizontal");
                direction = Mathf.Clamp(direction, -carData.gyrpMaxVelocity, carData.gyrpMaxVelocity);
                lerpedDirection = Mathf.Lerp(lerpedDirection, direction, carData.gyroResponse);
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (carData.controlSystem)
        {
            case ControlSystem.GRYO:
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
