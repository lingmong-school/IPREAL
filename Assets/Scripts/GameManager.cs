using UnityEngine;
using Firebase.Database;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string classID;
    private string studentID;

    public bool koiDone, recycleDone, mochiDone, teaDone, qnaDone;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetStudent(string selectedClassID, string selectedStudentID)
    {
        classID = selectedClassID;
        studentID = selectedStudentID;
    }

    public void CompleteActivity(string activity)
    {
        switch (activity)
        {
            case "Koi":
                koiDone = true;
                break;
            case "Recycle":
                recycleDone = true;
                break;
            case "Mochi":
                mochiDone = true;
                break;
            case "Tea":
                teaDone = true;
                break;
            case "QNA":
                qnaDone = true;
                break;
        }
    }

    public void PushProgressToFirebase()
    {
        if (string.IsNullOrEmpty(classID) || string.IsNullOrEmpty(studentID)) return;

        DatabaseReference studentRef = FirebaseManager.Instance.databaseReference
            .Child("classes").Child(classID).Child("students").Child(studentID);

        studentRef.Child("Koi").SetValueAsync(koiDone);
        studentRef.Child("Recycle").SetValueAsync(recycleDone);
        studentRef.Child("Mochi").SetValueAsync(mochiDone);
        studentRef.Child("Tea").SetValueAsync(teaDone);
        studentRef.Child("QNA").SetValueAsync(qnaDone);
    }
}
