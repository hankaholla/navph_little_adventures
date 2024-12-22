using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public bool isPaused = false;    // keeps track of the pause state
    private static PauseMenu instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // destroy duplicate instances
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // persist this instance
        }
    }


    // show pause menu and pause the game
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
    }

    // hide pause menu and resume the game
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
    }

    // restart the game (reload the current scene)
    public void RestartGame()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // stop play mode in Unity Editor
        #else
        Application.Quit();  // quit game when in build mode
        #endif
    }

    // return to main menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        pauseMenuCanvas.SetActive(false);  // hide the pause menu
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
