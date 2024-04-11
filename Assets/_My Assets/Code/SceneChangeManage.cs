using UnityEngine;
using UnityEngine.SceneManagement;

public enum ScenesAvailable
{
    MAIN_MENU,
    GAMEPLAY
}

public class SceneChangeManage : MonoBehaviour
{

    private void ChangeScene(ScenesAvailable desireScene)
    {
        SceneManager.LoadScene((int)desireScene);
    }

    public void _ChangeToMainMenu()
    {
        ChangeScene(ScenesAvailable.MAIN_MENU);
    }
    public void _ChangeGameplayScene()
    {
        ChangeScene(ScenesAvailable.GAMEPLAY);
    }
}
