using UnityEngine;
using UnityEngine.UI;

public class TestToggle : MonoBehaviour
{
    public Toggle musicToggle;
    public AudioSource audioSource;

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
