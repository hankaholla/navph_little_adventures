using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour
{
    public GameObject KeyBindingsPanel;
    public GameObject CreditsPanel;

    public void ToggleKeyBindingsPanel()
    {
        bool isActive = KeyBindingsPanel.activeSelf; // Check if the panel is currently active
        KeyBindingsPanel.SetActive(!isActive);      // Toggle the active state
    }

    public void ToggleCreditsPanel()
    {
        bool isActive = CreditsPanel.activeSelf; // Check if the panel is currently active
        CreditsPanel.SetActive(!isActive);      // Toggle the active state
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Home");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stop Play mode in Unity Editor
        #else
        Application.Quit(); // Quit the application in a built version
        #endif
        Debug.Log("Quit Game");
    }

    public void ToggleMusic(bool isOn)
    {
        if (MusicManager.instance != null)
        {
            // dynamically set the toggle reference to the main menu's toggle
            MusicManager.instance.SetToggleReference(GetComponentInChildren<Toggle>());
            MusicManager.instance.OnToggleChanged(isOn);
        }
        else
        {
            Debug.LogWarning("MusicManager instance not found.");
        }
    }
}

