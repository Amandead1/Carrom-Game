using UnityEngine;
using System;
public class TurnHandler : MonoBehaviour
{

    [SerializeField] Transform playerStriker;
    [SerializeField] Transform aiStriker;

    Vector2 initialPlayerStrikerPos;
    Vector2 initialAIStrikerPos;


    private void OnEnable()
    {
        initialPlayerStrikerPos = playerStriker.position;
        initialAIStrikerPos = aiStriker.position;

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
            playerStriker.localScale = Vector3.one;
            aiStriker.localScale = Vector3.zero;

            SetInitialPosition();
        }
        else if (state == GameState.AITurn)
        {
            playerStriker.localScale = Vector3.zero;
            aiStriker.localScale = Vector3.one;

            SetInitialPosition();
        }
      
    }

    void SetInitialPosition()
    {
        playerStriker.position = initialPlayerStrikerPos;
        aiStriker.position = initialAIStrikerPos;
    }
}
