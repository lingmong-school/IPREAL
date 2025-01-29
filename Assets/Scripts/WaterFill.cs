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

        waterMaterial.color = targetColor; // Ensure the final color is set
    }
}
