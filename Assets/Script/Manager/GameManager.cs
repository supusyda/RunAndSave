using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<LevelSO> _levels = new();
    public static GameManager Instance { get; private set; }
    public static UnityEvent<int> OnPassFinishLine = new();
    public static UnityEvent<GameState> ChangeState = new();
    public static UnityEvent<LevelSO> Init = new();


    private GameState _currentGameState;
    private Logger logger;
    private int _currentLevel = 1;
    private int _currentPhase = 1;

    public int currentScore = 0;


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
    void OnEnable()
    {
        OnPassFinishLine.AddListener(PassFinishLine);
        InteractOjb.OnBarFillFull.AddListener(OnBarFillFull);
        ChangeState.AddListener(ChangeGameState);
        // StarterAssetsInputs.OnTapEvent.AddListener(OnTapEvent);
    }



    void OnDisable()
    {
        OnPassFinishLine.RemoveListener(PassFinishLine);
        InteractOjb.OnBarFillFull.RemoveListener(OnBarFillFull);
        ChangeState.RemoveListener(ChangeGameState);
        // StarterAssetsInputs.OnTapEvent.RemoveListener(OnTapEvent);

    }
    private void OnBarFillFull()
    {
        currentScore++;
        ScoreUI.UpdateScore.Invoke(currentScore, _levels[_currentLevel - 1].cats.Count);
    }
    private void PassFinishLine(int finishLineNum)// could use enum
    {
        if (finishLineNum == 2)// last finish line
        {
            ChangeGameState(GameState.GameOver);
        }
    }

    void Start()
    {
        ChangeGameState(GameState.Init);
    }
    public void ChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.SpawnCat:



                break;
            case GameState.SpawnObject:


                break;
            case GameState.SpawnChasing:


                _currentGameState = GameState.SpawnChasing;
                break;
            case GameState.CutScene:
                break;
            case GameState.Start:
                _currentGameState = GameState.Start;

                break;
            case GameState.CountDown:
                CountdownUI.BeginCountDown.Invoke();
                _currentGameState = GameState.CountDown;
                break;
            case GameState.Init:
                logger.Log("INIT", this);
                SpawnManager.Init.Invoke(_levels[_currentLevel - 1]);


                Chasing.SetUpChasing.Invoke(_levels[_currentLevel - 1].chasingSO);
                ScoreUI.UpdateScore.Invoke(currentScore, _levels[_currentLevel - 1].cats.Count);//init score UI
                CameraManager.Instance.GetCamRef();
                LevelProgressBar.OnRoadProgressBarInit.Invoke(_levels[_currentLevel - 1].chasingSO.GetRoadLength());

                currentScore = 0;
                _currentGameState = GameState.Init;
                ChangeGameState(GameState.CountDown);
                break;
            case GameState.GameOver:
                GameOverUI.OnGameOver.Invoke();
                _currentGameState = GameState.GameOver;

                break;
            case GameState.Win:
                break;

            default:
                break;
        }
    }
    public GameState GetCurrentState()
    {
        return _currentGameState;
    }
}
