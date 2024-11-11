using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
public class anxiety_Fade : MonoBehaviour
{
    public Image fadeImage;  // Reference to the Image component of the panel
    public float fadeDuration = 2.0f;  // Duration of the fade effect
    public float maxFadeAlpha = 0.5f;
    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);  // Make sure the image is transparent at start
    }

    public void TriggerFadeOut()
    {
        Debug.Log("here");
        StartCoroutine(FadeOut());
    }

    public void TriggerFadeIn()
    {
        Debug.Log("here");
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration) * maxFadeAlpha;  // Calculate alpha over time
            fadeImage.color = new Color(0, 0, 0, alpha);  // Update image color
            yield return null;
        }

        fadeImage.color = new Color (0,0,0, maxFadeAlpha);

        // Optional: After fade-out, you can add any logic like restarting the game or loading a new scene.
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration); // Reverse alpha for fade-in
            fadeImage.color = new Color(0, 0, 0, alpha); // Update image color
            yield return null;
        }
    }
}
