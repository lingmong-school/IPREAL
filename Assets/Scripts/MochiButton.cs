
using UnityEngine;

/// <summary>
/// Handles interaction with the Mochi button, triggering an animation, playing a sound,
/// enabling a GameObject, and updating the GameManager when poked.
/// </summary>
public class MochiButton : MonoBehaviour
{
    [SerializeField] private string requiredTag = "mochi"; // The tag to check for
    [SerializeField] private Animator targetAnimator;      // Reference to the Animator of the target GameObject
    [SerializeField] private AudioSource audioSource;      // Reference to the AudioSource
    [SerializeField] private AudioClip pokeSound;          // The sound to play when poked
    [SerializeField] private GameObject objectToEnable;    // The GameObject to enable when poked

    private bool isInteractable = false;                  // Determines if the button can be interacted with

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            isInteractable = true;
            Debug.Log("Mochi detected. Button is now interactable.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            isInteractable = false;
            Debug.Log("Mochi exited. Button is no longer interactable.");
        }
    }

    /// <summary>
    /// Called when the button is poked.
    /// </summary>
    public void OnPoke()
    {
        if (isInteractable)
        {
            Debug.Log("Mochi button has been poked!");
            SetCookTrigger();
            PlayPokeSound();
            EnableGameObject();
            UpdateGameManager();
        }
        else
        {
            Debug.Log("Button is not interactable. No mochi detected.");
        }
    }

    /// <summary>
    /// Triggers the "Cook" animation.
    /// </summary>
    private void SetCookTrigger()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger("Cook");
            Debug.Log("Animation trigger 'Cook' has been set.");
        }
        else
        {
            Debug.LogWarning("Target Animator is not assigned.");
        }
    }

    /// <summary>
    /// Plays the poke sound effect.
    /// </summary>
    private void PlayPokeSound()
    {
        Debug.Log("AudioSource assigned? " + (audioSource != null));
        Debug.Log("PokeSound assigned? " + (pokeSound != null));

        if (audioSource != null && pokeSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(pokeSound);
            Debug.Log("Poke sound played.");
        }
        else
        {
            Debug.LogWarning("AudioSource or PokeSound is not assigned!");
        }
    }

    /// <summary>
    /// Enables the assigned GameObject.
    /// </summary>
    private void EnableGameObject()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log("Object enabled: " + objectToEnable.name + " | Active: " + objectToEnable.activeSelf);
        }
        else
        {
            Debug.LogWarning("No object assigned to enable.");
        }
    }

    /// <summary>
    /// Updates the GameManager by setting MochiDone to true and pushing progress to Firebase.
    /// </summary>
    private void UpdateGameManager()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CompleteActivity("Mochi"); // Mark Mochi as complete
            GameManager.Instance.PushProgressToFirebase(); // Sync progress with Firebase
            Debug.Log("GameManager updated: MochiDone set to TRUE and progress pushed.");
        }
        else
        {
            Debug.LogWarning("GameManager instance not found!");
        }
    }
}
