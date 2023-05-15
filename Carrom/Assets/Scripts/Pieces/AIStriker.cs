using UnityEngine;
using System.Collections;
public class AIStriker : Striker
{

    [Header("Striker Position")]
    [SerializeField] float minXPos = -3.28f;
    [SerializeField] float maxXPos = 3.28f;

    [Header("AI Striker Logic")]
    [SerializeField] Transform carromPieceParent;
    [SerializeField] float minShootForce = 20f;
    [SerializeField] float maxShootForce = 40f;
    [Space(5)]
    [SerializeField] float minWaitTime = 1f;
    [SerializeField] float maxWaitTime = 3.5f;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleAiStrikerState;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleAiStrikerState;
    }


    IEnumerator ShootStriker()
    {
        float randWaitTime = Random.Range(minWaitTime, maxWaitTime);

        yield return new WaitForSeconds(0.1f);
        //Setting the striker's X position
        ChangeXPos(value: Random.Range(minXPos, maxXPos));

        yield return new WaitForSeconds(randWaitTime);

        //Finding a random target(piece) and rotate towards it
        Transform carromPiece = GetRandomPiece();
        CalculateShootDirection(carromPiece);

        //Shoot the striker towards the carrom piece
        AddForce();

        //Change the game state to the Ai wait state
        GameManager.instance.UpdateGameState(GameState.AIWait);

    }
    void AddForce()
    {
        float forceValue = Random.Range(minShootForce, maxShootForce);
        ForceToAdd(forceValue: forceValue,directionTransform: transform);
    }
    Transform GetRandomPiece()
    {
        int randIndex = Random.Range(0, carromPieceParent.childCount);
        Transform carromPiece = carromPieceParent.GetChild(randIndex);
        return carromPiece;
    }

    void CalculateShootDirection(Transform carromPiece)
    {
        Vector2 direction = carromPiece.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);

        //Performing the rotation of arrow
        transform.rotation = rot;
    }

    void HandleAiStrikerState(GameState currentState)
    {
        switch(currentState)
        {
            case GameState.AITurn:
                
                //For starting the AI's turn timer
                transform.GetComponent<StrikerTimer>().HandleTimer();

                //Firing the striker
                StartCoroutine(ShootStriker());

                break;

            case GameState.AIWait:
                break;

        }
    }
}
