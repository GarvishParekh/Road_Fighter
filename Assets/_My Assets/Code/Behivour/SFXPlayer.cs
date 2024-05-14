using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance;   

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private SoundData soundData;
    [SerializeField] private SettingsData settingsData;

    [Header ("<size=15>[COMPONENTS]")]
    [SerializeField] private AudioSource sfxAudioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        AudioSourceSetup();
    }

    private void PlaySound(AudioClip audioToPlay)
    {
        switch(settingsData.sfxStatus)
        {
            case SFXStatus.ON:
                sfxAudioSource.PlayOneShot(audioToPlay);
                break;
        }
    }

    private void AudioSourceSetup()
    {
        sfxAudioSource.volume = soundData.SFXVolume;
    }

    public void PlayWooshSound()
    {
        PlaySound(soundData.whooshSFX);
    }

    public void TurnSFX(bool check)
    {
        sfxAudioSource.enabled = check;
    }
}
