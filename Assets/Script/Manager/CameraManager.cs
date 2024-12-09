using System;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CameraManager Instance { get; private set; }
    [SerializeField] private CinemachineVirtualCamera phase1Cam;
    [SerializeField] private CinemachineVirtualCamera phase2Cam;
    Logger logger;

    void OnEnable()
    {
        GameManager.OnPassFinishLine.AddListener(OnPassFinishLine);
    }
    void OnDisable()
    {
        GameManager.OnPassFinishLine.RemoveListener(OnPassFinishLine);
    }

    private void OnPassFinishLine(int phaseID)
    {
        if (phaseID == 1)
        {
            phase1Cam.gameObject.SetActive(false);
            phase2Cam.gameObject.SetActive(true);
        }

    }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; // Set the instance
        DontDestroyOnLoad(gameObject); // Keep the instance across scenes

        logger = GetComponent<Logger>();
    }
}
