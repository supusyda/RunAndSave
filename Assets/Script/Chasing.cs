using UnityEngine;
using UnityEngine.Events;

public class Chasing : MonoBehaviour
{
    private ChasingSO _chasingSO;
    public static UnityEvent<ChasingSO> SetUpChasing = new();

    void OnEnable()
    {
        SetUpChasing.AddListener(Init);
    }
    void Start()
    {

    }

    private void Init(ChasingSO chasingSO)
    {
        transform.position = Vector3.zero; // set pos to 000

        _chasingSO = chasingSO;
        _chasingSO.Init(transform);
        _chasingSO.StartMove();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _chasingSO?.Stop();
        GameManager.ChangeState?.Invoke(GameState.GameOver);
        Debug.Log("TOUCH THE WAVE");
    }
}
