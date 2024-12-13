using DG.Tweening;
using UnityEngine;

public class JumpOnPlayerHead : MonoBehaviour
{

    public Transform playerHead;
    public float jumpPower = 5f;
    public int numJumps = 1;
    public float duration = 1f; //second
    public float offsetBetweenEachObj = 2f;
    static private int stackCount = 1;
    void OnEnable()
    {
        stackCount = 0;
    }

    public void JumpToPlayer()
    {
        if (playerHead == null)
        {
            Debug.LogError("Player head reference is not assigned!");
            return;
        }
        stackCount++;
        Vector3 jumpTarget = new Vector3(playerHead.position.x, playerHead.position.y + (Vector3.up * offsetBetweenEachObj * stackCount).y, playerHead.position.z);
        // Start the jump animation
        transform.DOJump(
            jumpTarget,
            jumpPower,
            numJumps,
            duration
        ).SetEase(Ease.OutQuad).OnUpdate(() =>
        {
            // jumpTarget = new Vector3(playerHead.position.x, playerHead.position.y + (Vector3.up * offsetBetweenEachObj * stackCount).y, playerHead.position.z);

        }).OnComplete(() =>
        {
            transform.SetParent(playerHead);

            transform.localPosition = Vector3.up * offsetBetweenEachObj * stackCount;
        });
    }
}
