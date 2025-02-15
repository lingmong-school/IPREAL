
using UnityEngine;

/// <summary>
/// Handles the recycling logic by checking if the correct rubbish type is placed in the bin.
/// </summary>
public class RecyclingBin : MonoBehaviour
{
    public enum RubbishType
    {
        Paper,
        Plastic,
        Metal,
        Glass
    }

    public RubbishType acceptedType; // The type of rubbish the bin accepts
    public Animator tourGuideAnimator; // Reference to the tour guide's animator
    public AudioSource correctAudioSource; // AudioSource for correct recycling voiceline
    public AudioSource wrongAudioSource; // AudioSource for incorrect recycling voiceline
    public GameObject objectToEnable; // The GameObject to enable after 4 objects enter

    private static int objectCount = 0; // Tracks total objects entered

    private void OnTriggerEnter(Collider other)
    {
        objectCount++; // Increment the counter when any object enters

        if (other.CompareTag(acceptedType.ToString())) // Correct type
        {
            Debug.Log($"Accepted: {other.gameObject.name} is {acceptedType} and can be recycled here!");

            // Play correct animation and voice line
            if (tourGuideAnimator != null)
                tourGuideAnimator.SetTrigger("IsCorrect");

            PlayVoiceLine(correctAudioSource);
        }
        else // Incorrect type
        {
            Debug.Log($"Rejected: {other.gameObject.name} is not {acceptedType} and cannot be recycled here.");

            // Play wrong animation and voice line
            if (tourGuideAnimator != null)
                tourGuideAnimator.SetTrigger("IsWrong");

            PlayVoiceLine(wrongAudioSource);
        }

        // Check if 4 objects have been processed
        if (objectCount >= 4)
        {
            EnableGameObject();
        }
    }

    /// <summary>
    /// Plays a voice line from the specified AudioSource.
    /// </summary>
    /// <param name="audioSource">The AudioSource to play from.</param>
    private void PlayVoiceLine(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // Stop any currently playing audio
            audioSource.Play(); // Play the voice line
        }
    }

    /// <summary>
    /// Enables the specified GameObject when 4 objects have entered the bins.
    /// </summary>
    private void EnableGameObject()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log("4 objects have been processed. Enabling GameObject: " + objectToEnable.name);
        }
    }
}
