using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;            // The UI Panel object
    public GameObject player1;                  // Player 1 
    public GameObject player2;                  // Player 2
    private ComboData comboDataP1;              // Player 1 combo data
    private ComboData comboDataP2;              // Player 2 combo data
    public TextMeshProUGUI instructionsText;  // The main instructions text
    public TextMeshProUGUI keyStatusText;     // The text showing key status

    private bool active = false;
    private bool p2Combo = false;
    private bool finishedComboP1 = false;
    private bool finishedComboP2 = false;

    void Update()
    {
        if (active)
        {
            if(comboDataP1.finishedCombo)
            {
                finishedComboP1 = true;
            }
            if (p2Combo && comboDataP2.finishedCombo)
            {
                finishedComboP2 = true;
            }

            // Update the key status text
            UpdateStatus();

            // Check if all keys are pressed
            if (finishedComboP1)
            {
                p2Combo = true;
                instructionsText.text = "Player 2, press\nP(↑), L(←), ;(↓), and '(→)\nto activate a solo combo.\nPress Q to cancel.";
                if (finishedComboP2)
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

        comboDataP1 = player1.GetComponentInChildren<ComboData>();
        comboDataP2 = player2.GetComponentInChildren<ComboData>();

        instructionsText.text = "Player 1, press\nY(↑), G(←), H(↓), and J(→)\nto activate a solo combo.\nPress Q to cancel.";
        UpdateStatus();
    }

    void UpdateStatus()
    {
        // Update the text to show which keys are pressed
        if (!p2Combo)
        {
            keyStatusText.text = $"Player 1 Combo " + $"{(finishedComboP1 ? "<color=green>Complete</color>" : "Complete")}";
        }
        else
        {
            keyStatusText.text = $"Player 2 Combo " + $"{(finishedComboP2 ? "<color=green>Complete</color>" : "Complete")}";
        }
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
