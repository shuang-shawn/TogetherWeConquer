using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFadeIn : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button codexButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI title;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInButton(startButton, 0.5f));
        StartCoroutine(FadeInButton(tutorialButton, 0.5f));
        StartCoroutine(FadeInButton(settingsButton, 1f));
        StartCoroutine(FadeInButton(codexButton, 1f));
        StartCoroutine(FadeInButton(quitButton, 1.5f));

        StartCoroutine(FadeInTitle(2f));
    }

    private IEnumerator FadeInButton(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha <= 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeInTitle(float delay)
    {
        yield return new WaitForSeconds(delay);

        Color originalColor = title.color;
        originalColor.a = 0;
        title.color = originalColor;

        float fadeDuration = 1f;  // Adjust this duration as needed
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);  // Smooth transition
            title.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure the title is fully visible
        title.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

    }
}
