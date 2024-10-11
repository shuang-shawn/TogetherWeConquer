using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ComboUIFX : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Duration of shake
    public float shakeMagnitude = 0.1f; // Magnitude of shake

    // Public method to trigger shake
    public void TriggerShake(Image target)
    {
        if(target != null)
            StartCoroutine(Shake(target));
    }

    private IEnumerator Shake(Image target)
    {
        RectTransform rectTransform = target.GetComponent<RectTransform>(); // Get RectTransform from Image
        Vector3 originalPosition = rectTransform.localPosition; // Store original position
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            rectTransform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset position
        rectTransform.localPosition = originalPosition;
    }
}

