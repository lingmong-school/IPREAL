using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPour : MonoBehaviour
{
    public Transform teapotSpout;
    public ParticleSystem liquidStream;
    public WaterFill waterFill; // Reference to the WaterFill script
    public float pourRate = 0.1f; // Rate at which liquid is added per second

    private bool isPouring = false;

    void Update()
    {
        float tiltAngle = Vector3.Angle(transform.up, Vector3.up);

        if (tiltAngle > 30f) // Adjust this threshold for teapot tilt
        {
            StartPouring();
        }
        else
        {
            StopPouring();
        }

        if (isPouring)
        {
            // Gradually increase the water level in the container
            float newWaterLevel = Mathf.Clamp(waterFill.waterLevel + (pourRate * Time.deltaTime), 0.0f, 1.0f);
            waterFill.waterLevel = newWaterLevel; // Update the stored value
            waterFill.UpdateWaterLevel(newWaterLevel); // Pass the correct value to the method
        }
    }

    void StartPouring()
    {
        if (!isPouring)
        {
            isPouring = true;
            if (liquidStream != null)
            {
                liquidStream.Play(); // Start the liquid particle effect
            }
        }
    }

    void StopPouring()
    {
        if (isPouring)
        {
            isPouring = false;
            if (liquidStream != null)
            {
                liquidStream.Stop(); // Stop the liquid particle effect
            }
        }
    }
}
