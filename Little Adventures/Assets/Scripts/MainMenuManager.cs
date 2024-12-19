using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour
{
    public GameObject KeyBindingsPanel;

    public void ToggleKeyBindingsPanel()
    {
        bool isActive = KeyBindingsPanel.activeSelf; // Check if the panel is currently active
        KeyBindingsPanel.SetActive(!isActive);      // Toggle the active state
    }

    public void StartGame()
    {
        SceneManager.LoadScene("City");
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
}

