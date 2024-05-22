using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControls : EventTrigger
{
    public static SwipeControls instance;

    float direction;

    private void Awake()
    {
        instance = this;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        float eventDelta = eventData.delta.x / 10;
        float touchInfo = Mathf.Abs(eventDelta);
        if (touchInfo > 0.8f)
        {
            direction = eventDelta ;
        }
        else
        {
            direction = 0;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        direction = 0; 
    }

    public float GetDirection()
    {
        return direction;
    }
}
