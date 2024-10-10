using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComboTimer : MonoBehaviour
{
    public Slider timer;
    public float maxTime;

    private float timeLeft;
    private bool isRunning = false;

    private void Start()
    {
        timer.value = timeLeft / maxTime;
    }

    public void InitializeTimer(float maxTimeLimit, float remaining)
    {
        maxTime = maxTimeLimit;
        timeLeft = remaining;
        gameObject.SetActive(true);
        isRunning = true;
    }

    private void Update()
    {
        if (isRunning)
        {
            //Decrement timer
            timeLeft -= Time.deltaTime;

            //Update slider
            timer.value = timeLeft / maxTime;

            if (timer.value <= 0.0f)
            {
                isRunning = false;

                ResetTimer();
            }
        }
    }

    public void ResetTimer()
    {
        gameObject.SetActive(false);
        isRunning = false;
    }
}
