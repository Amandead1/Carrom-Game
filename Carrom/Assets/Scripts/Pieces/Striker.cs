using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] Rigidbody2D rb;

   
    public void ChangeXPos(float value)
    {
        transform.position = new Vector2(value, transform.position.y);
    }

   
    protected virtual void ForceToAdd(float forceValue, Transform directionTransform)
    {
        rb.AddForce(forceValue * directionTransform.right,ForceMode2D.Impulse);
    }

    protected float CalculateForce(float minForce, float maxForce, float dragDistance, float maxDragDistance)
    {
        if (dragDistance <= 0) return 0;
        else if (dragDistance >= maxDragDistance) return maxForce;

        float force = minForce + (dragDistance / maxDragDistance) * (maxForce - minForce);
        return force;
    }
}
