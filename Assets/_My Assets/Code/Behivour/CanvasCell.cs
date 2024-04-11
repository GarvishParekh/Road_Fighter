using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasCell : MonoBehaviour, ICanvasCell
{
    ICanvasCellAnimation canvasAnimation;
    CanvasGroup canvasGroup;
    [SerializeField] private CanvasCellsName myCanvasCellName;


    private void Start()
    {
        canvasAnimation = GetComponent<ICanvasCellAnimation>(); 
        canvasGroup = GetComponent<CanvasGroup>();  
    }

    public void OpenCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasAnimation?.PlayAnimation();
    }
    public void CloseCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public CanvasCellsName GetCanvasName()
    {
        return myCanvasCellName;
    }
}


