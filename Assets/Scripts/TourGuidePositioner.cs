using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;


/// <summary>
/// Checks the GameManager for completed activities (MochiDone & TeaDone),
/// sets the Tour Guide's animation, and teleports XR Origin when a scene loads.
/// Calls StartTeaStep() after Mochi and StartQNAStep() after Tea.
/// </summary>
public class TourGuidePositioner : MonoBehaviour
{
    [SerializeField] public Animator tourGuideAnimator; // Reference to the Tour Guide's Animator
    [SerializeField] public Transform mochiSpawnPoint;  // The location to teleport XR Origin if MochiDone
    [SerializeField] public Transform teaSpawnPoint;    // The location to teleport XR Origin if TeaDone
    [SerializeField] public Transform xrOrigin;         // Reference to the XR Origin
    [SerializeField] public AvaTea avaTeaScript;        // Reference to AvaTea script
    [SerializeField] public AvaQNA avaQNAScript;        // Reference to AvaQNA script

    private bool hasInitialized = false; // Prevents multiple executions of SetTourGuideState()

    private void Start()
    {
        Debug.Log("TourGuidePositioner Initialized in Scene: " + SceneManager.GetActiveScene().name);

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager instance not found! Tour Guide position may not update correctly.");
            return;
        }

        // Ensure it only runs once per scene load
        if (!hasInitialized)
        {
            SetTourGuideState();
            hasInitialized = true;
        }
    }

    /// <summary>
    /// Checks the GameManager for completed activities and triggers the correct animation.
    /// TeaDone has higher priority than MochiDone.
    /// Teleports XR Origin to the correct location.
    /// Calls StartQNAStep() when TeaDone is triggered.
    /// </summary>
    private void SetTourGuideState()
    {
        if (GameManager.Instance.teaDone)
        {
            tourGuideAnimator.SetTrigger("TeaDone");
            Debug.Log("Tour Guide animation trigger 'TeaDone' set.");
            TeleportXROrigin(teaSpawnPoint);

            // Call StartQNAStep after Tea is processed
            if (avaQNAScript != null)
            {
                avaQNAScript.StartQNAStep();
                Debug.Log("StartQNAStep() has been called from AvaQNA.");
            }
            else
            {
                Debug.LogWarning("AvaQNA script is not assigned!");
            }
        }
        else if (GameManager.Instance.mochiDone)
        {
            tourGuideAnimator.SetTrigger("MochiDone");
            Debug.Log("Tour Guide animation trigger 'MochiDone' set.");
            TeleportXROrigin(mochiSpawnPoint);

            // Call StartTeaStep after Mochi is processed
            if (avaTeaScript != null)
            {
                avaTeaScript.StartTeaStep();
                Debug.Log("StartTeaStep() has been called from AvaTea.");
            }
            else
            {
                Debug.LogWarning("AvaTea script is not assigned!");
            }
        }
    }

    /// <summary>
    /// Teleports the XR Origin to a specified spawn point.
    /// </summary>
    private void TeleportXROrigin(Transform targetSpawnPoint)
    {
        if (xrOrigin != null && targetSpawnPoint != null)
        {
            Debug.Log("Before Teleport: XR Origin at " + xrOrigin.position);
            xrOrigin.position = targetSpawnPoint.position;
            xrOrigin.rotation = targetSpawnPoint.rotation;
            Debug.Log("After Teleport: XR Origin moved to " + targetSpawnPoint.position);
        }
        else
        {
            Debug.LogWarning("XR Origin or Target Spawn Point is not assigned!");
        }
    }
}
