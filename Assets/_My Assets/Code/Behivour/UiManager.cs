using UnityEngine;
using System.Collections.Generic;

public enum CanvasCellsName
{
    GAMEPLAY,
    GAMEOVER,
    TILT_INS,
    HOLD_INS,
    ALL_SET,
    MAIN_MENU,
    EMPTY,
    GARAGE,
    DAILY_REWARD,
    DAILY_REWARD_COLLECTED,
    POWERUP_UPGRADE,
    CONFIRM_BUY_POPUP,
    NOT_ENOUGH_POPUP,
    SETTINGS,
    UPDATE_AVAILABLE,
    DOUBLE_COINS_DONE
}
public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [SerializeField] private List<CanvasCell> canvasCells = new List<CanvasCell>();
    [SerializeField] private CanvasCell confirmBuyCanvas;

    private void Awake()
    {
        instance = this;
    }

    public void OpenCanvas(CanvasCellsName canvasToOpen)
    {
        foreach (var cell in canvasCells)
        {
            if (canvasToOpen == cell.GetCanvasName())
            {
                cell.OpenCanvas();
            }
            else
            {
                cell.CloseCanvas();
            }
        }
    }

    public void OpenPopupCanvas(CanvasCellsName canvasToOpen)
    {
        foreach (var cell in canvasCells)
        {
            if (canvasToOpen == cell.GetCanvasName())
            {
                cell.OpenCanvas();
            }
        }
    }
}
