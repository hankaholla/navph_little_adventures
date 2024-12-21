using UnityEngine;
using UnityEngine.SceneManagement;  // For loading scenes
using UnityEngine.UI;  // For accessing UI elements

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;  // Reference to the PauseMenu Canvas
    public bool isPaused = false;      // Keeps track of the pause state

    // Show pause menu and pause the game
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  // Stop game time
        pauseMenuCanvas.SetActive(true);  // Show the pause menu
    }

    // Hide pause menu and resume the game
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Resume game time
        pauseMenuCanvas.SetActive(false);  // Hide the pause menu
    }

    // Restart the game (reload the current scene)
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Make sure time resumes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload current scene - city scene
    }

    // Quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode in Unity Editor
        #else
        Application.Quit();  // quit game when in build mode
        #endif
    }

    // return to main menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleMusic(bool isOn)
    {
        if (MusicManager.instance != null)
        {
            // dynamically set the toggle reference to the pause menu's toggle
            MusicManager.instance.SetToggleReference(GetComponentInChildren<Toggle>());
            MusicManager.instance.OnToggleChanged(isOn);
        }
        else
        {
            Debug.LogWarning("MusicManager instance not found.");
        }
    }
}
