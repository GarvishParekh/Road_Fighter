using UnityEngine;

[CreateAssetMenu(fileName = "Car Data", menuName = "Car Data")]
public class CarData : ScriptableObject
{
    public float carSpeed;
    public float touchSensitivity;
    [Space]    
    
    public float gyroSensitivity;
    public float gyroResponse;
    public float gyrpMaxVelocity;
}
