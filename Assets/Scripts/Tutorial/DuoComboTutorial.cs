using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuoComboTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;            // The UI Panel object
    public GameObject player1;                  // Player 1 
    public GameObject player2;                  // Player 2
    private ComboData comboDataP1;              // Player 1 combo data
    private ComboData comboDataP2;              // Player 2 combo data
    public TextMeshProUGUI instructionsText;  // The main instructions text
    public TextMeshProUGUI keyStatusText;     // The text showing key status

    private bool active = false;
    private bool player1Start = true;
    private bool pressedE = false;
    private bool pressedO = false;

    private bool secondHalf = false;
    private bool finishedComboP1 = false;
    private bool finishedComboP2 = false;
    private bool windowOpen = false;

    void Update()
    {
        if (active)
        {
            if (windowOpen)
            {
                if (player1Start)
                {
                    instructionsText.text = "Player 1, press\nY(↑), G(←), H(↓), and J(→)\nto activate the starting half of a duo combo.\nPress Q to cancel.";

                    if(comboDataP1.finishedCombo)
                    {
                        secondHalf = true;
                    }

                    if (secondHalf)
                    {
                        instructionsText.text = "Player 2, press\nP(↑), L(←), ;(↓), and '(→)\nto finish the second half of the duo combo.\nPress Q to cancel.";
                    }

                    if (comboDataP2.finishedCombo)
                    {
                        finishedComboP1 = true;
                        instructionsText.text = "Press O to open duo combo window.";
                        windowOpen = false;
                        secondHalf = false;
                        player1Start = false;
                    }
                }
                else
                {
                    instructionsText.text = "Player 2, press\nP(↑), L(←), ;(↓), and '(→)\nto activate the starting half of a duo combo.\nPress Q to cancel.";

                    if (comboDataP2.finishedCombo)
                    {
                        secondHalf = true;
                    }

                    if (secondHalf)
                    {
                        instructionsText.text = "Player 1, press\nY(↑), G(←), H(↓), and J(→)\nto finish the second half of the duo combo.\nPress Q to cancel.";
                    }

                    if (comboDataP1.finishedCombo)
                    {
                        secondHalf = false;
                        finishedComboP2 = true;
                    }
                }

                // Check if all keys are pressed
                if (finishedComboP1)
                {
                    if (finishedComboP2)
                    { 
                        CompleteTutorial();
                    }
                }
            }
            else
            {
                if (player1Start)
                {
                    if (Input.GetKeyDown(KeyCode.E)) pressedE = true;

                    if (pressedE)
                    {
                        windowOpen = true;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.O)) pressedO = true;

                    if (pressedO)
                    {
                        windowOpen = true;
                    }
                }
            }
            // Update the key status text
            UpdateStatus();
        }
    }

    public void Play()
    {
        tutorialPanel.SetActive(true);
        active = true;

        comboDataP1 = player1.GetComponentInChildren<ComboData>();
        comboDataP2 = player2.GetComponentInChildren<ComboData>();

        instructionsText.text = "Player 1, press E to open duo combo window.";

        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (windowOpen)
        {
            // Update the text to show which keys are pressed
            if (player1Start)
            {
                keyStatusText.text = $"Player 1 Combo " + $"{(finishedComboP1 ? "<color=green>Complete</color>" : "Complete")}";
            }
            else
            {
                keyStatusText.text = $"Player 2 Combo " + $"{(finishedComboP2 ? "<color=green>Complete</color>" : "Complete")}";
            }
        }
        else
        {
            // Update the text to show which keys are pressed
            if (player1Start)
            {
                keyStatusText.text = $"Window Open: Press " + $"{(finishedComboP1 ? "<color=green>E</color>" : "E")}";
            }
            else
            {
                keyStatusText.text = $"Window Open: Press " + $"{(finishedComboP2 ? "<color=green>O</color>" : "O")}";
            }
        }
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
