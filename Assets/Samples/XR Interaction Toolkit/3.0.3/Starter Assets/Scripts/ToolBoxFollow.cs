using UnityEngine;

public class ToolboxFollow : MonoBehaviour
{
    [SerializeField] public Transform playerTransform; // Reference to the player
    [SerializeField] private Vector3 offset = new Vector3(0, 1, -0.5f); // Offset from the player's position

    private void Update()
    {
        if (playerTransform != null)
        {
            // Update position while keeping rotation fixed
            transform.position = playerTransform.position + offset;
        }
    }
}
