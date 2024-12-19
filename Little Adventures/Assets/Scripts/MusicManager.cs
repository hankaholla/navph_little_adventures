using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Toggle musicToggle;
    public AudioSource audioSource;
    private static MusicManager instance;  // Singleton instance

    void Awake()
    {
        // If there is already another instance of MusicManager, destroy this one
        if (instance != null)
        {
            Destroy(gameObject); // Destroy this object to keep only one MusicManager
        }
        else
        {
            // Set this object as the persistent one across scenes
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent the music object from being destroyed
        }
    }

    void Start()
    {
        musicToggle.isOn = audioSource.isPlaying;
        musicToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Play the music
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Stop the music
            }
        }
    }
}
