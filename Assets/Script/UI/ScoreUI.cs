using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UnityEvent<int, int> UpdateScore = new();// currentScore/maxScore

    private TMP_Text _scoreTxt;

    void Awake()
    {
        _scoreTxt = GetComponent<TMP_Text>();

    }
    void OnEnable()
    {
        UpdateScore.AddListener(OnUpdateScore);
    }
    void OnDisable()
    {
        UpdateScore.RemoveListener(OnUpdateScore);
    }

    private void OnUpdateScore(int currentScore, int maxScore)
    {
        if (_scoreTxt == null) return;
        _scoreTxt.text = currentScore.ToString() + " / " + maxScore.ToString();
    }
}
