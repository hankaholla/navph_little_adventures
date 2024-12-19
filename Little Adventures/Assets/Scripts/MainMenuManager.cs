using UnityEngine;
using UnityEngine.SceneManagement;

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
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

