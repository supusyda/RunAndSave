using UnityEngine;

public class UpgradeSpeedBtn : BtnBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] PlayerStat playerStat;
    [SerializeField] SpeedTxt speedTxt;
    void Start()
    {
        speedTxt.SetText(playerStat.speed);
    }
    protected override void OnClick()
    {
        playerStat.UpgradeSpeed();
        speedTxt.SetText(playerStat.speed);
        base.OnClick();
    }

}
