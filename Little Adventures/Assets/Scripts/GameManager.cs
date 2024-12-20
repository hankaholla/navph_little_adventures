using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PauseMenu pauseMenuScript; // Reference to the PauseMenu script

    void Update()
    {
        // Detect Escape key press to toggle the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuScript.isPaused)
            {
                pauseMenuScript.ResumeGame();
            }
            else
            {
                pauseMenuScript.PauseGame();
            }
        }
    }
}
