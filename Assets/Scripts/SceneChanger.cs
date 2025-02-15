using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene changes using scene indexes.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// Changes to Scene 1 (index 0).
    /// </summary>
    public void ChangeScene1()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Changes to Scene 2 (index 1).
    /// </summary>
    public void ChangeScene2()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Changes to Scene 3 (index 2).
    /// </summary>
    public void ChangeScene3()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Changes to Scene 4 (index 3).
    /// </summary>
    public void ChangeScene4()
    {
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Changes to Scene 5 (index 4).
    /// </summary>
    public void ChangeScene5()
    {
        SceneManager.LoadScene(4);
    }
}
