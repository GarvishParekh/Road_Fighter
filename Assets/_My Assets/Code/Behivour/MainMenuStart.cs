using TMPro;
using UnityEngine;
using System.Globalization;
using UnityEngine.SceneManagement;

public class MainMenuStart : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private TMP_Text highscoreTxt;
    [SerializeField] private CarData carData;

    private void Start()
    {
        SetHighscore();
    }

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

    private void SetHighscore()
    {
        int highscoreCount = PlayerPrefs.GetInt(ConstantKeys.HIGHSCORE_COUNT, 0);
        highscoreTxt.text = highscoreCount.ToString("#,##0", CultureInfo.InvariantCulture);
    }
}
