using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField] private List<LevelSO> _levels = new();
    private GameState _currentGameState;
    Logger logger;

    private int _currentLevel = 1;

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
    void Start()
    {
        ChangeGameState(GameState.SpawnCat);
    }
    public void ChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.SpawnCat:
                SpawnManager.OnSpawnCat.Invoke(_levels[_currentLevel - 1]);
                logger.Log("SpawnCat", this);
                ChangeGameState(GameState.SpawnChasing);
                _currentGameState = GameState.SpawnCat;
                break;
            case GameState.SpawnObject:
                break;
            case GameState.SpawnChasing:
                Chasing.SetUpChasing.Invoke(_levels[_currentLevel - 1].chasingSO);
                logger.Log("SpawnChasing", this);
                _currentGameState = GameState.SpawnChasing;
                break;
            case GameState.CutScene:
                break;
            case GameState.Start:
                break;
            case GameState.WaitForInput:
                break;
            case GameState.GameOver:
                break;
            case GameState.Win:
                break;

            default:
                break;
        }
    }
}
