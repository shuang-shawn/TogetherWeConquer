using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float healthBarDisplayTime = 1.0f;

    public Slider healthSlider; // Assign your health slider in the inspector
    public ComboUIFX shake;

    void Start()
    {
        if (gameObject.tag == "Hidden")
        {
            gameObject.SetActive(false);
        }

        shake = GameObject.FindGameObjectWithTag("FXManager").GetComponent<ComboUIFX>();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth; // Update slider value

        if (gameObject.tag == "Hidden")
        {
            gameObject.SetActive(true); // Show the health bar

            StartCoroutine(ShowHealthBar());
        }
    }

    private IEnumerator ShowHealthBar()
    {
        shake.TriggerShake(healthSlider.GetComponent<Image>());

        yield return new WaitForSeconds(healthBarDisplayTime); // Wait for the specified duration

        gameObject.SetActive(false); // Hide the health bar after the duration
    }

}

