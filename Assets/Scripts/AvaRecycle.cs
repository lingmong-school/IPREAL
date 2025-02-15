using UnityEngine;
using System.Collections;


public class AvaRecycle : MonoBehaviour
{
    [Header("Tour Guide Components")]
    public GameObject tourGuide;       // Assign Tour Guide GameObject
    public Animator tourGuideAnimator; // Animator component for animations

    [Header("Voice Lines")]
    public AudioSource voiceLine1;     // First voice line AudioSource
    public AudioSource voiceLine2;     // Second voice line AudioSource

    [Header("Game Object to Enable")]
    public GameObject objectToEnable;  // Assign the GameObject to enable after voice lines

    private Coroutine moveRoutine;

    public void Move()
    {
        if (tourGuideAnimator == null)
        {
            Debug.LogError("Tour Guide Animator not assigned!");
            return;
        }

        // Trigger the "ToBin" animation
        tourGuideAnimator.SetTrigger("ToBin");

        // Start Coroutine to wait for animation to finish
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(WaitForAnimationToEnd());
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        if (tourGuideAnimator == null)
            yield break;

        // Wait for animation to complete
        yield return new WaitForSeconds(GetAnimationLength());

        // Play the voice lines sequentially
        yield return StartCoroutine(PlayVoiceLines());

        // After all voicelines, enable the object
        EnableObject();
    }

    private IEnumerator PlayVoiceLines()
    {
        if (voiceLine1 != null)
        {
            voiceLine1.Play();
            yield return new WaitForSeconds(voiceLine1.clip.length);
        }
        else
        {
            Debug.LogError("Voice Line 1 not assigned!");
        }

        if (voiceLine2 != null)
        {
            voiceLine2.Play();
            yield return new WaitForSeconds(voiceLine2.clip.length);
        }
        else
        {
            Debug.LogError("Voice Line 2 not assigned!");
        }
    }

    private void EnableObject()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log("GameObject enabled after all voice lines.");
        }
        else
        {
            Debug.LogError("Object to Enable is not assigned!");
        }
    }

    private float GetAnimationLength()
    {
        AnimatorStateInfo animState = tourGuideAnimator.GetCurrentAnimatorStateInfo(0);
        return animState.length;
    }
}
