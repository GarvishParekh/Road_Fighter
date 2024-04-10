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
    [Header("<size=15>[SPEED VALUE]")]
    public float carHealth = 1;
    public float carMainMenuSpeed = 1.8f;
    public float gearShiftingSpeed = 5f;
    public float pickUpSpeed = 1f;
    
    [Header("<size=15>[SPEED LEVEL VALUE]")]
    public float speedLevelValue;
    public CurrentSpeedLevel currentSpeedLevel;
    public CarSpeedLevel[] carSpeedLevel;

    [Header("<size=15>[CONTOLS VALUE]")]
    public float touchSensitivity;
    public float gyroSensitivity;
    public float gyroResponse;
    public float gyrpMaxVelocity;
}

[System.Serializable]
public class CarSpeedLevel
{
    public string levelName;
    public float speedValue;
    
    public float minValue;
    public float maxValue;
}
