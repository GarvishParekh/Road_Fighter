using UnityEngine;

public class CarCollision : MonoBehaviour
{
    AdsManager adsManager;
    UiManager uiManager;
    GameStatus gameStauts;

    [SerializeField] private CarData carData;
    [SerializeField] private TrafficData trafficData;

    private void Start()
    {
        uiManager = UiManager.instance;
        gameStauts = GameStatus.instance;
        adsManager = AdsManager.instance;

        trafficData.trafficStatus = TrafficData.TrafficStatus.MOVING;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameOverFunction();
        }
    }


    public void GameOverFunction()
    {
        if (carData.godMode == GodMode.ON)
        {
            return;   
        }
        if (gameStauts.GetGameState() == GameState.GAMEOVER)
        {
            return;
        }

        Debug.Log("gameover");
        carData.carEngine = CarEngine.OFF;
        trafficData.trafficStatus = TrafficData.TrafficStatus.STATIC;

        uiManager.OpenCanvas(CanvasCellsName.GAMEOVER);

        gameStauts.ChangeGameState(GameState.GAMEOVER);
        GameStatus.GameOverAction?.Invoke();
        Actions.Crash?.Invoke();

        // show ads
        if (adsManager !=null)
        {
            adsManager.ShowInterstitialAd();
        }
    }
}
