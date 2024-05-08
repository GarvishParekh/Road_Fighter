using UnityEngine;

public class CarCollision : MonoBehaviour
{
    UiManager uiManager;
    GameStatus gameStauts;
    EconomyManager economyManager;

    [SerializeField] private CarData carData;
    [SerializeField] private TrafficData trafficData;

    [SerializeField] private InGameCoinCollection inGameCoinCollection;

    private void Start()
    {
        uiManager = UiManager.instance;
        gameStauts = GameStatus.instance;
        economyManager = EconomyManager.instance;

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

        economyManager.AddCoins(inGameCoinCollection.GetCollectedCoins());
    }
}
