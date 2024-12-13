using UnityEngine;

public class Road : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform playerTrans;
    readonly private float roadLength = 39.5f;
    const float distanceThreshold = 1.5f;
    [SerializeField] float distanceToPlayer = 0;
    [SerializeField] float distancetgarget = 0;

    void OnEnable()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        distancetgarget = this.transform.position.z + 2 * roadLength;
        CheackPlayerOutOfSight();
    }
    void CheackPlayerOutOfSight()
    {
        Vector3 playerPos = playerTrans.position;
        distanceToPlayer = playerPos.z - transform.position.z;
        // distanceThreshold = 1f;
        if (playerPos.z >= this.transform.position.z + 2 * roadLength)
        {
            MoveToTop();
        }

    }
    void MoveToTop()
    {
        const int totalNumberOfRoad = 5;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + roadLength * totalNumberOfRoad);
    }
}
