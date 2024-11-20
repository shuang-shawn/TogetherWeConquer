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
    public TextMeshProUGUI instructionsTextP1;  // The main instructions text
    public TextMeshProUGUI keyStatusTextP1;     // The text showing key status
    public TextMeshProUGUI instructionsTextP2;  // The main instructions text
    public TextMeshProUGUI keyStatusTextP2;     // The text showing key status

    private bool active = false;
    private bool pressedE = false;
    private bool pressedO = false;

    private bool finishedComboP1 = false;
    private bool finishedComboP2 = false;
    private bool windowOpen = false;

    void Update()
    {
        if (active)
        {
            if (windowOpen)
            {
                instructionsTextP1.text = "Press Y(↑), G(←), H(↓), and J(→) to activate half of a duo combo.";
                instructionsTextP2.text = "Press P(↑), L(←), ;(↓), and '(→) to activate half of a duo combo.";

                if (comboDataP1.finishedCombo)
                {
                    finishedComboP1 = true;
                }
                if (comboDataP2.finishedCombo)
                {
                    finishedComboP2 = true;
                }

                // Check if all keys are pressed
                if (finishedComboP1 && finishedComboP2)
                {
                    CompleteTutorial();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E)) pressedE = true;
                if (Input.GetKeyDown(KeyCode.O)) pressedO = true;

                if (pressedE && pressedO)
                {
                    windowOpen = true;
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

        instructionsTextP1.text = "Press E to open duo combo window.";
        instructionsTextP2.text = "Press O to open duo combo window.";

        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (windowOpen)
        {
            // Update the text to show which keys are pressed
            keyStatusTextP1.text = $"Player 1 Combo " + $"{(finishedComboP1 ? "<color=green>Complete</color>" : "Complete")}";

            keyStatusTextP2.text = $"Player 2 Combo " + $"{(finishedComboP2 ? "<color=green>Complete</color>" : "Complete")}";
        }
        else
        {
            // Update the text to show which keys are pressed
            keyStatusTextP1.text = $"Window Open: Press " + $"{(finishedComboP1 ? "<color=green>E</color>" : "E")}";

            keyStatusTextP2.text = $"Window Open: Press " + $"{(finishedComboP2 ? "<color=green>O</color>" : "O")}";
        }
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
