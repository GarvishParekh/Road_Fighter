using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    BackgroundMusic backgroundMusic;
    SFXPlayer sfxPlayer;

    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private SettingsData settingsData;
    [SerializeField] private CarData carData;

    [Header("<size=15>[TOGGLES]")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle tiltToggle;
    [SerializeField] private Toggle touchToggle;

    private void Start()
    {
        backgroundMusic = BackgroundMusic.instance;
        sfxPlayer = SFXPlayer.instance; 
        SetupSettings();
        ControlSetup();
    }

    private void SetupSettings()
    {
        MusicSetup();
        SFXSetup();
    }

    private void MusicSetup()
    {
        int musicStatus = PlayerPrefs.GetInt(ConstantKeys.SETTINGS_MUSIC, 1);

        if (musicStatus == 0 )
        {
            musicToggle.isOn = false;
            settingsData.backgroundMusicStatus = BackgroundMusicStatus.OFF;
        }
        else if (musicStatus == 1)
        {
            musicToggle.isOn = true;
            settingsData.backgroundMusicStatus = BackgroundMusicStatus.ON;
        }

        backgroundMusic.TurnMusic(musicToggle.isOn);
    }

    private void SFXSetup()
    {
        int sfxStatus = PlayerPrefs.GetInt(ConstantKeys.SFX_Settings, 1);

        if (sfxStatus == 0)
        {
            sfxToggle.isOn = false;
            settingsData.sfxStatus = SFXStatus.OFF;
        }
        else if (sfxStatus == 1)
        {
            sfxToggle.isOn = true;
            settingsData.sfxStatus = SFXStatus.ON;
        }

        sfxPlayer.TurnSFX(sfxToggle.isOn);
    }

    public void ChangeMusicToggle()
    {
        if (musicToggle.isOn)
        {
            settingsData.backgroundMusicStatus = BackgroundMusicStatus.ON;
            backgroundMusic.TurnMusic(true);
            PlayerPrefs.SetInt(ConstantKeys.SETTINGS_MUSIC, 1);
        }
        else if (!musicToggle.isOn)
        {
            settingsData.backgroundMusicStatus = BackgroundMusicStatus.OFF;
            backgroundMusic.TurnMusic(false);
            PlayerPrefs.SetInt(ConstantKeys.SETTINGS_MUSIC, 0);
        }
    }

    public void ChangeSFXToggle()
    {
        if (sfxToggle.isOn)
        {
            settingsData.sfxStatus = SFXStatus.ON;
            sfxPlayer.TurnSFX(true);
            PlayerPrefs.SetInt(ConstantKeys.SFX_Settings, 1);
        }
        else if (!sfxToggle.isOn)
        {
            settingsData.sfxStatus = SFXStatus.OFF;
            sfxPlayer.TurnSFX(false);
            PlayerPrefs.SetInt(ConstantKeys.SFX_Settings, 0);
        }
    }

    public void ChangeControls()
    {
        if (tiltToggle.isOn)
        {
            carData.controlSystem = ControlSystem.GRYO;
            PlayerPrefs.SetInt(ConstantKeys.CONTROLS_SETTINGS, 0);
        }
        else if (touchToggle.isOn)
        {
            carData.controlSystem = ControlSystem.SWIPE;
            PlayerPrefs.SetInt(ConstantKeys.CONTROLS_SETTINGS, 1);
        }
    }

    private void ControlSetup()
    {
        int controlIndex = PlayerPrefs.GetInt(ConstantKeys.CONTROLS_SETTINGS, 0);

        switch (controlIndex)
        {
            case 0:
                tiltToggle.isOn = true;
                carData.controlSystem = ControlSystem.GRYO;
                break;
            case 1:
                touchToggle.isOn = true;
                carData.controlSystem = ControlSystem.SWIPE;
                break;
        }
    }
}
