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

public enum ControlSystem
{
    GRYO,
    SLIDER,
    KEYBOARD
}

public enum HealthDepletion
{
    OFF,
    ON
}

public enum GodMode
{
    OFF,
    ON
}

[CreateAssetMenu(fileName = "Car Data", menuName = "Car Data")]
public class CarData : ScriptableObject
{
    public GodMode godMode;
    public CarEngine carEngine;
    public ControlSystem controlSystem;
    public HealthDepletion healthDepletion;

    [Range (-2, 2)]
    public float controlSlider = -4;

    [Header("<size=15>[CAR SETTINGS]")]
    public float healthDepletionRate = 0.1f;
    public float maxSizeValues = 2.0f;


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


