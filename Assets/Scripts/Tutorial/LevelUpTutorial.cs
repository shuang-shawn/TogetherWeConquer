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
        if (!gameStateManager.levelUp)
        {
            CompleteTutorial();
        }
    }

    public void Play()
    {
        tutorialPanel.SetActive(true);
        active = true;
        instructionsText.text = "Select a combo and then press Done.";
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
