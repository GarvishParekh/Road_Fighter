using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SwipeControlValues))]
public class SwipeControls : EventTrigger
{
    SwipeControlValues settings;
    public static SwipeControls instance;

    float direction;
    float eventDelta;
    float touchInfo;

    private void Awake()
        => instance = this;

    private void Start()
        => settings = GetComponent<SwipeControlValues>();  

    public override void OnDrag(PointerEventData eventData)
    {
        eventDelta = eventData.delta.x / settings.controlSmoothness;
        touchInfo = Mathf.Abs(eventDelta);

        if (touchInfo > settings.deltaThreshold)
            direction = eventDelta ;
        else
            direction = 0;
    }

    public override void OnEndDrag(PointerEventData eventData)
        => direction = 0; 

    public float GetDirection()
    {
        return direction;
    }
}
