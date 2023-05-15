using UnityEngine;
using System;

public class PotHole : MonoBehaviour
{

    public static Action<int> OnPiecePotted;

    [Tooltip("Velocity of the puck upto which it can be potted")]
    [SerializeField] float safeLimitVelocity = 3f;

    bool interacted = false;

    private void Start()
    {
        
    }


    private void OnTriggerStay2D(Collider2D col)
    {
         IPottable pottable = col.GetComponent<IPottable>();
        if (pottable != null)
        {
            //For more realistic approach
            if(col.GetComponent<Rigidbody2D>().velocity.magnitude <= safeLimitVelocity && !interacted)
            {
                //Also check if the piece to be potted velocity is less than a limit velocity then only pot
                pottable.PotPiece();

                //for updating the score
                OnPiecePotted?.Invoke(pottable.GetPotScore());

                interacted = true;
            }
        }
    }

}
