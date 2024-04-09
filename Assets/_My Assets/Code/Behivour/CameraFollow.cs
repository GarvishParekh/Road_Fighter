using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [Header(" [COMPONENTS] ")]
    [SerializeField] CarMovement carMovement;

    [Header (" [COMPONENTS] ")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform targetTransform;
    
    [Header (" [VALUES] ")]
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float cameraSmoothness = 3;

    public void Start()
    {
        StartCoroutine(nameof(StartOff));
    }

    private void LateUpdate()
    {
        Vector3 finalPosition = targetTransform.position + cameraOffset;
        cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, finalPosition, Time.deltaTime * cameraSmoothness);
    }

    private IEnumerator StartOff()
    {
        while (cameraOffset.z > -4.5)
        {
            cameraOffset.z = Mathf.MoveTowards(cameraOffset.z, -4.5f, Time.deltaTime * 5);
            yield return null;  
        }
        carMovement.TakeControl();
    }
}
