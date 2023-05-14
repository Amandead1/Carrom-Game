using UnityEngine;

public class PlayerStriker : Striker
{
  
    [Header("Physics Properties")]
    [SerializeField] float minForce = 1f;
    [SerializeField] float maxForce = 10f;
    [SerializeField] float maxDragPos = 5f;

  
    private void OnEnable()
    {
        StrikerDrag.OnStrikerRelease += AddForce;
    }

    private void OnDisable()
    {
        StrikerDrag.OnStrikerRelease -= AddForce;
    }

    void AddForce(float dragDistance, Transform directionTransform)
    {
       
       float forceValue = base.CalculateForce(minForce: minForce, maxForce: maxForce,
                           dragDistance: dragDistance, maxDragDistance: maxDragPos);

        //change the transform to the arrow transform
        ForceToAdd(forceValue, directionTransform);
    }

    protected override void ForceToAdd(float forceValue, Transform directionTransform)
    {
        base.ForceToAdd(forceValue, directionTransform);
    }

}
