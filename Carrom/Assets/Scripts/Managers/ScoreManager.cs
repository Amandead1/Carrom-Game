using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    public UnityEvent OnPlayerWin;
    public UnityEvent OnAIWin;
    public UnityEvent OnGameDraw;

    [SerializeField] TMP_Text playerScoreTextUI;
    [SerializeField] TMP_Text aiScoreTextUI;


    public int currentPlayerScore { get; private set; }
    public int currentAIScore { get; private set; }


    private void OnEnable()
    {
        PotHole.OnPiecePotted += UpdateScore;
        GameManager.OnGameOver += CheckWinner;
    }

    private void OnDisable()
    {
        PotHole.OnPiecePotted -= UpdateScore;
        GameManager.OnGameOver -= CheckWinner;
    }

    void UpdateScore(int scoretoAdd)
    {
        if(GameManager.instance.currentState == GameState.PlayerWait)
        {
            currentPlayerScore += scoretoAdd;
            playerScoreTextUI.text = currentPlayerScore.ToString();
        }
        else if(GameManager.instance.currentState == GameState.AIWait)
        {
            currentAIScore += scoretoAdd;
            aiScoreTextUI.text = currentAIScore.ToString();
        }
    }

    void CheckWinner()
    {
        if(currentPlayerScore > currentAIScore)
        {
            OnPlayerWin?.Invoke();
        }
        else if (currentPlayerScore < currentAIScore)
        {
            OnAIWin?.Invoke();
        }
        else
        {
            OnGameDraw?.Invoke();
        }
    }

}
