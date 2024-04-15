using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuStart : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private CarData carData;
    
    public void _StartBtn()
    {
        mainMenuCanvas.SetActive(false);
        carData.carEngine = CarEngine.ON;
        Invoke(nameof(ChangeScene), 1.5f);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
