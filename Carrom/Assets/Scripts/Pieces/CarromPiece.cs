using UnityEngine;
using UnityEngine.Events;

public class CarromPiece : MonoBehaviour, IPottable
{
    public UnityEvent OnPuckPotted;

   [SerializeField] int potScore = 1;

    public int GetPotScore()
    {
        return potScore;
    }

    public void PotPiece()
    {
        //Play audio and particle effect of potting here
        OnPuckPotted?.Invoke();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
