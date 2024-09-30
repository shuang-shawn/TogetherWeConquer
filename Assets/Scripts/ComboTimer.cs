using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComboTimer : MonoBehaviour
{
    public Slider timer;
    public float maxTime = 5.0f;

    private float timeLeft;
    private bool isRunning = false;

    private void Start()
    {
        //Initialize the timer to the maximum value
        timeLeft = maxTime;

        //Set slider value to full
        timer.value = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) 
            || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRunning = true;
        }

        if(isRunning)
        {
            //Decrement timer
            timeLeft -= Time.deltaTime;

            //Update slider
            timer.value = timeLeft / maxTime;

            if(timer.value <= 0.0f)
            {
                isRunning = false;

                resetTimer();
            }
        }
    }

    

    public void resetTimer()
    {
        timeLeft = maxTime;
        timer.value = 1.0f;
    }
}
