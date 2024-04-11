using UnityEngine;

public enum ShowInstruction
{
    YES,
    NO
}

public class InstructionManager : MonoBehaviour
{
    UiManager uiManager;
    [SerializeField] private ShowInstruction showInstruction;
    [SerializeField] private GameObject trafficHolder;

    [Header(" <size=15>[COMPONENETS] ")]
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Collider levelShiftCollider;

    [Header (" <size=15>[UI] ")]
    [SerializeField] private GameObject controlCanvas;

    private void Awake()
    {
        showInstruction = (ShowInstruction)PlayerPrefs.GetInt(ConstantKeys.SHOW_INSTRUCTION, 0);
    }

    private void Start()
    {
        uiManager = UiManager.instance;
    }

    public void ShowFirstCanvas()
    {
        switch (showInstruction)
        {
            case ShowInstruction.YES:
                uiManager.OpenCanvas(CanvasCellsName.TILT_INS);
                trafficHolder.SetActive(false);
                controlCanvas.SetActive(false);
                break;
            case ShowInstruction.NO:
                uiManager.OpenCanvas(CanvasCellsName.GAMEPLAY);
                break;
        }
    }

    public void _ShowNextInstruction()
    {
        uiManager.OpenCanvas(CanvasCellsName.HOLD_INS);
    }

    public void _ShowAllSetCanvas()
    {
        cameraFollow.ChangeCameraSmoothness(2);
        levelShiftCollider.enabled = false;
        uiManager.OpenCanvas(CanvasCellsName.ALL_SET);
        PlayerPrefs.SetInt(ConstantKeys.SHOW_INSTRUCTION, 1);
    }
}
