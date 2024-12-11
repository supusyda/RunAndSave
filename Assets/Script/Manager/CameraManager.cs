using System;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CameraManager Instance { get; private set; }
    private CinemachineVirtualCamera phase1Cam;
    private CinemachineVirtualCamera phase2Cam;
    Logger logger;

    void OnEnable()
    {
        GameManager.OnPassFinishLine.AddListener(OnPassFinishLine);
    }
    void
    OnDisable()
    {
        GameManager.OnPassFinishLine.RemoveListener(OnPassFinishLine);
    }

    public void GetCamRef()
    {
        if (phase1Cam == null && phase2Cam == null)
        {
            phase1Cam = GameObject.FindWithTag("CamPhase1").GetComponent<CinemachineVirtualCamera>();
            phase2Cam = GameObject.FindWithTag("CamPhase2").GetComponent<CinemachineVirtualCamera>();
        }
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
