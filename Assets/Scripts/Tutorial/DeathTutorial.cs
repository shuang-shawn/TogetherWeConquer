using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;            // The UI Panel object
    public GameObject player1;                  // Player 1 
    public GameObject player2;                  // Player 2
    private ComboData comboDataP1;              // Player 1 combo data
    private ComboData comboDataP2;              // Player 2 combo data
    private PlayerManager p1Manager;             // Player 1 player manager
    private PlayerManager p2Manager;             // Player 2 player manager
    public TextMeshProUGUI instructionsText;  // The main instructions text
    public TextMeshProUGUI keyStatusText;     // The text showing key status

    private bool p1dead = true;
    private bool active = false;
    private bool pressedKey = false;
    private bool reviving = false;

    void Update()
    {
        if (active)
        {
            if (p1dead)
            {
                if (reviving)
                {
                    if(comboDataP1.revived)
                    {
                        p2Manager.TakeDamage(100);
                        instructionsText.text = "Player 2, press\nO\nto start revival combo.";
                        pressedKey = false;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E)) pressedKey = true;

                    if (pressedKey)
                    {
                        reviving = true;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.O)) pressedKey = true;

                if (pressedKey)
                {
                    reviving = true;
                }
            }

            if (comboDataP2.revived)
            {
                CompleteTutorial();
            }

            UpdateStatus();
        }
    }

    public void Play()
    {
        tutorialPanel.SetActive(true);
        active = true;

        comboDataP1 = player1.GetComponentInChildren<ComboData>();
        comboDataP2 = player2.GetComponentInChildren<ComboData>();

        p1Manager = player1.GetComponent<PlayerManager>();
        p2Manager = player2.GetComponent<PlayerManager>();

        p1Manager.TakeDamage(100);

        instructionsText.text = "Player 1, press\nE\nto start revival combo.";
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (reviving)
        {
            keyStatusText.text = "Perform 5 combos to revive.";
        }
        else if (p1dead)
        {
            keyStatusText.text = $"Key Pressed: " + $"{(pressedKey ? "<color=green>E</color> " : "E ")}";
        }
        else
        {
            keyStatusText.text = $"Key Pressed: " + $"{(pressedKey ? "<color=green>O</color> " : "O ")}";
        }
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
