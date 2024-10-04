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
        //Initialize the timer to the maximum value
        timeLeft = maxTime;

        //Set slider value to full
        timer.value = 1.0f;

    }

    public void InitializeTimer(float maxTimeLimit)
    {
        maxTime = maxTimeLimit;
        timeLeft = maxTimeLimit;
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
        timeLeft = maxTime;
        timer.value = 1.0f;
        gameObject.SetActive(false);
        isRunning = false;
    }
}
