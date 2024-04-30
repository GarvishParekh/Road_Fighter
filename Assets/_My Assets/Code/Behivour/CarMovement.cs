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


    Vector3 pos;
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
        lerpedSpeed = Mathf.MoveTowards(lerpedSpeed, 1, Time.deltaTime * carData.pickUpSpeed);
        return lerpedSpeed; 
    }

    private float GetLerpedMovementValue()
    {
        lerpedCarMovement = Mathf.MoveTowards(lerpedCarMovement, carData.carSpeedLevel[(int)carData.currentSpeedLevel].speedValue, carData.gearShiftingSpeed * Time.deltaTime);
        return lerpedCarMovement;
    }

    private void ApplyTurning()
    {
        // clamp the position
        pos.x = Mathf.Clamp(carRB.position.x, -carData.maxSizeValues, carData.maxSizeValues);
        pos.y = mainRB.position.y;
        pos.z = mainRB.position.z;

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

                break;
        }
        carRB.position = pos;
    }

    public void TakeControl()
    {
        carControl = CarControl.ON;
    }
}
