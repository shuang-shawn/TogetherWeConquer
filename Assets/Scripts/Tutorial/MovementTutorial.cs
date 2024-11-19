﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;            // The UI Panel object
    public TextMeshProUGUI instructionsTextP1;  // The main instructions text
    public TextMeshProUGUI keyStatusTextP1;     // The text showing key status
    public TextMeshProUGUI instructionsTextP2;  // The main instructions text
    public TextMeshProUGUI keyStatusTextP2;     // The text showing key status

    private bool active = false;
    private bool pressedW, pressedA, pressedS, pressedD;
    private bool pressedUp, pressedLeft, pressedDown, pressedRight;

    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.W)) pressedW = true;
            if (Input.GetKeyDown(KeyCode.A)) pressedA = true;
            if (Input.GetKeyDown(KeyCode.S)) pressedS = true;
            if (Input.GetKeyDown(KeyCode.D)) pressedD = true;
            if (Input.GetKeyDown(KeyCode.UpArrow)) pressedUp = true;
            if (Input.GetKeyDown(KeyCode.LeftArrow)) pressedLeft = true;
            if (Input.GetKeyDown(KeyCode.DownArrow)) pressedDown = true;
            if (Input.GetKeyDown(KeyCode.RightArrow)) pressedRight = true;

            // Update the key status text
            UpdateKeyStatus();

            // Check if all keys are pressed
            if (pressedW && pressedA && pressedS && pressedD && pressedUp && pressedLeft && pressedDown && pressedRight)
            {
                CompleteTutorial();
            }
        }
    }

    public void Play()
    {
        tutorialPanel.SetActive(true);
        active = true;
        instructionsTextP1.text = "Press W, A, S, and D to move.";
        instructionsTextP2.text = "Press ↑, ←, ↓, and → to move.";
        UpdateKeyStatus();
    }
    void UpdateKeyStatus()
    {
        // Update the text to show which keys are pressed
        keyStatusTextP1.text = $"Keys Pressed: " +
                             $"{(pressedW ? "<color=green>W</color> " : "W ")}" +
                             $"{(pressedA ? "<color=green>A</color> " : "A ")}" +
                             $"{(pressedS ? "<color=green>S</color> " : "S ")}" +
                             $"{(pressedD ? "<color=green>D</color> " : "D ")}";


        keyStatusTextP2.text = $"Keys Pressed: " +
                             $"{(pressedUp ? "<color=green>↑</color> " : "↑ ")}" +
                             $"{(pressedLeft ? "<color=green>←</color> " : "← ")}" +
                             $"{(pressedDown ? "<color=green>↓</color> " : "↓ ")}" +
                             $"{(pressedRight ? "<color=green>→</color> " : "→ ")}";
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}