
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Teleports the XR Origin to a specified GameObject's position when the scene starts.
/// </summary>
public class XRTeleportOnStart : MonoBehaviour
{
    [SerializeField] public Transform xrOrigin; // Reference to XR Origin
    [SerializeField] public Transform teleportTarget; // Reference to the target location

    private void Start()
    {
        TeleportXROrigin();
    }

    /// <summary>
    /// Moves the XR Origin to the teleport target's position and rotation.
    /// </summary>
    private void TeleportXROrigin()
    {
        if (xrOrigin != null && teleportTarget != null)
        {
            xrOrigin.position = teleportTarget.position;
            xrOrigin.rotation = teleportTarget.rotation;
            Debug.Log("XR Origin teleported to: " + teleportTarget.name);
        }
        else
        {
            Debug.LogWarning("XR Origin or Teleport Target is not assigned!");
        }
    }
}
