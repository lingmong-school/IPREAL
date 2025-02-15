using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the tour guide animation and voice lines for the QNA step.
/// </summary>
public class AvaQNA : MonoBehaviour
{
    public Animator tourGuideAnimator; // Reference to the tour guide's Animator
    public AudioSource firstVoiceLine; // First voice line AudioSource
    public AudioSource secondVoiceLine; // Second voice line AudioSource

    /// <summary>
    /// Starts the QNA step by triggering the animation and playing two voice lines sequentially.
    /// Call this function from a button or event.
    /// </summary>
    public void StartQNAStep()
    {
        Debug.Log("Starting QNA step...");

        // Trigger the animation
        if (tourGuideAnimator != null)
            tourGuideAnimator.SetTrigger("ToQNA");

        // Start playing the voice lines sequentially
        StartCoroutine(PlayVoiceLines());
    }

    /// <summary>
    /// Plays the first voice line, then waits for it to finish before playing the second one.
    /// </summary>
    private IEnumerator PlayVoiceLines()
    {
        if (firstVoiceLine != null)
        {
            firstVoiceLine.Stop(); // Ensure it's not already playing
            firstVoiceLine.Play();
            yield return new WaitForSeconds(firstVoiceLine.clip.length); // Wait for first clip to finish
        }

        if (secondVoiceLine != null)
        {
            secondVoiceLine.Stop();
            secondVoiceLine.Play();
        }
    }
}
