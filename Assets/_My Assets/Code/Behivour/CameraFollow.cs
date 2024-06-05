using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    UiManager uiManager;

    [Header(" <size=15>[SCRIPTABLE OBEJCT] ")]
    [SerializeField] private CameraData cameraData;

    [Header(" [SCRIPTS] ")]
    [SerializeField] CarMovement carMovement;
    [SerializeField] InstructionManager instructionManager;
    [SerializeField] GameStatus gameStatus;

    [Header (" [COMPONENTS] ")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform cameraShakeTransform;

    [Header (" [DYNAMIC CAMERA] ")]
    [SerializeField] private Transform carHolder;
    [SerializeField] private Transform cameraHolderTransform;
    [SerializeField] private Transform lookAtTransform;

    public void Start()
    {
        uiManager = UiManager.instance;
        cameraData.cameraOffsetValue.z = cameraData.cameraZValue;
        StartCoroutine(nameof(StartOff));

        // disable camera shake
        cameraData.cameraShake = CameraShake.OFF;

    }

    private void LateUpdate()
    {
        Vector3 finalPosition = targetTransform.position + cameraData.cameraOffsetValue;
        cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, finalPosition, Time.deltaTime * cameraData.cameraSmoothness);

        CameraShakeEffect();
        DynamicCamera();
    }

    private IEnumerator StartOff()
    {
        while (cameraData.cameraOffsetValue.z > -4.5)
        {
            cameraData.cameraOffsetValue.z = Mathf.MoveTowards(cameraData.cameraOffsetValue.z, -4.5f, Time.deltaTime * 5);
            yield return null;  
        }
        carMovement.TakeControl();
        instructionManager.ShowFirstCanvas();
    }

    public void ChangeCameraSmoothness(float newValue)
    {
        cameraData.cameraSmoothness = newValue;
    }

    private void CameraShakeEffect()
    {
        if (gameStatus.GetGameState() == GameState.GAMEOVER)
        {
            return;
        }
        switch (cameraData.cameraShake)
        {
            case CameraShake.ON:
                cameraShakeTransform.localPosition = (Random.insideUnitSphere * cameraData.cameraShakeIntensity * Time.deltaTime);
                break;
            case CameraShake.OFF:
                cameraShakeTransform.localPosition = Vector3.zero;
                break;
        }
    }

    Vector3 dynamicVector = Vector3.zero;
    private void DynamicCamera()
    {
        dynamicVector = transform.localPosition;
        dynamicVector.x = -(carHolder.localPosition.x / 20);

        cameraHolderTransform.localPosition = dynamicVector;
        //cameraHolderTransform.LookAt(lookAtTransform);    
    }
}
