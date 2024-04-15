using UnityEngine;

public class CarCollision : MonoBehaviour
{
    UiManager uiManager;
    GameStatus gameStauts;

    [SerializeField] private CarData carData;
    [SerializeField] private TrafficData trafficData;
    [SerializeField] private GameObject gameoverCanvas;

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
            //gameoverCanvas.SetActive(true);

            uiManager.OpenCanvas(CanvasCellsName.GAMEOVER);

            gameStauts.ChangeGameState(GameState.GAMEOVER);
            
        }
    }
}
