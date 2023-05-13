using UnityEngine;

public class PlayerStriker : Striker
{

    [SerializeField] Transform arrowIndicator;

    [Header("Physics Properties")]
    [SerializeField] float minForce = 1f;
    [SerializeField] float maxForce = 10f;
    [SerializeField] float maxDragPos = 2f;

    private float dragDistance = 0.2f;

    protected override void AddForce()
    {
       
       float forceValue = base.CalculateForce(minForce: minForce, maxForce: maxForce,
                           dragDistance: dragDistance, maxDragDistance: maxDragPos);

        ForceToAdd(forceValue, arrowIndicator);
    }

    protected override void ForceToAdd(float forceValue, Transform directionTransform)
    {
        base.ForceToAdd(forceValue, directionTransform);
    }

}
