using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpTutorial : MonoBehaviour
{
    public GameStateManager gameStateManager;   // The GameStateManager
    public GameObject tutorialPanel;            // The UI Panel object
    public TextMeshProUGUI instructionsText;    // The main instructions text

    private bool active = false;

    private void Update()
    {
        if (!gameStateManager.isPlayer1Level)
        {
            instructionsText.text = "Player 2, select a combo and then press Accept.";
        }
        if (!gameStateManager.levelUp)
        {
            CompleteTutorial();
        }
    }

    public void Play()
    {
        tutorialPanel.SetActive(true);
        active = true;
        if (!gameStateManager.duoLevel)
        {
            instructionsText.text = "Player 1, select a combo and then press Accept.";
        }
        else
        {
            instructionsText.text = "Select a duo combo and then press Accept.";
        }
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
