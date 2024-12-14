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
    public static UnityEvent<LevelSO, int> OnInitByLevel = new(); // levelSO and current level



    private GameState _currentGameState = GameState.None;
    private Logger logger;
    private int _currentLevel = 1;
    public int currentScore = 0;
    public int maxScore = 0;
    [SerializeField] private bool _isLevelByDesign;



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
        LevelManager.OnSelectLevel.AddListener(LoadLevelFormLevelSelect);
        // StarterAssetsInputs.OnTapEvent.AddListener(OnTapEvent);
    }

    private void LoadLevelFormLevelSelect(int levelID)
    {
        _currentLevel = levelID;
        ChangeGameState(GameState.InitByLevel);
    }

    void OnDisable()
    {
        OnPassFinishLine.RemoveListener(PassFinishLine);
        InteractOjb.OnBarFillFull.RemoveListener(OnBarFillFull);
        ChangeState.RemoveListener(ChangeGameState);
        LevelManager.OnSelectLevel.RemoveListener(LoadLevelFormLevelSelect);
        // StarterAssetsInputs.OnTapEvent.RemoveListener(OnTapEvent);

    }
    private void OnBarFillFull()
    {
        currentScore++;
        ScoreUI.UpdateScore.Invoke(currentScore, maxScore);
    }
    private void PassFinishLine(int finishLineNum)// could use enum
    {
        if (finishLineNum == 2)// last finish line
        {
            ChangeGameState(GameState.Win);
        }
    }

    void Start()
    {
        if (_currentGameState != GameState.None) return;
        // ChangeGameState(GameState.Init);
        // logger.Log("CAN GO THIS ??", this);

    }
    public void ChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.CutScene:
                break;
            case GameState.Start:
                _currentGameState = GameState.Start;

                break;
            case GameState.CountDown:
                CountdownUI.BeginCountDown.Invoke();
                _currentGameState = GameState.CountDown;
                logger.Log("COUNT DOWN", this);
                ChangeGameState(GameState.Start);

                break;
            case GameState.Init:

                _isLevelByDesign = true;
                currentScore = 0;
                SetUpGame(_levels[0]); // get the default level 

                _currentGameState = GameState.Init;
                ChangeGameState(GameState.CountDown);
                break;
            case GameState.InitByLevel:

                _isLevelByDesign = false;

                logger.Log("INIT BY LEVEL", this);
                LevelSO newLevel = LevelSO.GetLevelSOBaseOnThisSO(_levels[0], _currentLevel); // create a level but multyply diff by level

                currentScore = 0;
                SetUpGame(newLevel);
                _currentGameState = GameState.InitByLevel;
                ChangeGameState(GameState.CountDown);
                break;
            case GameState.GameOver:
                GameOverUI.OnGameOver.Invoke();
                _currentGameState = GameState.GameOver;

                break;
            case GameState.Win:
                GameOverUI.OnWinGame.Invoke();
                _currentGameState = GameState.Win;
                break;

            default:
                break;
        }
    }
    public GameState GetCurrentState()
    {
        return _currentGameState;
    }
    private void SetUpGame(LevelSO level)
    {
        maxScore = level.cats.Count;
        SpawnManager.Init.Invoke(level);
        Chasing.SetUpChasing.Invoke(level.chasingSO);
        ScoreUI.UpdateScore.Invoke(currentScore, maxScore);//init score UI
        CameraManager.Instance.GetCamRef();
        LevelProgressBar.OnRoadProgressBarInit.Invoke(level.chasingSO.GetRoadLength());
    }

    public void ResetLevel()
    {
        if (_isLevelByDesign == false)
        {

            ChangeGameState(GameState.InitByLevel);
        }
        else
        {
            ChangeGameState(GameState.Init);
        }
    }
}
