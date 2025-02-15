using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects when the player enters the Welcome checkpoint trigger zone.
/// </summary>
public class WelcomeCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the correct tag
        {
            Debug.Log("Player entered Welcome Checkpoint.");
            // Future logic can be added here
        }
    }
}
