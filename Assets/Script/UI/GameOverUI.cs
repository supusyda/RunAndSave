using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnWinGame = new UnityEvent();
    public TMP_Text gameOverText;

    [SerializeField] Transform GameoverTransform;
    void OnEnable()
    {
        OnGameOver.AddListener(OnGameOverEvent);
        OnWinGame.AddListener(OnWinGameEvent);
    }

    private void OnWinGameEvent()
    {
        gameOverText.text = "WIN GAME !!!!";

        GameoverTransform.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        OnGameOver.RemoveListener(OnGameOverEvent);
        OnWinGame.RemoveListener(OnWinGameEvent);
    }

    private void OnGameOverEvent()
    {

        gameOverText.text = "Game Over";

        GameoverTransform.gameObject.SetActive(true);
    }
}
