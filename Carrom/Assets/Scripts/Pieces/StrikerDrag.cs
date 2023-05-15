using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class StrikerDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public static Action<float,Transform> OnStrikerRelease;

    [Header("UI")]
    [SerializeField] GameObject strikerSlider;

    [Header("Indicator GFX Properties")]
    [SerializeField] Transform indicatorGfx;
    [SerializeField] Transform arrowIndicator;
    [Space(5)]
    [SerializeField] float minIndicatorGfxScale = 1f;
    [SerializeField] float maxIndicatorGfxScale = 3f;


    Vector2 dragStartPos;
    Vector2 dragReleasePos;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleStrikerState;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleStrikerState;        
    }
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
        float dragLength = Vector2.Distance(touchPos, dragStartPos);
        dragLength = Mathf.Clamp(dragLength, minIndicatorGfxScale, maxIndicatorGfxScale);

        indicatorGfx.localScale = new Vector3(dragLength, dragLength, dragLength);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        dragReleasePos = Camera.main.ScreenToWorldPoint(eventData.position);
        float dragLength = Vector2.Distance(dragStartPos, dragReleasePos);

        if(GameManager.instance.currentState == GameState.PlayerTurn)
        {
            //Get FIRED when Player shot the striker
            OnStrikerRelease?.Invoke(dragLength, arrowIndicator);
        }

        //Changing game state after shot
        GameManager.instance.UpdateGameState(GameState.PlayerWait);
    }

    void CalculateArrowDirection(Vector2 touchPos)
    {
        Vector2 direction = dragStartPos - touchPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, arrowIndicator.forward);

        //Performing the rotation of arrow
        arrowIndicator.rotation = rot;
    }

    void ResetIndicatorGfx(bool showGraphics)
    {
        indicatorGfx.localScale = Vector3.one;
        HideOrShowIndicatorGfx(canShow: showGraphics);
    }

    void HideOrShowIndicatorGfx(bool canShow)
    {
        indicatorGfx.gameObject.SetActive(canShow);
        strikerSlider.SetActive(canShow);
    }

    void HandleStrikerState(GameState currenState)
    {
        switch (currenState)
        {
            case GameState.PlayerTurn:

                HideOrShowIndicatorGfx(canShow: true);
                strikerSlider.GetComponent<Slider>().value = 0;

                //For starting the player's turn timer
                transform.GetComponent<StrikerTimer>().HandleTimer();

                break;
            case GameState.PlayerWait:

                //for disabling the indicator and slider UI
                ResetIndicatorGfx(showGraphics: false);
                break;

            case GameState.AITurn:
                ResetIndicatorGfx(showGraphics: false);
                break;

        }

    }
}
