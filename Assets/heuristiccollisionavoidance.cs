using UnityEngine;

public class AvoidRearCollisionWithColliders : MonoBehaviour
{
    public Transform nextCheckpoint;  // Assign the next checkpoint
    public float normalSpeed = 10f;   // Default speed
    public float slowdownFactor = 0.5f;  // Slowdown percentage
    public float stopDistance = 3f;   // Distance at which AI stops

    private float currentSpeed;  // Adjusted speed
    private bool shouldSlowDown = false;
    private bool shouldStop = false;

    void Start()
    {
        currentSpeed = normalSpeed;  // Start at normal speed
    }

    void Update()
    {
        if (nextCheckpoint == null) return;

        // If player is in trigger, slow down or stop
        if (shouldStop)
        {
            currentSpeed = 0f;  // Stop completely
        }
        else if (shouldSlowDown)
        {
            currentSpeed = normalSpeed * slowdownFactor;  // Slow down
        }
        else
        {
            currentSpeed = normalSpeed;  // Resume normal speed
        }

        MoveObject();
    }

    void MoveObject()
    {
        if (currentSpeed > 0)  // Move only if not stopped
        {
            transform.position = Vector3.MoveTowards(transform.position, nextCheckpoint.position, currentSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure the player's car has the "Player" tag
        {
            float distanceToPlayer = Vector3.Distance(transform.position, other.transform.position);

            if (distanceToPlayer < stopDistance)
            {
                shouldStop = true;  // Stop if too close
            }
            else
            {
                shouldSlowDown = true;  // Slow down if detected
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldSlowDown = false;
            shouldStop = false;  // Resume normal movement
        }
    }
}
