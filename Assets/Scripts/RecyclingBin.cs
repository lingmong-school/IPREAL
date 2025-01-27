using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RecyclingBin : MonoBehaviour
{
    public enum RubbishType
    {
        Paper,
        Plastic,
        Metal
    }

    [SerializeField] private RubbishType acceptedType; // The type of rubbish the bin accepts

    private void OnTriggerEnter(Collider other)
    {
        // Check the tag of the entering object
        if (other.CompareTag(acceptedType.ToString())) // Match tag with selected type
        {
            Debug.Log($"Accepted: {other.gameObject.name} is {acceptedType} and can be recycled here!");
        }
        else
        {
            Debug.Log($"Rejected: {other.gameObject.name} is not {acceptedType} and cannot be recycled here.");
        }
    }
}
