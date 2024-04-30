using System;
using UnityEngine;

public enum GameState
{
    PLAYING,
    GAMEOVER
}
public class GameStatus : MonoBehaviour
{
    public static Action GameOverAction;
    public static GameStatus instance;
    [SerializeField] private GameState gameState;
    [SerializeField] private GameScene gameScene;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeGameState (GameState desireGameState)  
    {
        gameState = desireGameState;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public GameScene GetCurrentGameScene()
    {
        return gameScene;
    }
}
