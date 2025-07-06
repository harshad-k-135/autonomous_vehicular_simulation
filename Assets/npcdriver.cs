using UnityEngine;

public class CarAI : MonoBehaviour
{
    private Transform[] checkpoints;  // Stores all checkpoint transforms
    private int currentCheckpointIndex = 0;  // Start at the first checkpoint
    public float speed = 5f;  // Movement speed
    public float reachThreshold = 1f;  // Distance to consider a checkpoint "reached"

    void Start()
    {
        // Find the "Checkpoints" GameObject and get all children
        GameObject checkpointParent = GameObject.Find("checkpointA");

        if (checkpointParent == null)
        {
            Debug.LogError("No GameObject named 'Checkpoints' found! Make sure it's correctly named.");
            return;
        }

        // Store all children (checkpoints) in an array
        int childCount = checkpointParent.transform.childCount;
        checkpoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            checkpoints[i] = checkpointParent.transform.GetChild(i);
        }
    }

    void Update()
    {
        if (checkpoints == null || checkpoints.Length == 0) return; // Ensure there are checkpoints

        // Move towards the current checkpoint
        Transform targetCheckpoint = checkpoints[currentCheckpointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetCheckpoint.position, speed * Time.deltaTime);

        // Rotate towards next checkpoint smoothly
        Vector3 direction = (targetCheckpoint.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Check if reached the checkpoint
        if (Vector3.Distance(transform.position, targetCheckpoint.position) < reachThreshold)
        {
            currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Length; // Loop through waypoints
        }
    }
}