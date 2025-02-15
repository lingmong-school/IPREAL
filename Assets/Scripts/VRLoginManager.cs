using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;


public class VRLoginManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject loginPanel;
    public GameObject configurePanel;

    [Header("Login UI Elements")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;
    public GameObject loginButton;

    [Header("Class & Student Selection UI")]
    public TMP_Dropdown classDropdown;
    public TMP_Dropdown studentDropdown;
    public GameObject startGameButton;

    private Dictionary<string, string> classIDs = new Dictionary<string, string>();
    private Dictionary<string, string> studentIDs = new Dictionary<string, string>();

    private string selectedClassID;
    private string selectedStudentID;

    private void Start()
    {
        loginButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoginTeacher());
        startGameButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartGame());

        configurePanel.SetActive(false);
        startGameButton.SetActive(false);
    }

    public void LoginTeacher()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        FirebaseManager.Instance.auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                FirebaseUser user = task.Result.User;
                Debug.Log("Teacher Logged In: " + user.Email);
                statusText.text = "Login Successful!";

                loginPanel.SetActive(false);
                configurePanel.SetActive(true);
                LoadClasses();
            }
            else
            {
                Debug.LogError("Login Failed: " + task.Exception);
                statusText.text = "Login Failed. Please try again.";
            }
        });
    }

    public void LoadClasses()
    {
        FirebaseManager.Instance.databaseReference.Child("classes").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                classDropdown.ClearOptions();
                classIDs.Clear();

                List<string> classNames = new List<string>();

                foreach (var classData in snapshot.Children)
                {
                    string classID = classData.Key;
                    string className = classData.Child("name").Value.ToString();

                    classNames.Add(className);
                    classIDs[className] = classID;
                }

                classDropdown.AddOptions(classNames);
                classDropdown.onValueChanged.AddListener(delegate { LoadStudents(); });
            }
        });
    }

    public void LoadStudents()
    {
        selectedClassID = classIDs[classDropdown.options[classDropdown.value].text];

        FirebaseManager.Instance.databaseReference.Child("classes").Child(selectedClassID).Child("students").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                studentDropdown.ClearOptions();
                studentIDs.Clear();

                List<string> studentNames = new List<string>();

                foreach (var studentData in snapshot.Children)
                {
                    string studentID = studentData.Key;
                    string studentName = studentData.Child("name").Value.ToString();

                    studentNames.Add(studentName);
                    studentIDs[studentName] = studentID;
                }

                studentDropdown.AddOptions(studentNames);
                studentDropdown.onValueChanged.AddListener(delegate { SelectStudent(); });

                startGameButton.SetActive(studentDropdown.options.Count > 0);
            }
        });
    }

    public void SelectStudent()
    {
        selectedStudentID = studentIDs[studentDropdown.options[studentDropdown.value].text];
        Debug.Log("Selected Student ID: " + selectedStudentID);
    }

    public void StartGame()
    {
        Debug.Log("Starting Game with Student: " + selectedStudentID);
        GameManager.Instance.SetStudent(selectedClassID, selectedStudentID);
    }
}
