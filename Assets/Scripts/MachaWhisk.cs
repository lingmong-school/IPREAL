using UnityEngine;

public class MatchaWhisk : MonoBehaviour
{
    public WaterFill waterFill; // Reference to the WaterFill script
    public string whiskTag = "Whisk"; // Tag for the whisk object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(whiskTag))
        {
            // Start the progressive color change to matcha if conditions are met
            if (waterFill != null)
            {
                waterFill.StartColorChange();
            }
        }
    }
}
