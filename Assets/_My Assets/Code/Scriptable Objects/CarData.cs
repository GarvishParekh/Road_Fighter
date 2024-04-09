using UnityEngine;

public enum CurrentSpeedLevel
{
    Relaxed,
    Swift, 
    Turbo,
    Hypersonic,
    Insane
}

public enum CarEngine
{
    OFF,
    ON
}
[CreateAssetMenu(fileName = "Car Data", menuName = "Car Data")]
public class CarData : ScriptableObject
{
    public CarEngine carEngine;
    public float carHealth = 1;
    public float carMainMenuSpeed = 1.8f;
    public CurrentSpeedLevel currentSpeedLevel;
    public CarSpeedLevel[] carSpeedLevel;
    public float touchSensitivity;
    [Space]    
    
    public float gyroSensitivity;
    public float gyroResponse;
    public float gyrpMaxVelocity;
}

[System.Serializable]
public class CarSpeedLevel
{
    public string levelName;
    public float speedValue;
}
