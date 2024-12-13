using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountdownUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float countdownTime = 10f; // Set the countdown time in seconds
    public TMP_Text countdownText; // Assign a UI Text element to display the countdown

    private float currentTime;
    bool isCounting = false;
    public static UnityEvent BeginCountDown = new();
    void OnEnable()
    {
        BeginCountDown.AddListener(OnBeginCountDown);
    }
    void OnDisable()
    {
        BeginCountDown.RemoveListener(OnBeginCountDown);
    }
    private void OnBeginCountDown()
    {
        isCounting = true;
    }
    void Start()
    {
        currentTime = countdownTime;
        UpdateCountdownText();
    }

    void Update()
    {
        if (!isCounting) return;
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateCountdownText();
        }
        else
        {
            currentTime = 0;
            OnCountdownEnd();
        }
    }

    void UpdateCountdownText()
    {
        countdownText.text = Mathf.Ceil(currentTime).ToString(); // Display time as an integer
    }

    void OnCountdownEnd()
    {
        Debug.Log("Countdown finished!");
        isCounting = false;
        GameManager.Instance.ChangeGameState(GameState.Start);
        countdownText.enabled = false;
    }
}
