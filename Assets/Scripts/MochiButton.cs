
using UnityEngine;

public class MochiButton : MonoBehaviour
{
    [SerializeField] private string requiredTag = "mochi"; // The tag to check for
    [SerializeField] private Animator buttonAnimator; // Reference to the Animator for the button
    private bool isInteractable = false; // Determines if the button can be interacted with

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the required tag
        if (other.CompareTag(requiredTag))
        {
            isInteractable = true; // Enable interaction
            Debug.Log("Mochi detected. Button is now interactable.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger has the required tag
        if (other.CompareTag(requiredTag))
        {
            isInteractable = false; // Disable interaction
            Debug.Log("Mochi exited. Button is no longer interactable.");
        }
    }

    // This method is called when the button is poked
    public void OnPoke()
    {
        if (isInteractable)
        {
            Debug.Log("Mochi button has been poked!");
            PlayButtonAnimation();
        }
        else
        {
            Debug.Log("Button is not interactable. No mochi detected.");
        }
    }

    private void PlayButtonAnimation()
    {
        if (buttonAnimator != null)
        {
            buttonAnimator.SetTrigger("Poke");
            Debug.Log("Button animation triggered.");
        }
        else
        {
            Debug.LogWarning("Button Animator is not assigned.");
        }
    }
}
