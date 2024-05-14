using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private SoundData soundData;
    [SerializeField] private SettingsData settingsData;
    

    [Header("<size=15>[COMPONENTS]")]
    [SerializeField] private AudioSource backgroundAudioSource;

    int musicToPlay = 0;

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
        SceneManager.sceneLoaded += SetupAudioSourceAndPlay;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetupAudioSourceAndPlay;
    }


    private void SetupAudioSourceAndPlay(Scene currentScene, LoadSceneMode mode)
    {
        switch (currentScene.buildIndex)
        {
            case 0:
                StartCoroutine(nameof(ChangeMusic), BackgroundMusicType.MAINMENU);
                break;
            case 1:
                StartCoroutine(nameof(ChangeMusic), BackgroundMusicType.GAMEPLAY);
                break;
        }
    }

    /*
    private void ChangeMusic(BackgroundMusicType musicType)
    {
        backgroundAudioSource.Stop();

        backgroundAudioSource.volume = soundData.musicVolume;
        switch (musicType)
        {
            case BackgroundMusicType.MAINMENU:
                musicToPlay = Random.Range(0, soundData.mainMenuMusic.Count);
                backgroundAudioSource.clip = soundData.mainMenuMusic[musicToPlay];
                break;
            case BackgroundMusicType.GAMEPLAY:
                musicToPlay = Random.Range(0, soundData.gameplayMusic.Count);
                backgroundAudioSource.clip = soundData.gameplayMusic[musicToPlay];
                break;
        }
        backgroundAudioSource.Play();
    }
    */
    private IEnumerator ChangeMusic(BackgroundMusicType musicType)
    {
        float newVolume = backgroundAudioSource.volume;
        
        while (newVolume > 0)
        {
            newVolume -= Time.deltaTime * soundData.soundBuffer;
            backgroundAudioSource.volume = newVolume;
            yield return null;
        }

        switch (musicType)
        {
            case BackgroundMusicType.MAINMENU:
                musicToPlay = Random.Range(0, soundData.mainMenuMusic.Count);
                backgroundAudioSource.clip = soundData.mainMenuMusic[musicToPlay];
                break;
            case BackgroundMusicType.GAMEPLAY:
                musicToPlay = Random.Range(0, soundData.gameplayMusic.Count);
                backgroundAudioSource.clip = soundData.gameplayMusic[musicToPlay];
                break;
        }
        backgroundAudioSource.Play();
        float maxVolume = soundData.musicVolume;

        while (newVolume <= maxVolume)
        {
            newVolume += Time.deltaTime * soundData.soundBuffer;
            backgroundAudioSource.volume = newVolume;
            yield return null;
        }
    }


    public void TurnMusic(bool check)
    {
        backgroundAudioSource.enabled = check; 
    }
}