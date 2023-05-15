using UnityEngine;
using System;

public class PotHole : MonoBehaviour
{

    public static Action<int> OnPiecePotted;

   
    private void OnTriggerEnter2D(Collider2D col)
    {
        IPottable pottable = col.GetComponent<IPottable>();
        if (pottable != null)
        {
            //For more realistic approach
            //Also check if the piece to be potted velocity is less than a limit velocity then only pot
            pottable.PotPiece();

            //for updating the score
            OnPiecePotted?.Invoke(pottable.GetPotScore());
           
        }
    }

}
