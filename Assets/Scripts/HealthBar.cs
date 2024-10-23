using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float healthBarDisplayTime = 1.0f;

    public Slider healthSlider; // Assign your health slider in the inspector

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth; // Update slider value

        StartCoroutine(ShowHealthBar());
    }

    private IEnumerator ShowHealthBar()
    {
        gameObject.SetActive(true); // Show the health bar

        yield return new WaitForSeconds(healthBarDisplayTime); // Wait for the specified duration

        gameObject.SetActive(false); // Hide the health bar after the duration
    }

}

