using UnityEngine;

public class InteractOjb : MonoBehaviour, IDetectedObj
{
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
    }
    public void OutDetectedObj()
    {
        if (fullFillValue >= maxFullFillValue) return;
        fullFillValue = 0;
    }
    private void OnFullBar()
    {
        //ToDo: 
        jumpOnPlayerHead.playerHead = detector;
        jumpOnPlayerHead.JumpToPlayer();
    }
}
