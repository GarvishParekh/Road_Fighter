using UnityEngine;
using System.Collections.Generic;

public enum CanvasCellsName
{
    GAMEPLAY,
    GAMEOVER,
    TILT_INS,
    HOLD_INS,
    ALL_SET
}
public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [SerializeField] private List<CanvasCell> canvasCells = new List<CanvasCell>();

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
}
