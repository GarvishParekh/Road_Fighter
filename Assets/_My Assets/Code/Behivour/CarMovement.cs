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

    Vector3 pos;

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
        }
    }

    private void ApplyVelocity()
    {
        switch (gameScene)
        {
            case GameScene.MAIN_MENU:
                mainRB.velocity = Vector3.forward * carData.carMainMenuSpeed; 
                break;
            case GameScene.GAMEPLAY:
                mainRB.velocity = Vector3.forward * (carData.carSpeedLevel[(int)carData.currentSpeedLevel].speedValue + nosSystem.GetNosValue());
                break;
        }
    }

    private void ApplyTurning()
    {
        // clamp the position
        pos.x = Mathf.Clamp(carRB.position.x, -2, 2);
        pos.y = mainRB.position.y;
        pos.z = mainRB.position.z;

        switch (carControl)
        {
            case CarControl.ON:
                    carRB.velocity = Vector3.right * inputManager.GetLerpedDirection() * carData.touchSensitivity;
                break;
        }
        carRB.position = pos;
    }

    public void TakeControl()
    {
        carControl = CarControl.ON;
    }
}
