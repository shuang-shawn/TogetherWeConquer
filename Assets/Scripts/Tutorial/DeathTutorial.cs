using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;            // The UI Panel object
    public GameObject player1;                  // Player 1 
    private ComboData comboDataP1;              // Player 1 combo data
    public TextMeshProUGUI instructionsTextP1;  // The main instructions text
    public TextMeshProUGUI keyStatusTextP1;     // The text showing key status

    private bool active = false;
    private bool pressedKey = false;
    private bool reviving = false;

    void Update()
    {
        if (active)
        {
            if(reviving)
            {
                if(comboDataP1.revived)
                {
                    CompleteTutorial();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E)) pressedKey = true;

                // Check if all keys are pressed
                if (pressedKey)
                {
                    reviving = true;
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

        instructionsTextP1.text = "Dead player press combo key to start revival.";
        UpdateStatus();
    }
    void UpdateStatus()
    {
        if (reviving)
        {
            keyStatusTextP1.text = "Perform 5 combos to revive.";
        }
        else
        {
            // Update the text to show which keys are pressed
            keyStatusTextP1.text = $"Key Pressed: " + $"{(pressedKey ? "<color=green>E</color> " : "E ")}";
        }
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
