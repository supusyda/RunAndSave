using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Scriptable Objects/PlayerStat")]
public class PlayerStat : ScriptableObject
{
    public float speed = 5;
    public float maxSpeed = 10;

    public void UpgradeSpeed()
    {
        //enter formilar here;
        float increaseAmount = .5f;
        speed += increaseAmount;
        maxSpeed += increaseAmount;
    }
}
