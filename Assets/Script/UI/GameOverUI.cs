using System;
using UnityEngine;
using UnityEngine.Events;

public class GameOverUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UnityEvent OnGameOver = new UnityEvent();
    [SerializeField] Transform GameoverTransform;
    void OnEnable()
    {
        OnGameOver.AddListener(OnGameOverEvent);
    }

    void OnDisable()
    {
        OnGameOver.RemoveListener(OnGameOverEvent);
    }

    private void OnGameOverEvent()
    {
        GameoverTransform.gameObject.SetActive(true);
    }
}
