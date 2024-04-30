using UnityEngine;

public class CarSideDamage : MonoBehaviour
{
    GameStatus gameStatus;
    Transform carTransform;

    [Header ("<size=15>[SCRIPTS]")]
    [SerializeField] private InstructionManager instructionManager;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private CarData carData;
    [SerializeField] private CameraData cameraData;

    [Header ("<size=15>[PARTICLE SYSTEM]")]
    [SerializeField] private ParticleSystem rightSideSparkParticle;
    [SerializeField] private ParticleSystem leftSideSparkParticle;

    private void Start()
    {
        gameStatus = GameStatus.instance;
        carTransform = transform.GetChild(0);
    }

    private void Update()
    {
        switch (gameStatus.GetCurrentGameScene())
        {
            // if it's gameplay scene
            case GameScene.GAMEPLAY:
                // if it is turtiol skip car damage
                switch (instructionManager.GetInstructionState())
                {
                    case ShowInstruction.NO:
                        CheckForSideDamage();
                        break;
                }
            break;
        }
    }

    private void CheckForSideDamage()
    {
        if (gameStatus.GetGameState() == GameState.GAMEOVER)
        {
            cameraData.cameraShake = CameraShake.OFF;
            return;
        }

        RightLaneCheck();
        LeftLaneCheck();
        AppleCameraShake();
    }

    private void RightLaneCheck()
    {
        // damaging right side of the car
        if (carTransform.transform.position.x > carData.maxSizeValues - 0.15f)
            rightSideSparkParticle.Play();
        else
            rightSideSparkParticle.Stop();
    }

    private void LeftLaneCheck()
    {
        // damaging left side of the car
        if (carTransform.transform.position.x < -carData.maxSizeValues + 0.15f)
            leftSideSparkParticle.Play();
        else
            leftSideSparkParticle.Stop();
    }

    private void AppleCameraShake()
    {
        if (carTransform.transform.position.x > carData.maxSizeValues - 0.15f ||
            carTransform.transform.position.x < -carData.maxSizeValues + 0.15f)
        {
            carData.healthDepletion = HealthDepletion.ON;
            cameraData.cameraShake = CameraShake.ON;
        }

        else
        {
            carData.healthDepletion = HealthDepletion.OFF;
            cameraData.cameraShake = CameraShake.OFF;
        }
    }
}
