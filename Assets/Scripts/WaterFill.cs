using UnityEngine;
using System.Collections;

public class WaterFill : MonoBehaviour
{
    [Header("Water Level Settings")]
    public Transform waterObject; // The water object to adjust
    [Range(0, 1)] public float waterLevel = 0.0f; // Inspector slider for water level (starts at 0)
    public float minHeight = 0.0f; // Minimum water height (starts at 0)
    public float maxHeight = 1.0f; // Maximum water height

    [Header("Water Color Settings")]
    public Material waterMaterial; // Material of the water object
    public Color defaultColor = Color.clear; // Initial water color
    public Color matchaColor = Color.green; // Matcha green color
    public float colorChangeDuration = 3.0f; // Time taken to fully turn green

    [Header("Matcha Activation")]
    public GameObject matchaActivator; // The required object that must be enabled for color change

    [Header("Audio & UI Settings")]
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip colorChangeSound; // Sound to play when color change completes
    public GameObject objectToEnable; // GameObject to enable when color change is completed

    private Vector3 initialScale; // Store the initial scale
    private Coroutine colorChangeCoroutine; // Coroutine reference
    private bool isColorChanged = false; // Track if the water has turned green

    void Start()
    {
        if (waterObject != null)
        {
            initialScale = waterObject.localScale;
            waterObject.localScale = new Vector3(initialScale.x, 0.0f, initialScale.z);
            waterObject.gameObject.SetActive(false); // Disable water initially
        }

        if (waterMaterial != null)
        {
            waterMaterial.color = defaultColor; // Set the default color
        }

        if (objectToEnable != null)
        {
            objectToEnable.SetActive(false); // Ensure the target object is disabled initially
        }
    }

    void Update()
    {
        UpdateWaterLevel(waterLevel);
    }

    public void UpdateWaterLevel(float value)
    {
        if (waterObject != null)
        {
            if (value <= 0.0f)
            {
                waterObject.gameObject.SetActive(false);
            }
            else
            {
                waterObject.gameObject.SetActive(true);
                float newHeight = Mathf.Lerp(minHeight, maxHeight, value);
                waterObject.localScale = new Vector3(initialScale.x, newHeight, initialScale.z);
            }
        }
    }

    public void StartColorChange()
    {
        if (!isColorChanged) // Prevent color change if it has already turned green
        {
            if (matchaActivator != null && matchaActivator.activeSelf)
            {
                if (colorChangeCoroutine != null)
                    StopCoroutine(colorChangeCoroutine);

                colorChangeCoroutine = StartCoroutine(ChangeWaterColorOverTime(matchaColor));
                isColorChanged = true; // Lock the color change
            }
            else
            {
                Debug.Log("Color change blocked: Matcha ingredient is not enabled!");
            }
        }
    }

    private IEnumerator ChangeWaterColorOverTime(Color targetColor)
    {
        if (waterMaterial == null) yield break;

        Color startColor = waterMaterial.color;
        float elapsedTime = 0f;

        while (elapsedTime < colorChangeDuration)
        {
            waterMaterial.color = Color.Lerp(startColor, targetColor, elapsedTime / colorChangeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color is set
        waterMaterial.color = targetColor;

        // Trigger completion actions
        OnColorChangeComplete();
    }

    /// <summary>
    /// Plays the completion sound, enables the target GameObject, and updates the GameManager when the water turns green.
    /// </summary>
    private void OnColorChangeComplete()
    {
        // Play sound
        if (audioSource != null && colorChangeSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(colorChangeSound);
            Debug.Log("Color change sound played.");
        }
        else
        {
            Debug.LogWarning("AudioSource or colorChangeSound is not assigned!");
        }

        // Enable the object
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log("Object enabled: " + objectToEnable.name);
        }
        else
        {
            Debug.LogWarning("No object assigned to enable.");
        }

        // Update the GameManager
        UpdateGameManager();
    }

    /// <summary>
    /// Updates the GameManager by setting TeaDone to true and pushing progress to Firebase.
    /// </summary>
    private void UpdateGameManager()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CompleteActivity("Tea"); // Mark Tea as complete
            GameManager.Instance.PushProgressToFirebase(); // Sync progress with Firebase
            Debug.Log("GameManager updated: TeaDone set to TRUE and progress pushed.");
        }
        else
        {
            Debug.LogWarning("GameManager instance not found!");
        }
    }
}
