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
    private bool isInsideTrigger = false; // ✅ Check if inside the trigger zone

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

        // ✅ Only increase water if pouring AND inside the trigger
        if (isPouring && isInsideTrigger)
        {
            float newWaterLevel = Mathf.Clamp(waterFill.waterLevel + (pourRate * Time.deltaTime), 0.0f, 1.0f);
            waterFill.waterLevel = newWaterLevel;
            waterFill.UpdateWaterLevel(newWaterLevel);
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

    // ✅ Detect entering trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PourZone")) // Ensure the zone is tagged correctly
        {
            isInsideTrigger = true;
            Debug.Log("Teapot entered pour zone.");
        }
    }

    // ✅ Detect exiting trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PourZone"))
        {
            isInsideTrigger = false;
            Debug.Log("Teapot exited pour zone.");
        }
    }
}
