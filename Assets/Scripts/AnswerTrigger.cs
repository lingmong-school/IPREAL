using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerTrigger : MonoBehaviour
{
    private bool isCorrectAnswer = false; // Track if the correct block is inside

    private void OnTriggerEnter(Collider other)
    {
        // Check if an object enters the trigger zone
        if (other.CompareTag("Correct"))
        {
            isCorrectAnswer = true; // Store that the correct answer is inside
            Debug.Log("Correct block placed!");
        }
        else
        {
            isCorrectAnswer = false; // Incorrect block entered
            Debug.Log("Incorrect block placed.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the object leaves the trigger, reset the answer state
        if (other.CompareTag("Correct"))
        {
            isCorrectAnswer = false;
            Debug.Log("Correct block removed.");
        }
    }

    // Public method to check the answer when the button is clicked
    public void CheckAnswer()
    {
        if (isCorrectAnswer)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }
}
