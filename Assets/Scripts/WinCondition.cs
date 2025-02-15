using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

/// <summary>
/// Handles the final win condition by marking QNA as completed, resetting player data, and returning to the main menu.
/// </summary>
public class WinCondition : MonoBehaviour
{
    /// <summary>
    /// Marks QNA as completed, resets progress, signs out the player, and returns to the main scene.
    /// </summary>
    public void CompleteExperience()
    {
        if (GameManager.Instance != null)
        {
            // Mark QNA as complete
            GameManager.Instance.CompleteActivity("QNA");
            Debug.Log("WinCondition: QNA step completed.");

            // Push final progress to Firebase
            GameManager.Instance.PushProgressToFirebase();
            Debug.Log("WinCondition: Final progress pushed to Firebase.");

            // Reset Player Progress
            ResetPlayerProgress();

            // Change to Main Menu Scene (Scene Index 0)
            SceneManager.LoadScene(0);
            Debug.Log("WinCondition: Returning to Main Menu.");
        }
        else
        {
            Debug.LogWarning("WinCondition: GameManager instance not found!");
        }
    }

    /// <summary>
    /// Resets player progress by clearing GameManager data and signing out the player.
    /// </summary>
    private void ResetPlayerProgress()
    {
        // Reset GameManager progress
        PlayerPrefs.DeleteAll(); // Clear saved progress
        Debug.Log("WinCondition: Player progress reset.");

        // Sign out from Firebase Authentication (if used)
        if (FirebaseAuth.DefaultInstance != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
            Debug.Log("WinCondition: Player signed out.");
        }
        else
        {
            Debug.LogWarning("WinCondition: FirebaseAuth instance not found!");
        }
    }
}
