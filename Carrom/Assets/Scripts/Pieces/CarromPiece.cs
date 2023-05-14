using UnityEngine;

public class CarromPiece : MonoBehaviour, IPottable
{

   [SerializeField] int potScore = 1;

    public int GetPotScore()
    {
        return potScore;
    }

    public void PotPiece()
    {
        //Play audio and particle effect of potting here
        Destroy(gameObject);
    }
}
