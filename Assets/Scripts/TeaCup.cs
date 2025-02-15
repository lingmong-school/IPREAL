
using UnityEngine;

/// <summary>
/// Handles interaction with the Tea Cup, enabling an object when a "Lump" enters the trigger zone,
/// disabling another object, and playing a sound.
/// </summary>
public class TeaCup : MonoBehaviour
{
    public GameObject objectToEnable; // The GameObject to enable
    public GameObject objectToDisable; // The GameObject to disable
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip activationSound; // The sound to play when enabled

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the tag "Lump"
        if (other.CompareTag("Lump"))
        {
            // Enable the specified GameObject
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
                Debug.Log("Lump detected in the trigger zone. Object enabled!");
            }
            else
            {
                Debug.LogWarning("No GameObject assigned to enable!");
            }

            // Disable the specified GameObject
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
                Debug.Log("Object disabled!");
            }

            // Play activation sound
            PlayActivationSound();
        }
    }

    /// <summary>
    /// Plays the activation sound when the object is enabled.
    /// </summary>
    private void PlayActivationSound()
    {
        if (audioSource != null && activationSound != null)
        {
            audioSource.Stop(); // Stop any currently playing sound
            audioSource.PlayOneShot(activationSound);
            Debug.Log("Activation sound played.");
        }
        else
        {
            Debug.LogWarning("AudioSource or activation sound not assigned!");
        }
    }
}
