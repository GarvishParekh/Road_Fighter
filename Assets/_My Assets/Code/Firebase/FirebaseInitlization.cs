using Firebase;
using UnityEngine;
using Firebase.Analytics;
using Firebase.Extensions;

public class FirebaseInitlization : MonoBehaviour
{
    public static FirebaseInitlization instance;

    private void Awake()
    {
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });
    }
}
