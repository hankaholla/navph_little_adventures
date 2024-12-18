using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
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

    public void OpenKeyBindings()
    {
        // Logic to toggle Key Bindings Panel visibility.
    }

    public void ToggleMusic(bool isOn)
    {
        // Logic to turn music on/off.
    }
}

