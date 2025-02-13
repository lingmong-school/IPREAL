using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.Networking;


public class CameraScreenshot : MonoBehaviour
{
    public Camera inGameCamera;      // The in-game camera that captures the screenshot
    public RenderTexture renderTexture; // The RenderTexture linked to the camera

    // Supabase credentials (Replace with yours)
    private string supabaseUrl = "https://hiulgvnlkismjybyewsg.supabase.co";
    private string supabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImhpdWxndm5sa2lzbWp5Ynlld3NnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mzk0MzI1NTYsImV4cCI6MjA1NTAwODU1Nn0.cgIe7YpVIJOXy2VU6TQev8_ocxTzD9elT5dCyeaLKMs";
    private string storageBucket = "pics"; // Name of the Supabase storage bucket

    /// <summary>
    /// Public function to capture and upload a screenshot. Can be called from any button GameObject.
    /// </summary>
    public void CaptureAndUploadScreenshot()
    {
        Debug.Log("CaptureAndUploadScreenshot() called!");
        StartCoroutine(CaptureScreenshotCoroutine());
    }

    /// <summary>
    /// Captures a screenshot from the RenderTexture and uploads it to Supabase.
    /// </summary>
    private IEnumerator CaptureScreenshotCoroutine()
    {
        Debug.Log("📷 Capturing screenshot...");

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        if (renderTexture == null)
        {
            Debug.LogError("❌ RenderTexture is not assigned!");
            yield break; // Stop the function if there's no RenderTexture
        }

        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();

        RenderTexture.active = currentRT;
        Debug.Log("✅ Screenshot captured!");

        byte[] screenshotBytes = screenshot.EncodeToPNG();
        Destroy(screenshot); // Free memory

        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"Screenshot_{timestamp}.png";

        yield return StartCoroutine(UploadToSupabase(fileName, screenshotBytes));
    }


    /// <summary>
    /// Uploads the screenshot to Supabase Storage.
    /// </summary>
    private IEnumerator UploadToSupabase(string fileName, byte[] fileData)
    {
        string uploadUrl = $"{supabaseUrl}/storage/v1/object/{storageBucket}/{fileName}";

        Debug.Log($"🔄 Uploading {fileName} to Supabase...");

        UnityWebRequest request = new UnityWebRequest(uploadUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(fileData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", $"Bearer {supabaseAnonKey}");
        request.SetRequestHeader("Content-Type", "image/png");
        request.SetRequestHeader("x-upsert", "true");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"✅ Screenshot uploaded successfully: {uploadUrl}");
        }
        else
        {
            Debug.LogError($"❌ Upload failed: {request.responseCode} - {request.error}");
            Debug.LogError($"🔍 Supabase Response: {request.downloadHandler.text}");
        }
    }


}