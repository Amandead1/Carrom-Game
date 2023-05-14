using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // To Notify game state changes
    public static Action<GameState> OnGameStateChanged;

    [SerializeField] int turnWaitDuration = 3;



    GameState currentState = GameState.PlayerTurn;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.PlayerTurn);
    }
    public void UpdateGameState(GameState newState)
    {
        currentState = newState;
        print(currentState);
        switch(currentState)
        {
            case GameState.PlayerTurn:
                break;
            case GameState.PlayerWait: StartCoroutine(WaitState(turnWaitDuration));
                break;
            case GameState.AITurn:
                break;
            case GameState.AIWait: StartCoroutine(WaitState(turnWaitDuration));
                break;
            case GameState.GameOver: GameOver();
                break;
            default: Debug.LogWarning("Invalid Game State");
                break;
        }

        OnGameStateChanged?.Invoke(newState);

    }

    IEnumerator WaitState(int duration)
    {
        while(duration > 0)
        {
            yield return new WaitForSeconds(1);
            duration--;
        }
        print("state ended");

        //Also check if game has ended, either player wins or AI
        CheckForGameOver();

        //changing the turn as per the current state
        if(currentState == GameState.PlayerWait)
        {
            UpdateGameState(GameState.AITurn);
        }
        else if(currentState == GameState.AIWait)
        {
            UpdateGameState(GameState.PlayerTurn);
        }
    }

    void CheckForGameOver()
    {

    }
    
    void GameOver()
    {
        print("Game over");
    }

}
public enum GameState
{
    PlayerTurn,
    PlayerWait,
    AITurn,
    AIWait,
    GameOver
}