using UnityEngine;
using System.Collections;

/// <summary>
/// Plays two voice lines sequentially when the scene starts.
/// </summary>
public class AvaMochiActivity : MonoBehaviour
{
    public AudioSource audioSource; // The shared AudioSource
    public AudioClip firstVoiceLine; // First voice line clip
    public AudioClip secondVoiceLine; // Second voice line clip

    private void Start()
    {
        StartCoroutine(PlayVoiceLines());
    }

    /// <summary>
    /// Plays the first voice line, waits for it to finish, then plays the second one.
    /// </summary>
    private IEnumerator PlayVoiceLines()
    {
        if (audioSource != null)
        {
            if (firstVoiceLine != null)
            {
                audioSource.clip = firstVoiceLine;
                audioSource.Play();
                yield return new WaitForSeconds(firstVoiceLine.length); // Wait for first clip to finish
            }

            if (secondVoiceLine != null)
            {
                audioSource.clip = secondVoiceLine;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("AvaMochiActivity: AudioSource is not assigned!");
        }
    }
}
