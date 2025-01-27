
using UnityEngine;

public class TeaCup : MonoBehaviour
{
    // Reference to the GameObject to enable
    public GameObject objectToEnable;
    public GameObject objectToDisable;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the tag "Lump"
        if (other.CompareTag("Lump"))
        {
            // Enable the specified GameObject
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
                objectToDisable.SetActive(false);
                Debug.Log("Lump detected in the trigger zone. Object enabled!");
            }
            else
            {
                Debug.LogWarning("No GameObject assigned to enable!");
            }
        }
    }
}
