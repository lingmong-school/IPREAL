using UnityEngine;

public class AvaStability : MonoBehaviour
{
    private Quaternion initialRotation;

    private void Start()
    {
        // Save the initial Y-axis rotation so Ava can turn
        initialRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        // Lock X and Z rotation while allowing Y-axis rotation
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
