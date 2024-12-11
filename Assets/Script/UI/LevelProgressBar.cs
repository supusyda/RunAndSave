using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelProgressBar : ProgressBar
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform playerTrans;
    float _roadLength = 0;
    public static UnityEvent<float> OnRoadProgressBarInit = new();
    void OnEnable()
    {
        OnRoadProgressBarInit.AddListener(Init);
    }
    void OnDisable()
    {
        OnRoadProgressBarInit.RemoveListener(Init);
    }

    private void Init(float maxRoadLength)
    {
        _roadLength = maxRoadLength;
    }

    void Start()
    {

    }

    void Update()
    {
        fillImage.fillAmount = playerTrans.position.z / (_roadLength * MyUnit.yMul);
    }

}
