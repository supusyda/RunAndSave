using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    const string Current_Level = "Current_Level";
    public static LevelManager Instance { get; private set; }
    private int currentLevel = 1;

    private List<string> levelFiles;
    [SerializeField] List<Transform> transitions;

    private void Awake()
    {
        // Ensure only one instance of LevelManager exists
        if (Instance == null)
        {
            Instance = this; Debug.Log("DONT DESTROY");
            DontDestroyOnLoad(transform.gameObject);
            // Keep across scene loads
        }
        else
        {
            Destroy(gameObject);
        }

        // Load all level files at the start
        // transitions

        LoadTransition();
        // LoadAllLevelFiles();

    }

    void OnEnable()
    {
        LoadEvent();
    }
    void OnDisable()
    {
        RemoveEvent();
    }


    void LoadTransition()
    {
        Transform allTransition = transform.Find("Transition");
        foreach (Transform transition in allTransition)
        {
            transitions.Add(transition);
        }
    }
    void LoadEvent()
    {
        RestartBtn.restartGame.AddListener(OnReset);
        StartGameDefaultBtn.OnStartGameDefault.AddListener(OnStartGameDefault);
    }
    void RemoveEvent()
    {
        RestartBtn.restartGame.RemoveListener(OnReset);
        StartGameDefaultBtn.OnStartGameDefault.RemoveListener(OnStartGameDefault);
    }
    ITransition GetTransitionByName(string name)
    {

        return transitions.First(t => t.name == name).GetComponent<ITransition>();
    }
    // Load all level files ending with level.json


    // Get all available levels
    public List<string> GetAvailableLevels()
    {
        return new List<string>(levelFiles);
    }


    public void TransitionToScene(string sceneName, string transitionName, Action callback = null)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName, callback));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName, Action callback
     = null)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        ITransition transition = GetTransitionByName(transitionName);

        // Optionally, show a loading screen or progress here
        yield return transition.TransitionIn();
        while (asyncLoad.progress < 0.9f)
        {

            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        // yield return new WaitUntil(() => asyncLoad.isDone);
        yield return null;

        callback?.Invoke();
        yield return transition.TransitionOut();

    }
    IEnumerator NextLevelAnim(string transitionName)
    {
        ITransition transition = GetTransitionByName(transitionName);
        Debug.Log(GetTransitionByName(transitionName));
        yield return transition.TransitionIn();

        yield return transition.TransitionOut();


    }
    // Save level progress (customize as needed)
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;

    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    // public void LoadNextLevel()
    // {
    //     currentLevel += 1;
    //     if (!HandleSave.HasLevel(currentLevel))

    //     {
    //         Event.OnGameOver.Invoke();
    //         return;
    //     }
    //     LoadLevel(currentLevel);
    // }

    // public void LoadLevel(int level)
    // {
    //     GridEditManager.OnLoadPlayLevel.Invoke(level);
    //     CommandScheduler.Clear();
    // }
    // public void LoadLevelFormMenu(int level)
    // {
    //     // TransitionToScene("Game2");
    //     SetCurrentLevel(level);
    //     TransitionToScene("Game2", "Circle");

    // }
    // void OnWin()
    // {
    //     SaveCurrentLevelToPlayerPref();
    //     if (!HandleSave.HasLevel(currentLevel + 1))
    //     {
    //         Event.OnGameOver.Invoke();
    //         return;
    //     }
    //     StartCoroutine(NextLevelAnim("Circle"));
    // }
    void OnReset()
    {
        TransitionToScene("game", "Circle", (() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Init);
        }));

    }
    void OnStartGameDefault()
    {
        TransitionToScene("game", "Circle");

    }
    // void OnClickBackBtn()
    // {
    //     TransitionToScene("Menu", "Circle");
    // }
    // void SaveCurrentLevelToPlayerPref()
    // {
    //     if (LoadCurrentLevelFormPlayerPref() > currentLevel) return;
    //     PlayerPrefs.SetInt("Current_Level", currentLevel);
    //     PlayerPrefs.Save(); // Ensure the changes are written to storage.
    //     Debug.Log("Level saved: " + currentLevel);
    // }
    // public int LoadCurrentLevelFormPlayerPref()
    // {
    //     int level = PlayerPrefs.GetInt(Current_Level, 1); // Default to level 1 if not set.

    //     return level;
    // }
}
