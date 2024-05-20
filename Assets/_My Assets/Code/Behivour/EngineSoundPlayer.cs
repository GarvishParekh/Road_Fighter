using UnityEngine;
using System.Collections;

public class EngineSoundPlayer : MonoBehaviour
{
    SFXPlayer sfxPlayer;

    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private SoundData soundData;

    [Header("<size=15>[COMPONENTS]")]
    [SerializeField] private AudioSource engineAudioSource;

    EngineSoundSettings engineSettings;

    private void Awake()
        => engineSettings = soundData.engineSoundSettings;

    private void Start()
    {
        sfxPlayer = SFXPlayer.instance;
        engineAudioSource = sfxPlayer.GetEngineAudoiSource();   

        StartCoroutine(nameof(EnginePitcher));
    }

    private IEnumerator EnginePitcher()
    {
        engineAudioSource.pitch = engineSettings.engineStartingPitch;
        while (engineAudioSource.pitch < engineSettings.engineMaxPitch)
        {
            engineAudioSource.pitch += Time.deltaTime * engineSettings.soundMultiplier;
            yield return null;
        }
    }
}
