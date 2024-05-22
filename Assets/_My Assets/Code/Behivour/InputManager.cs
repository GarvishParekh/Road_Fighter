using UnityEngine;

public class InputManager : MonoBehaviour
{
    SwipeControls swipeControls;

    [SerializeField] private CarData carData;
    [SerializeField] private GameObject gyroCanvas;
    [SerializeField] private GameObject touchCanvas;
    [SerializeField] private GameObject swipeCanvas;

    float direction;
    float lerpedDirection;

    private void Start()
    {
        swipeControls = SwipeControls.instance;

        ControlsSetup();
    }

    private void ControlsSetup()
    {
        switch (carData.controlSystem)
        {
            case ControlSystem.KEYBOARD:
                swipeCanvas.SetActive(false);
            break;


            case ControlSystem.GRYO:
                gyroCanvas.SetActive(true);
            break;

            case ControlSystem.SWIPE:
                gyroCanvas.SetActive(true);
                swipeCanvas.SetActive(true);
            break;
        }
    }

    private void Update()
    {
        switch (carData.controlSystem)
        {
            case ControlSystem.KEYBOARD:
                direction = Input.GetAxisRaw("Horizontal");
                direction = Mathf.Clamp(direction, -carData.gyrpMaxVelocity, carData.gyrpMaxVelocity);
                lerpedDirection = Mathf.Lerp(lerpedDirection, direction, carData.gyroResponse);
            break;

            case ControlSystem.SWIPE:
                direction = swipeControls.GetDirection();
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
