using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{

    [SerializeField] TMP_Text timerTextUI;

    [Tooltip("Match Duration is in seconds")]
    [SerializeField] float matchDuration = 120f;


    bool timerRunning = true;
    float timeRemaining;

    private void Start()
    {
        timeRemaining = matchDuration;
    }

    private void Update()
    {
        if(timerRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                print("Match Ended");
                timeRemaining = 0;
                DisplayTime(timeRemaining);
                timerRunning = false;

                //Set the state to game over state
                GameManager.instance.UpdateGameState(GameState.GameOver);
            }
          
        }
        
        

    }

    void DisplayTime(float timeRemaining)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerTextUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
