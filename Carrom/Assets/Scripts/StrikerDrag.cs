using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class StrikerDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static Action<float,Transform> OnStrikerRelease;

    [SerializeField] Transform indicatorGfx;
    [SerializeField] Transform arrowIndicator;
    [Space(10)]

    [SerializeField] float minIndicatorGfxScale = 1f;
    [SerializeField] float maxIndicatorGfxScale = 3f;

    Vector2 dragStartPos;
    Vector2 dragReleasePos;

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(eventData.position);

        //Calculating the arrow's direction
        CalculateArrowDirection(touchPos);

        //Scaling and clamping the Indicator graphic
        float length = Vector2.Distance(touchPos, dragStartPos);
        length = Mathf.Clamp(length, minIndicatorGfxScale, maxIndicatorGfxScale);

        indicatorGfx.localScale = new Vector3(length, length, length);

    }

    void CalculateArrowDirection(Vector2 touchPos)
    {
        Vector2 direction = dragStartPos - touchPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, arrowIndicator.forward);
        
        //Performing the rotation of arrow
        arrowIndicator.rotation = rot;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetIndicatorGfxToInitialValue();

        dragReleasePos = Camera.main.ScreenToWorldPoint(eventData.position);
        float dragLength = Vector2.Distance(dragStartPos, dragReleasePos);

        print(dragLength);

        OnStrikerRelease?.Invoke(dragLength,arrowIndicator);
    }

    void SetIndicatorGfxToInitialValue()
    {
        indicatorGfx.localScale = Vector3.one;
    }
}
