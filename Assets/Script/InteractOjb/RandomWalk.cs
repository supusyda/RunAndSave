using System.Collections;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{

    public float moveSpeed = 5f; // Speed of movement
    public float directionChangeInterval = 2f; // Time interval to change direction
    private Rigidbody rb;
    [SerializeField] private Vector3 randomDirection;
    // private Vector3 spawnPosition; // Store the spawn position
    [SerializeField] private bool isRandomWalk = true;
    Coroutine randWalkCoroutine;
    // private Vector3 targetDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // spawnPosition = transform.position; // Record the spawn position

        BeginWalk();
    }
    void BeginWalk()
    {
        isRandomWalk = true;
        randWalkCoroutine = StartCoroutine(ChangeDirectionRoutine());
    }
    void FixedUpdate()
    {
        // Apply movement in the current random direction
        if (isRandomWalk == false) { return; }
        RotateTowardsTarget();
        rb.linearVelocity = randomDirection * moveSpeed;
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            // Generate a new random direction
            randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

            // Wait for the next direction change
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    public void Reset()
    {

        rb.linearVelocity = Vector3.zero;
        randomDirection = Vector3.zero;

        isRandomWalk = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        if (randWalkCoroutine != null) StopCoroutine(randWalkCoroutine);

    }
    void RotateTowardsTarget()
    {
        // Smoothly rotate towards target direction with adjustable rotation speed
        float rotationSpeed = 5f;  // Adjust as needed

        if (randomDirection != Vector3.zero)  // Avoid unnecessary rotations
        {
            // Debug.Log("");
            Quaternion targetRotation = Quaternion.LookRotation(randomDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
