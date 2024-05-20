using UnityEngine;
using System.Collections.Generic;



public enum BackgroundMusicType
{
    MAINMENU,
    GAMEPLAY
}

[CreateAssetMenu (fileName = "Sound Data", menuName = "Sound Data")]
public class SoundData : ScriptableObject
{
    public BackgroundMusicType backgroundMusicType;

    [Header ("<size=15>[MUSIC LIST]")]
    public List<AudioClip> mainMenuMusic = new List<AudioClip>();
    public List<AudioClip> gameplayMusic = new List<AudioClip>();

    [Header ("<size=15>[VALUES]")]
    public float musicVolume = 0.5f;
    public float SFXVolume = 0.5f;
    public float soundBuffer = 0.2f;

    [Header("<size=15>[SFX]")]
    public AudioClip whooshSFX;

    [Header("<size=15>[ENGINE SETTINGS]")]
    public CarEngineSound carEngineSound;
    public EngineSoundSettings engineSoundSettings;
    public float eningeSFXVolume = 0.1f;
    public float enginePitch = 1.2f;
}

[System.Serializable]
public class CarEngineSound
{
    public AudioClip AClass_A;
    public AudioClip AClass_B;

    [Space]
    public AudioClip BClass_A;
    public AudioClip BClass_B;

    [Space]
    public AudioClip CClass_A;
    public AudioClip CClass_B;

    [Space]
    public AudioClip SClass_A;
}

[System.Serializable]
public class EngineSoundSettings
{
    public float engineStartingPitch = 0.35f;
    public float engineMaxPitch = 2.5f;
    public float soundMultiplier = 0.0005f;
}
