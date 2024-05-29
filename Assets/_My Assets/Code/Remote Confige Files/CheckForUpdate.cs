using UnityEngine;

using Unity.Services.Core;
using Unity.Services.RemoteConfig;

using System.Threading.Tasks;
using Unity.Services.Authentication;

public class CheckForUpdate : MonoBehaviour
{
    [Header("<size=15>[SCRIPTS]")]
    [SerializeField] private MainMenuUIManager mainMenuUiManager;

    public static CheckForUpdate instance;

    public float myGameVersion = 2.0f;

    public bool update_Check_RC = true;
    public float android_Version_RC = 1.0f;
    public float iOS_Version_RC = 1.0f;

    float retrivedVersion = 0;

    public struct userAttributes { }

    public struct appAttributes
    {
        public bool updateCheck;
        public int androidAppVersion;
        public int iOSAppVersion;
    }

    async Task InitilizeRemoteConfigAsync()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);  
    }

    async void Start()
    {
        await InitilizeRemoteConfigAsync();

        appAttributes aaStruct = new appAttributes();

        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes() { }, aaStruct);

        RemoteConfigService.Instance.FetchCompleted += RemoteConfigLoaded;
    }

    private void RemoteConfigLoaded (ConfigResponse configResponse)
    {
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Remote:

                update_Check_RC = RemoteConfigService.Instance.appConfig.GetBool("Version_Check");
                if (update_Check_RC)
                {
#if UNITY_IPHONE
                    iOS_Version_RC = RemoteConfigService.Instance.appConfig.GetFloat("iOS_Version");
                    retrivedVersion = iOS_Version_RC;

#elif UNITY_ANDROID
                    android_Version_RC = RemoteConfigService.Instance.appConfig.GetFloat("Android_Version");
                    retrivedVersion = android_Version_RC;

                    # endif

                    if (retrivedVersion != myGameVersion)
                    {
                        Debug.Log("Version Not matched");
                        mainMenuUiManager.OpenUpdateCanvas();
                    }
                    else if (retrivedVersion == myGameVersion)
                    {
                        Debug.Log("Update-to-date");
                    }
                    Debug.Log("Version check complete");
                }
            break;
        }
    }
}
