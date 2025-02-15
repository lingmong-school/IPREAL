
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the Mochi pounding progress bar and triggers events when it's full.
/// </summary>
public class MochiCounterUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;         // Reference to the UI Image for the fill bar
    [SerializeField] private int maxHits = 10;          // Maximum number of hits to fill the bar
    [SerializeField] private GameObject pickupMochi;    // The mochi to enable when progress is full
    [SerializeField] private GameObject interactableMochi; // The interactable mochi to disable when progress is full
    [SerializeField] private AudioSource audioSource;   // Reference to the AudioSource
    [SerializeField] private AudioClip completionSound; // Sound to play when progress is full

    private int hitCounter = 0;                         // Counter to track the number of hits

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "Mallet"
        if (other.CompareTag("Mallet"))
        {
            // Increment the hit counter
            hitCounter++;

            // Update the progress bar fill amount
            UpdateProgressBar();

            // Log the updated counter value
            Debug.Log("Mochi hit! Counter: " + hitCounter);

            // Check if the progress bar is full
            if (hitCounter >= maxHits)
            {
                OnProgressBarFull();
            }
        }
    }

    private void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            // Calculate the fill amount as a percentage of maxHits
            progressBar.fillAmount = (float)hitCounter / maxHits;

            // Clamp the fill amount to prevent overflow
            progressBar.fillAmount = Mathf.Clamp(progressBar.fillAmount, 0f, 1f);
        }
    }

    private void OnProgressBarFull()
    {
        // Enable the pickup mochi
        if (pickupMochi != null)
        {
            pickupMochi.SetActive(true);
            Debug.Log("Pickup mochi has been enabled.");
        }

        // Disable the interactable mochi
        if (interactableMochi != null)
        {
            interactableMochi.SetActive(false);
            Debug.Log("Interactable mochi has been disabled.");
        }

        // Play the completion sound
        if (audioSource != null && completionSound != null)
        {
            audioSource.Stop();  // Stop any currently playing sounds
            audioSource.PlayOneShot(completionSound);
            Debug.Log("Completion sound played.");
        }
    }
}
