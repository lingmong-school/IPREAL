
using UnityEngine;

/// <summary>
/// Manages answer validation, plays animations, and sound effects for correct and wrong answers.
/// </summary>
public class AnswerManager : MonoBehaviour
{
    [SerializeField] private Animator anim; // Reference to the Animator component
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource
    [SerializeField] private AudioClip correctAnswerClip; // Sound for correct answer
    [SerializeField] private AudioClip wrongAnswerClip; // Sound for wrong answer

    /// <summary>
    /// Called when the correct answer is chosen.
    /// </summary>
    public void CorrectAns()
    {
        Debug.Log("Correct Answer!");

        // Play animation
        if (anim != null)
        {
            anim.SetTrigger("IsRight");
        }
        else
        {
            Debug.LogWarning("AnswerManager: Animator is not assigned!");
        }

        // Play correct answer sound
        PlaySound(correctAnswerClip);
    }

    /// <summary>
    /// Called when the wrong answer is chosen.
    /// </summary>
    public void WrongAns()
    {
        Debug.Log("Wrong Answer. Try again!");

        // Play animation
        if (anim != null)
        {
            anim.SetTrigger("IsWrong");
        }
        else
        {
            Debug.LogWarning("AnswerManager: Animator is not assigned!");
        }

        // Play wrong answer sound
        PlaySound(wrongAnswerClip);
    }

    /// <summary>
    /// Plays a sound clip if assigned.
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AnswerManager: AudioSource or AudioClip is missing!");
        }
    }
}
