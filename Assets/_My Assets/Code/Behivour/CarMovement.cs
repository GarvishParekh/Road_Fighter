using UnityEngine;

public enum GameScene
{
    MAIN_MENU,
    GAMEPLAY
}

public class CarMovement : MonoBehaviour
{

    
    public enum CarControl
    {
        OFF,
        ON
    }
    [SerializeField] private CarControl carControl;
    Rigidbody mainRB;

    GameStatus gameStatus;
    
    [Header(" [SCRIPTS] ")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private NosSystem nosSystem;
    [SerializeField] private InstructionManager instructionManager;

    [Header (" [SCRIPTABLE OBJECT] ")]
    [SerializeField] private CarData carData;
    [SerializeField] private CameraData cameraData;

    [Header (" [COMPONENTS] ")]
    [SerializeField] private Rigidbody carRB;


    Vector3 turningVector;
    public float lerpedSpeed = 0;
    public float lerpedCarMovement;

    private void Start()
    {
        gameStatus = GameStatus.instance;

        Application.targetFrameRate = 60;
        mainRB = GetComponent<Rigidbody>();
        carData.currentSpeedLevel = 0;
        carData.carHealth = 1;

        EngineSetting();
        
    }

    private void EngineSetting()
    {
        switch (gameStatus.GetCurrentGameScene())
        {
            case GameScene.MAIN_MENU:
                carData.carEngine = CarEngine.OFF;
                break;
            case GameScene.GAMEPLAY:
                carData.carEngine = CarEngine.ON;
                lerpedCarMovement = carData.carSpeedLevel[0].speedValue;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (carData.carEngine)
        {
            case CarEngine.ON:
                ApplyVelocity();
                ApplyTurning();

               
                break;
            case CarEngine.OFF:
                mainRB.velocity = Vector3.zero;
                carRB.velocity = Vector3.zero;
            break;
        }
    }

    private void ApplyVelocity()
    {
        switch (gameStatus.GetCurrentGameScene())
        {
            case GameScene.MAIN_MENU:
                mainRB.velocity = Vector3.forward * carData.carMainMenuSpeed * GetPickUpLerp(); 
                break;
            case GameScene.GAMEPLAY:
                mainRB.velocity = Vector3.forward * (GetLerpedMovementValue() + nosSystem.GetNosValue());
                break;
        }
    }

    private float GetPickUpLerp()
    {
        float pickUpSpeed = Time.deltaTime * carData.pickUpSpeed;
        lerpedSpeed = Mathf.MoveTowards(lerpedSpeed, 1, pickUpSpeed);
        return lerpedSpeed; 
    }

    private float GetLerpedMovementValue()
    {
        float requriedSpeed = carData.carSpeedLevel[(int)carData.currentSpeedLevel].speedValue;
        float shiftingSpeed = carData.gearShiftingSpeed * Time.deltaTime;
        
        lerpedCarMovement = Mathf.MoveTowards(lerpedCarMovement, requriedSpeed, shiftingSpeed);
        return lerpedCarMovement;
    }

    private void ApplyTurning()
    {
        // clamp the position
        turningVector.x = Mathf.Clamp(carRB.position.x, -carData.maxSizeValues, carData.maxSizeValues);
        turningVector.y = mainRB.position.y;
        turningVector.z = mainRB.position.z;

        switch (carControl)
        {
            case CarControl.ON:
                switch (carData.controlSystem)
                {
                    case ControlSystem.GRYO:
                        carRB.velocity = Vector3.right * inputManager.GetLerpedDirection() * carData.touchSensitivity;
                        break;
                    case ControlSystem.KEYBOARD:
                        carRB.velocity = Vector3.right * inputManager.GetLerpedDirection() * carData.touchSensitivity;
                        break;
                }

                CarRotation();
            break;
        }
        carRB.position = turningVector;
    }

    Vector3 carRotationVec = Vector3.zero;
    private void CarRotation()
    {
        carRotationVec = carRB.transform.rotation.eulerAngles;
        carRotationVec.y = inputManager.GetLerpedDirection() * 8;
        carRB.transform.rotation = Quaternion.Euler(carRotationVec);
    }

    public void TakeControl()
    {
        carControl = CarControl.ON;
    }
}
