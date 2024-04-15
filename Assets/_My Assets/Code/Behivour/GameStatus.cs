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
}
