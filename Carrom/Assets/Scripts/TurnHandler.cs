using UnityEngine;
using System;
public class TurnHandler : MonoBehaviour
{

    [SerializeField] Transform playerStriker;
    [SerializeField] Transform aiStriker;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += ChangeTurn;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= ChangeTurn;
    }

    void ChangeTurn(GameState state)
    {
        if (state == GameState.PlayerTurn)
        {
            playerStriker.gameObject.SetActive(true);
            aiStriker.gameObject.SetActive(false);
        }
        else if (state == GameState.AITurn)
        {
            playerStriker.gameObject.SetActive(false);
            aiStriker.gameObject.SetActive(true);
        }
      
    }
}
