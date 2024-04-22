using UnityEngine;

public enum CameraShake
{
    OFF,
    ON
}

[CreateAssetMenu(fileName = "Camera Data", menuName = "Camera Data")]
public class CameraData : ScriptableObject
{

    public CameraShake cameraShake;

    [Header ("<size=15>[CAMERA SETTINGS]")]
    public Vector3 cameraOffsetValue = new Vector3(0, 2, 5);
    public float cameraSmoothness = 200f;
    public float cameraShakeIntensity = 1.0f;
    public float cameraZValue = 5;
}
