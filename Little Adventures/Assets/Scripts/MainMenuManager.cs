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
        // check if the panel is currently active
        bool isActive = KeyBindingsPanel.activeSelf; 
        KeyBindingsPanel.SetActive(!isActive);
    }

    public void ToggleCreditsPanel()
    {
        bool isActive = CreditsPanel.activeSelf;
        CreditsPanel.SetActive(!isActive);
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
        Application.Quit();
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

