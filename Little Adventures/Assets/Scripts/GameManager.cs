using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PauseMenu pauseMenuScript;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // persist through scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // detect ESC key press to toggle the pause menu
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
