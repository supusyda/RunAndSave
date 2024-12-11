using System.Collections;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // public Vector3 startPos;
    // public float walkRadius = 5f;
    // public float speed = 2f;       // Speed
    // public float changeDirectionInterval = 2f; // Time between direction changes
    // public bool isRandomWalk = true;


    // private Vector3 targetPosition;
    // private float changeDirectionTimer;

    // void OnEnable()
    // {
    //     // Initialize at spawn position

    // }
    // void Start()
    // {
    //     startPos = transform.position;


    //     // Set initial random target position
    //     SetRandomTargetPosition();
    // }
    // void Update()
    // {

    //     if (!isRandomWalk) return;
    //     // Move towards the target position

    //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    //     // Check if it's time to pick a new direction
    //     changeDirectionTimer -= Time.deltaTime;
    //     if (changeDirectionTimer <= 0f || Vector3.Distance(transform.position, targetPosition) < 0.1f)
    //     {
    //         SetRandomTargetPosition();
    //     }
    // }

    // void SetRandomTargetPosition()
    // {
    //     // Pick a random point within the walk radius
    //     Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
    //     randomDirection.y = 0; // Ensure movement is on a flat plane
    //     targetPosition = startPos + randomDirection;

    //     // Reset the direction change timer
    //     changeDirectionTimer = changeDirectionInterval;
    // }
    public float moveSpeed = 5f; // Speed of movement
    public float directionChangeInterval = 2f; // Time interval to change direction
    private Rigidbody rb;
    private Vector3 randomDirection;
    private Vector3 spawnPosition; // Store the spawn position
    private bool isRandomWalk = true;
    Coroutine randWalkCoroutine;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPosition = transform.position; // Record the spawn position

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
        if (!isRandomWalk) return;
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
        // Reset position and stop movement
        // transform.position = spawnPosition;
        rb.linearVelocity = Vector3.zero;

        // Optionally reset random direction
        randomDirection = Vector3.zero;
        isRandomWalk = false;
        if (randWalkCoroutine != null) StopCoroutine(randWalkCoroutine);

    }
}
