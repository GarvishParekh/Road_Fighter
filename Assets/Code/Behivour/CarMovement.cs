using UnityEngine;

public class CarMovement : MonoBehaviour
{
    Rigidbody mainRB;
    
    private enum InputMethord
    {
        KEYBOARD,
        TOUCH,
        GYRO
    }
    [SerializeField] private InputMethord inputMethord; 

    [Header (" [SCRIPTABLE OBJECT] ")]
    [SerializeField] private CarData carData;

    [Header (" [COMPONENTS] ")]
    [SerializeField] private Rigidbody carRB;

    [Header(" [USER INTERFACE] ")]
    [SerializeField] private GameObject touchCanvas;

    float direction;
    float lerpedDirection;
    Vector3 pos;

    private void Start()
    {
        Application.targetFrameRate = 60;
        mainRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (inputMethord)
        {
            case InputMethord.KEYBOARD:
                direction = Input.GetAxisRaw("Horizontal");
                break;
            case InputMethord.TOUCH:
                touchCanvas.SetActive(true);
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

        mainRB.velocity = Vector3.forward * carData.carSpeed;
        
        pos.x = Mathf.Clamp(carRB.position.x, -2, 2);
        pos.y = mainRB.position.y;
        pos.z = mainRB.position.z;

        carRB.velocity = Vector3.right * lerpedDirection * carData.touchSensitivity;
        carRB.position = pos;
    }

    public void PointDownLeft()
        => direction = -1;
    public void PointUpLeft()
        => direction = 0;
    public void PointDownRight()
        => direction = 1;
    public void PointUpRight()
        => direction = 0;
}
