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

        _chasingSO = chasingSO;
        _chasingSO.Init(transform);
        _chasingSO.StartMove();
    }


}
