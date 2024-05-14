using TMPro;
using UnityEngine;
using System.Globalization;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    UiManager uiManager;

    [Header(" <size=15>[SCRIPTS]")]
    [SerializeField] private DailyRewards dailyRewards;

    [Header (" <size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private CarData carData;

    [Header (" <size=15>[UI]")]
    [SerializeField] private TMP_Text highscoreTxt;
    [SerializeField] private TMP_Text needMoreMoney;

    private void Start()
    {
        uiManager = UiManager.instance;
        Invoke(nameof(SetMainMenu), 0.5f);
    }

    private void SetMainMenu()
    {
        switch (dailyRewards.GetDailyRewardStatus())
        {
            case DailyRewardStatus.NOT_COLLECTED:
                uiManager.OpenCanvas(CanvasCellsName.DAILY_REWARD);
                break;
            case DailyRewardStatus.COLLECTED:
                uiManager.OpenCanvas(CanvasCellsName.MAIN_MENU);
                break;

        }
        SetHighscore();
    }

    public void _StartBtn()
    {
        carData.carEngine = CarEngine.ON;
        Invoke(nameof(ChangeScene), 1.5f);
        uiManager.OpenCanvas(CanvasCellsName.EMPTY);
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

    public void OpenGarage()
    {
        uiManager.OpenCanvas(CanvasCellsName.GARAGE);
    }

    public void OpenMainMenu()
    {
        uiManager.OpenCanvas(CanvasCellsName.MAIN_MENU);
    }

    public void OpenUpgradeCanvas()
    {
        uiManager.OpenCanvas(CanvasCellsName.POWERUP_UPGRADE);
    }

    public void OpenConfirmBuyCanvas()
    {
        uiManager.OpenPopupCanvas(CanvasCellsName.CONFIRM_BUY_POPUP);
    }

    public void NoEnoughCanvas(int price)
    {
        uiManager.OpenPopupCanvas(CanvasCellsName.NOT_ENOUGH_POPUP);
        needMoreMoney.text = $"You need <color=#F1C40F><b>{price.ToString("#,##0", CultureInfo.InvariantCulture)}</b></color> more coins to make this pruchase!";
    }

    public void OpenSettings()
    {
        uiManager.OpenCanvas(CanvasCellsName.SETTINGS);
    }
}
