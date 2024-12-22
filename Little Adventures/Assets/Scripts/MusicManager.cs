using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Toggle musicToggle;
    public AudioSource audioSource;
    public static MusicManager instance;

    void Awake()
    {
        // if there is already another instance of MusicManager, destroy this one
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // set this object as the persistent one across scenes
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // add listener on the current music toggle
    void Start()
    {
        musicToggle.isOn = audioSource.isPlaying;
        musicToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void SetToggleReference(Toggle newToggle)
    {
        if (musicToggle != null)
        {
            musicToggle.onValueChanged.RemoveListener(OnToggleChanged); // remove listener from the old toggle
        }

        musicToggle = newToggle; // assign the new toggle from current menu

        if (musicToggle != null)
        {
            musicToggle.isOn = audioSource.isPlaying; // sync the toggle state with audio
            musicToggle.onValueChanged.AddListener(OnToggleChanged); // add listener to the new toggle
        }
    }

    // manage music on/off
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
