using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the tour guide animation and voice lines for the Tea step.
/// </summary>
public class AvaTea : MonoBehaviour
{
    public Animator tourGuideAnimator; // Reference to the tour guide's Animator
    public AudioSource audioSource; // Single AudioSource for both voice lines
    public AudioClip firstVoiceClip; // First voice clip
    public AudioClip secondVoiceClip; // Second voice clip

    /// <summary>
    /// Starts the Tea step by triggering the animation and playing two voice lines sequentially.
    /// Call this function from a button or event.
    /// </summary>
    public void StartTeaStep()
    {
        Debug.Log("Starting Tea step...");

        // Trigger the animation
        if (tourGuideAnimator != null)
            tourGuideAnimator.SetTrigger("ToTea");

        // Start playing the voice lines sequentially
        StartCoroutine(PlayVoiceLines());
    }

    /// <summary>
    /// Plays the first voice clip, then waits for it to finish before playing the second one.
    /// </summary>
    private IEnumerator PlayVoiceLines()
    {
        if (audioSource != null)
        {
            if (firstVoiceClip != null)
            {
                audioSource.Stop(); // Ensure it's not already playing
                audioSource.clip = firstVoiceClip;
                audioSource.Play();
                yield return new WaitForSeconds(firstVoiceClip.length); // Wait for first clip to finish
            }

            if (secondVoiceClip != null)
            {
                audioSource.Stop();
                audioSource.clip = secondVoiceClip;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }
}
