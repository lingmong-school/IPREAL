using UnityEngine;
using System.Collections.Generic;

public class AvaTour : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float checkpointThreshold = 1f; // How close before stopping at checkpoint

    [Header("Checkpoints")]
    public List<Checkpoint> checkpoints = new List<Checkpoint>(); // Assign in Inspector

    private int currentCheckpointIndex = 0;
    private bool isMoving = false;
    private bool playerInsideTrigger = false; // Player must be inside the trigger zone

    private void Update()
    {
        if (playerInsideTrigger) // Only move when the player is inside the trigger zone
        {
            MoveToCheckpoint();
        }
    }

    /// <summary>
    /// Moves the tour guide towards the next checkpoint if its assigned bool is true.
    /// </summary>
    private void MoveToCheckpoint()
    {
        if (currentCheckpointIndex >= checkpoints.Count) return; // No more checkpoints

        Checkpoint targetCheckpoint = checkpoints[currentCheckpointIndex];

        if (targetCheckpoint.isCompleted) // Only move if the checkpoint's bool is checked
        {
            if (!isMoving)
            {
                Debug.Log($"[AvaTour] Moving towards: {targetCheckpoint.gameObject.name}");
                isMoving = true;
            }

            // Rotate towards the target
            Vector3 direction = (targetCheckpoint.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, targetCheckpoint.transform.position, moveSpeed * Time.deltaTime);

            // Check if reached checkpoint
            if (Vector3.Distance(transform.position, targetCheckpoint.transform.position) < checkpointThreshold)
            {
                Debug.Log($"[AvaTour] Reached {targetCheckpoint.gameObject.name}!");
                isMoving = false;
                targetCheckpoint.OnReached(); // Call the checkpoint function

                // Move to the next checkpoint
                currentCheckpointIndex++;
            }
        }
    }

    /// <summary>
    /// Detects when the player enters Ava's trigger zone.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            Debug.Log("[AvaTour] Player entered the trigger zone. Tour guide is active.");
        }
    }

    /// <summary>
    /// Detects when the player exits Ava's trigger zone.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            isMoving = false;
            Debug.Log("[AvaTour] Player exited the trigger zone. Tour guide has stopped.");
        }
    }
}
