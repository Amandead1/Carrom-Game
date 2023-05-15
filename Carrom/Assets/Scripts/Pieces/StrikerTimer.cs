using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StrikerTimer : MonoBehaviour
{
    [SerializeField] Image turnTimerImageUI;

    [Tooltip("Time Duration is in seconds")]
    [SerializeField] float timerDuration = 3;

    private void Start()
    {
        
    }
    public void HandleTimer()
    {
        StartCoroutine(UpdateTimer(timerDuration));
    }

    IEnumerator UpdateTimer(float remainingTime)
    {
        //Timer has not ended
        while(remainingTime > 0)
        {
            if (GameManager.instance.currentState == GameState.PlayerWait ||
                GameManager.instance.currentState == GameState.AIWait)
                break;
            turnTimerImageUI.fillAmount = Mathf.InverseLerp(0, timerDuration, remainingTime);
            //turnTimerImageUI.fillAmount = Mathf.Lerp(turnTimerImageUI.fillAmount, fillAmount, 0.1f);
            remainingTime -= Time.deltaTime;
            yield return null;
            
        }

        //Changing state of the game if timer runs out
        ChangeState();
    }
    void ChangeState()
    {
        turnTimerImageUI.fillAmount = 1f;
        if (GameManager.instance.currentState == GameState.PlayerTurn)
        {
            GameManager.instance.UpdateGameState(GameState.AITurn);
        }

        else if(GameManager.instance.currentState == GameState.AITurn)
        {
            GameManager.instance.UpdateGameState(GameState.PlayerTurn);
        }
    }

}
