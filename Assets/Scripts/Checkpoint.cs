
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public bool isCompleted = false; // When true, the tour guide moves to this point

    /// <summary>
    /// Called when the tour guide reaches this checkpoint.
    /// </summary>
    public void OnReached()
    {
        Debug.Log($"[Checkpoint] {gameObject.name} reached!");
        // Add activity logic here
    }
}
