using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Toggle musicToggle;
    public AudioSource audioSource;
    public static MusicManager instance;  // Singleton instance

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

    public void SetToggleReference(Toggle newToggle)
    {
        if (musicToggle != null)
        {
            musicToggle.onValueChanged.RemoveListener(OnToggleChanged); // Remove listener from the old toggle
        }

        musicToggle = newToggle; // Assign the new toggle

        if (musicToggle != null)
        {
            musicToggle.isOn = audioSource.isPlaying; // Sync the toggle state with audio playback
            musicToggle.onValueChanged.AddListener(OnToggleChanged); // Add listener to the new toggle
        }
    }


    public void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
