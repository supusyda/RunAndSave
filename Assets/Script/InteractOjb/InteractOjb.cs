using UnityEngine;
using UnityEngine.Events;

public class InteractOjb : MonoBehaviour, IDetectedObj
{

    [SerializeField] private FillBar fillBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float maxFullFillValue = 3;
    private float barFillSpeed = 2f;
    private JumpOnPlayerHead jumpOnPlayerHead;
    private RandomWalk randomWalk;
    private bool isFullBar = false;

    public Transform detector { get; set; }
    public float fullFillValue = 0;
    public static UnityEvent OnBarFillFull = new();


    void Awake()
    {
        jumpOnPlayerHead = GetComponent<JumpOnPlayerHead>();
        randomWalk = GetComponent<RandomWalk>();
        // fillBar = transform.Find("HealthHolder")?.GetComponent<FillBar>();
    }
    public void OnDetectedObj()
    {
        fullFillValue += Time.deltaTime * barFillSpeed;//filling bar

        if (fullFillValue >= maxFullFillValue && isFullBar == false) //full
        {
            OnFullBar();
        }
        fillBar.OnFillNumberChange.Invoke(fullFillValue / maxFullFillValue);
    }
    public void OutDetectedObj()
    {
        if (fullFillValue >= maxFullFillValue) return;
        fullFillValue = 0;
        fillBar.OnFillNumberChange.Invoke(0f);
    }
    private void OnFullBar()
    {
        //ToDo: 
        fullFillValue = maxFullFillValue;
        isFullBar = true;

        jumpOnPlayerHead.playerHead = detector;//get ref of player head
        jumpOnPlayerHead.JumpToPlayer();
        randomWalk.Reset();

        OnBarFillFull.Invoke();

    }
}
