using UnityEngine;

public class MachaScoop : MonoBehaviour
{
    // Reference to the GameObject to enable
    public GameObject objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        // Debug log to check when something enters the trigger zone
        Debug.Log($"Trigger entered by: {other.name}");

        // Check if the entering object has the tag "Scoop"
        if (other.CompareTag("Scoop"))
        {
            // Enable the specified GameObject
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
                Debug.Log("Scoop detected. Object enabled!");
            }
            else
            {
                Debug.LogWarning("No GameObject assigned to enable!");
            }
        }
    }
}