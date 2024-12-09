using UnityEngine;

public class InteractOjb : MonoBehaviour, IDetectedObj
{
    [SerializeField] private FillBar fillBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float fullFillValue = 0;
    private float maxFullFillValue = 3;
    private float barFillSpeed = 2f;
    private JumpOnPlayerHead jumpOnPlayerHead;

    public Transform detector { get; set; }
    private bool isFullBar = false;

    void Awake()
    {
        jumpOnPlayerHead = GetComponent<JumpOnPlayerHead>();
        // fillBar = transform.Find("HealthHolder")?.GetComponent<FillBar>();
    }
    public void OnDetectedObj()
    {
        fullFillValue += Time.deltaTime * barFillSpeed;
        if (fullFillValue >= maxFullFillValue && isFullBar == false)
        {
            fullFillValue = maxFullFillValue;
            OnFullBar();
            isFullBar = true;
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
        jumpOnPlayerHead.playerHead = detector;
        jumpOnPlayerHead.JumpToPlayer();
    }
}
