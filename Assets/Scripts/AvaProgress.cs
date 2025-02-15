using UnityEngine;
using System.Collections;


public class AvaProgress : MonoBehaviour
{
    [Header("Tour Guide Components")]
    public GameObject tourGuide;        // Assign Tour Guide GameObject
    public AudioSource voiceLine1;      // First voice line AudioSource
    public AudioSource voiceLine2;      // Second voice line AudioSource
    public Animator tourGuideAnimator;  // Animator component for animations

    [Header("Game Object to Enable")]
    public GameObject objectToEnable;   // Assign the GameObject to enable after voice lines

    private void Start()
    {
        CheckPlayerProgress();
    }

    private void CheckPlayerProgress()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        // Check if any activity is completed
        bool hasProgress = GameManager.Instance.koiDone ||
                           GameManager.Instance.recycleDone ||
                           GameManager.Instance.mochiDone ||
                           GameManager.Instance.teaDone ||
                           GameManager.Instance.qnaDone;

        if (!hasProgress)
        {
            WelcomeLogic();
        }
    }

    private void WelcomeLogic()
    {
        Debug.Log("Welcome Logic Triggered - No activities completed.");

        // Trigger Wave Animation
        if (tourGuideAnimator != null)
        {
            tourGuideAnimator.SetTrigger("IsWave");
        }
        else
        {
            Debug.LogError("Tour Guide Animator not assigned!");
        }

        // Start playing voice lines
        StartCoroutine(PlayVoicelines());
    }

    private IEnumerator PlayVoicelines()
    {
        if (voiceLine1 != null)
        {
            voiceLine1.Play();
            yield return new WaitForSeconds(voiceLine1.clip.length); // Wait for first voiceline to finish
        }
        else
        {
            Debug.LogError("Voice Line 1 not assigned!");
        }

        if (voiceLine2 != null)
        {
            voiceLine2.Play();
            yield return new WaitForSeconds(voiceLine2.clip.length); // Wait for second voiceline to finish
        }
        else
        {
            Debug.LogError("Voice Line 2 not assigned!");
        }

        // Enable the GameObject after both voicelines finish
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log("GameObject enabled after voice lines.");
        }
        else
        {
            Debug.LogError("Object to Enable is not assigned!");
        }
    }
}
