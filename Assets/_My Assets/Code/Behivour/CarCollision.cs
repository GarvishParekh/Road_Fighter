using UnityEngine;

public class CarCollision : MonoBehaviour
{
    UiManager uiManager;
    GameStatus gameStauts;

    [SerializeField] private CarData carData;
    [SerializeField] private TrafficData trafficData;

    private void Start()
    {
        uiManager = UiManager.instance;
        gameStauts = GameStatus.instance;

        trafficData.trafficStatus = TrafficData.TrafficStatus.MOVING;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("gameover");
            carData.carEngine = CarEngine.OFF;
            trafficData.trafficStatus = TrafficData.TrafficStatus.STATIC;

            uiManager.OpenCanvas(CanvasCellsName.GAMEOVER);

            gameStauts.ChangeGameState(GameState.GAMEOVER);
            GameStatus.GameOverAction?.Invoke();    
        }
    }
}
