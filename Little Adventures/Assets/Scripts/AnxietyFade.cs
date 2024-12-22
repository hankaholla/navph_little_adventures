using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
public class AnxietyFade : MonoBehaviour
{
    public Image fadeImage;  // Reference to the Image component of the panel
    public float fadeDuration = 2.0f;  // Duration of the fade effect

    void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);  // Make sure the image is transparent at start
    }

    public void TriggerFadeOut(float startAlpha, float maxFadeAlpha)
    {
        StartCoroutine(FadeOut(startAlpha, maxFadeAlpha));
    }

    public void TriggerFadeIn(float startAlpha, float minAlpha)
    {
        StartCoroutine(FadeIn(startAlpha, minAlpha));
    }

   private IEnumerator FadeOut(float startAlpha, float maxFadeAlpha)
{
    float elapsedTime = 0f;
    float alphaRange = maxFadeAlpha - startAlpha;  // Calculate the range between start and max alpha

    while (elapsedTime < fadeDuration)
    {
        elapsedTime += Time.deltaTime;
        float alpha = startAlpha + Mathf.Clamp01(elapsedTime / fadeDuration) * alphaRange;  // Calculate alpha over time
        fadeImage.color = new Color(0, 0, 0, alpha);  // Update image color
        yield return null;
    }

    // Ensure the final alpha is set to the maximum specified value
    fadeImage.color = new Color(0, 0, 0, maxFadeAlpha);
}

    private IEnumerator FadeIn(float startAlpha, float minAlpha)
    {
        float elapsedTime = 0f;
        float alphaRange = startAlpha - minAlpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = startAlpha - Mathf.Clamp01(elapsedTime / fadeDuration) * alphaRange;
            fadeImage.color = new Color(0, 0, 0, alpha); // Update image color
            yield return null;
        }

        // Ensure the final alpha is set to the minimum specified value
        fadeImage.color = new Color(0, 0, 0, minAlpha);
    }
}
