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

    private void OnEnable()
    {
        Actions.SpeedLevelIncreased += IncreaseEnginePitch;
    }

    private void OnDisable()
    {
        Actions.SpeedLevelIncreased -= IncreaseEnginePitch;
    }

    private void Start()
    {
        sfxPlayer = SFXPlayer.instance;
        engineAudioSource = sfxPlayer.GetEngineAudoiSource();   

        engineAudioSource.pitch = engineSettings.engineStartingPitch;
        //StartCoroutine(nameof(IEEnginePitcher));
    }

    private void IncreaseEnginePitch()
    {
        StartCoroutine(nameof(IncreasePitch));
    }

    private IEnumerator IncreasePitch()
    {
        float desirePitch = engineAudioSource.pitch + 0.3f;

        // set engine max pitch 
        if (desirePitch > engineSettings.engineMaxPitch)
            desirePitch = engineSettings.engineMaxPitch;

        // rev the engine
        while (engineAudioSource.pitch <= engineSettings.engineRevPitch)
        {
            engineAudioSource.pitch += engineSettings.revSpeed * Time.deltaTime;
            yield return null;  
        }

        // get engine to it deisre pitch
        while (engineAudioSource.pitch >= desirePitch)
        {
            engineAudioSource.pitch -= engineSettings.normalSpeed * Time.deltaTime;
            yield return null;  
        }
    }

    /*
    private IEnumerator IEEnginePitcher()
    {
        while (engineAudioSource.pitch < engineSettings.engineMaxPitch)
        {
            engineAudioSource.pitch += Time.deltaTime * engineSettings.soundMultiplier;
            yield return null;
        }
    }

    private void SpeedChange()
        => StopCoroutine(nameof(IERevEngine)); 

    private void RevEngine()
    {
        Debug.Log("Engine Rev");
        StartCoroutine(nameof(IERevEngine));    
    }

    private IEnumerator IERevEngine()
    {
        StopCoroutine("IEEnginePitcher"); 
        float lastEnginePitch = engineAudioSource.pitch;
        float counterTime = 0;
        while (counterTime < 2)
        {
            Debug.Log("Reving Engine");
            if (engineAudioSource.pitch < 2)
            {
                engineAudioSource.pitch += 8 * Time.deltaTime;
            }
            else
            {
                float randSound = Random.Range(3, 3.5f);
                engineAudioSource.pitch = randSound;
            }
            counterTime += Time.deltaTime;  
            yield return null;
        }

        while (engineAudioSource.pitch > lastEnginePitch)
        {
            Debug.Log("Reving Engine");
            if (engineAudioSource.pitch > lastEnginePitch)
            {
                engineAudioSource.pitch -= 2 * Time.deltaTime;
            }
            yield return null;
        }
        engineAudioSource.pitch = lastEnginePitch;
        StartCoroutine (nameof(IEEnginePitcher));   
    }
    */
}
