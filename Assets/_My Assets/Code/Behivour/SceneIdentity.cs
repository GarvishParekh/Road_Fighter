using JetBrains.Annotations;
using UnityEngine;

public enum SceneName
{ 
    MAIN_MENU,
    GAMEPLAY
}
public class SceneIdentity : MonoBehaviour
{
    public static SceneIdentity instance;
    [SerializeField] private SceneName sceneName;

    private void Awake()
    {
        instance = this;
    }

    public SceneName GetCurrentScene()
    {
        return sceneName;
    }
}
