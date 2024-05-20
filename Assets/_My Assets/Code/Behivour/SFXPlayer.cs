using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance;

    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private CarStoreData carStoreData;
    [SerializeField] private SoundData soundData;
    [SerializeField] private SettingsData settingsData;

    [Header ("<size=15>[COMPONENTS]")]
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource engineAudioSource;

    AudioClip engineAudioClip;
    int buildIndex;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += PlayEngineSound;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= PlayEngineSound;
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

    private void PlayEngineSound(Scene _scene, LoadSceneMode _loadMode)
    {
        buildIndex = _scene.buildIndex;
        if (buildIndex == 1)
        {
            SetEngineSound();
        }
        else
        {
            engineAudioSource.Stop();
        }
    }

    private void SetEngineSound()
    {
        int equippedIndex = carStoreData.equippedCarData.equippedCarIndex;
        switch (carStoreData.equippedCarData.equippedCarclass)
        {
            case CarsClass.S_CLASS:
                engineAudioClip = soundData.carEngineSound.SClass_A;
                break;
            case CarsClass.A_CLASS:
                if (equippedIndex % 2 == 0)
                    engineAudioClip = soundData.carEngineSound.AClass_A;
                else
                    engineAudioClip = soundData.carEngineSound.AClass_B;
                break;
            case CarsClass.B_CLASS:
                if (equippedIndex % 2 == 0)
                    engineAudioClip = soundData.carEngineSound.BClass_A;
                else
                    engineAudioClip = soundData.carEngineSound.BClass_B;
                break;
            case CarsClass.C_CLASS:
                if (equippedIndex % 2 == 0)
                    engineAudioClip = soundData.carEngineSound.CClass_A;
                else
                    engineAudioClip = soundData.carEngineSound.CClass_B;
                break;
        }

        engineAudioSource.clip = engineAudioClip;
        engineAudioSource.volume = soundData.eningeSFXVolume;
        engineAudioSource.loop = true;
        engineAudioSource.Play();
    }

    public AudioSource GetEngineAudoiSource()
    {
        return engineAudioSource;
    }
}
