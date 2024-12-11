using UnityEngine;
using UnityEngine.Events;

public class RestartBtn : BtnBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public static UnityEvent restartGame = new();
    protected override void OnClick()
    {
        base.OnClick();
        restartGame.Invoke();
    }
}
