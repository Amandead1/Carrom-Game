using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameResultTextUI;


    private void OnEnable()
    {
        GameManager.OnGameOver += HandleGameOver;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= HandleGameOver;
    }

    void HandleGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void UpdateResultText(string textToShow)
    {
        gameResultTextUI.text = textToShow;
    }

}
