using UnityEngine;
using UnityEngine.Events;

public class LevelBtn : BtnBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int levelID;
    public static UnityEvent<int> OnLevelClick = new();
    protected override void OnClick()
    {
        base.OnClick();
        OnLevelClick?.Invoke(levelID);
    }
}
