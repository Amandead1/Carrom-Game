using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // To Notify game state changes
    public static Action<GameState> OnGameStateChanged;
    public static Action OnGameOver;

    [SerializeField] int turnWaitDuration = 3;
    [SerializeField] Transform carromPieceParent;

    public GameState currentState { get; private set; }

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
     
        OnGameStateChanged?.Invoke(newState);

        switch (currentState)
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

    }

    IEnumerator WaitState(int duration)
    {
        while(duration > 0)
        {
            yield return new WaitForSeconds(1);
            duration--;
        }
       
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
        if(carromPieceParent.childCount == 0)
        {
            UpdateGameState(GameState.GameOver);
            return;
        }
    }
    
    void GameOver()
    {
        //Fired when game over
        //Handle winner and show gameOver UI
        OnGameOver?.Invoke();
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