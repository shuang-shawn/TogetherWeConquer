﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;            // The UI Panel object
    public TextMeshProUGUI instructionsText;  // The main instructions text
    public TextMeshProUGUI keyStatusText;     // The text showing key status

    private bool active = false;
    private bool player1 = true;
    private bool pressedW, pressedA, pressedS, pressedD, pressedSpace;
    private bool pressedUp, pressedLeft, pressedDown, pressedRight, pressedM;

    void Update()
    {
        if (active)
        {
            if (player1)
            {
                if (Input.GetKeyDown(KeyCode.W)) pressedW = true;
                if (Input.GetKeyDown(KeyCode.A)) pressedA = true;
                if (Input.GetKeyDown(KeyCode.S)) pressedS = true;
                if (Input.GetKeyDown(KeyCode.D)) pressedD = true;
                if (Input.GetKeyDown(KeyCode.Space)) pressedSpace = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) pressedUp = true;
                if (Input.GetKeyDown(KeyCode.LeftArrow)) pressedLeft = true;
                if (Input.GetKeyDown(KeyCode.DownArrow)) pressedDown = true;
                if (Input.GetKeyDown(KeyCode.RightArrow)) pressedRight = true;
                if (Input.GetKeyDown(KeyCode.M)) pressedM = true;
            }

            // Update the key status text
            UpdateKeyStatus();

            // Check if all keys are pressed
            if (pressedW && pressedA && pressedS && pressedD && pressedSpace)
            {
                player1 = false;
                instructionsText.text = "Player 2, press\n↑, ←, ↓, and →\nto move as well as\nM\nto dash.";
                if (pressedUp && pressedLeft && pressedDown && pressedRight && pressedM)
                {
                    CompleteTutorial();
                }
            }
        }
    }

    public void Play()
    {
        tutorialPanel.SetActive(true);
        active = true;
        instructionsText.text = "Player 1, press\nW, A, S, and D\nto move as well as\nSpace\nto dash.";
        UpdateKeyStatus();
    }
    void UpdateKeyStatus()
    {
        // Update the text to show which keys are pressed
        if (player1)
        {
            keyStatusText.text = $"Keys Pressed: " +
                                 $"{(pressedW ? "<color=green>W</color> " : "W ")}" +
                                 $"{(pressedA ? "<color=green>A</color> " : "A ")}" +
                                 $"{(pressedS ? "<color=green>S</color> " : "S ")}" +
                                 $"{(pressedD ? "<color=green>D</color> " : "D ")}" +
                                 $"{(pressedSpace ? "<color=green>Space</color> " : "Space ")}";
        }
        else
        {
            keyStatusText.text = $"Keys Pressed: " +
                                 $"{(pressedUp ? "<color=green>↑</color> " : "↑ ")}" +
                                 $"{(pressedLeft ? "<color=green>←</color> " : "← ")}" +
                                 $"{(pressedDown ? "<color=green>↓</color> " : "↓ ")}" +
                                 $"{(pressedRight ? "<color=green>→</color> " : "→ ")}" +
                                 $"{(pressedM ? "<color=green>M</color> " : "M ")}";
        }
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
