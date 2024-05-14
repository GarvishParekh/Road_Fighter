using UnityEngine;

public enum BackgroundMusicStatus
{
    ON,
    OFF
}

public enum SFXStatus
{
    ON,
    OFF
}

[CreateAssetMenu (fileName = "Settings Data", menuName = "Settings Data")]
public class SettingsData : ScriptableObject
{
    public BackgroundMusicStatus backgroundMusicStatus;
    public SFXStatus sfxStatus;
}
