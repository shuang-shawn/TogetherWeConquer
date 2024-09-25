using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboLogic : MonoBehaviour
{
    KeyCode[] sequence = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    public int currentSequenceIndex = 0;
    public float timeLimit = 10f; // Time allowed between key presses
    public float timer = 0f;
    public int mistake = 0;

    private void Start()
    {
      PrintNextSequence();
    }
    void Update()
    {
        timer += Time.deltaTime;
       
        //if (timer > timeLimit)
        //{
        //    // If too much time has passed, reset the sequence
        //    Debug.Log("Not done");
        //    currentSequenceIndex = 0;
        //    mistake = 0;
        //    timer = 0f;
        //}
        RestartCombo();
        CheckComboInput();
    }

    // During the combo, check if user input matches current combo sequence.
    void CheckComboInput()
    {
        if (Input.GetKeyDown(sequence[currentSequenceIndex]))
        {
            currentSequenceIndex++;
            Debug.Log("Correct Input");
            PrintNextSequence();
        }
        else if (Input.anyKeyDown && !Input.GetKeyDown(sequence[currentSequenceIndex]))
        {
            // Check if the key pressed is not a mouse button
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
            {
                // If the wrong key is pressed, reset the sequence
                currentSequenceIndex++;
                Debug.Log("Wrong Input");
                mistake++;
                PrintNextSequence();
            }
        }
    }

    // Once combo sequence completed then restart combo
    void RestartCombo()
    {
        if (currentSequenceIndex >= sequence.Length)
        {
            Debug.Log("Combo completed!");
            Debug.Log("Mistakes: " + mistake);
            currentSequenceIndex = 0; // Reset for the next combo
            mistake = 0;
            Debug.Log("RESTART COMBO");
            PrintNextSequence();
        }
    }
    void PrintNextSequence()
    {
       try
        {
            Debug.Log(currentSequenceIndex + " Next Sequence: " + sequence[currentSequenceIndex]);
        }
        catch
        {

        }
    }
}