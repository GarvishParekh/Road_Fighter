using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public enum GameScene
    {
        MAIN_MENU,
        GAMEPLAY
    }

    [SerializeField] private GameScene gameScene;
    public enum CarControl
    {
        OFF,
        ON
    }
    [SerializeField] private CarControl carControl;
    Rigidbody mainRB;

    [Header(" [SCRIPTS] ")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private NosSystem nosSystem;

    [Header (" [SCRIPTABLE OBJECT] ")]
    [SerializeField] private CarData carData;

    [Header (" [COMPONENTS] ")]
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private ParticleSystem leftSideSparkParticle;
    [SerializeField] private ParticleSystem rightSideSparkParticle;

    Vector3 pos;
    public float lerpedSpeed = 0;
    public float lerpedCarMovement;

    private void Start()
    {
        Application.targetFrameRate = 60;
        mainRB = GetComponent<Rigidbody>();
        carData.currentSpeedLevel = 0;
        carData.carHealth = 1;

        EngineSetting();
    }

    private void EngineSetting()
    {
        switch (gameScene)
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
                CheckForSideDamage();
                break;
            case CarEngine.OFF:
                mainRB.velocity = Vector3.zero;
                carRB.velocity = Vector3.zero;
            break;
        }
    }

    private void ApplyVelocity()
    {
        switch (gameScene)
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

    private void CheckForSideDamage()
    {
        RightLaneCheck();
        LeftLaneCheck();
    }

    private void RightLaneCheck()
    {
        // damaging right side of the car
        if (carRB.transform.position.x > carData.maxSizeValues - 0.15f)
            rightSideSparkParticle.Play();
        else
            rightSideSparkParticle.Stop();
    }

    private void LeftLaneCheck()
    {
        // damaging left side of the car
        if (carRB.transform.position.x < -carData.maxSizeValues + 0.15f)
            leftSideSparkParticle.Play();
        else
            leftSideSparkParticle.Stop();
    }

    

}
