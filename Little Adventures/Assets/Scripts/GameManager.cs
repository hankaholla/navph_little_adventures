using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PauseMenu pauseMenuScript; // Reference to the PauseMenu script
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
