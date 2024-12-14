using UnityEngine;

public class LevelSceneBtn : BtnBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void OnClick()
    {
        base.OnClick();
        LevelManager.Instance.TransitionToScene(LevelManager.Instance.LEVEL_SCENE, "Circle");
    }
}
