using UnityEngine;
using UnityEngine.Events;

public class StartGameDefaultBtn : BtnBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static public UnityEvent OnStartGameDefault = new();
    protected override void OnClick()
    {
        base.OnClick();
        OnStartGameDefault.Invoke();
    }
}
