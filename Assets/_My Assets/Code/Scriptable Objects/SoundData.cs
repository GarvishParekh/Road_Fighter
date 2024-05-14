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
}
