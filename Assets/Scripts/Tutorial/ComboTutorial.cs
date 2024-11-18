using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;          // The UI Panel object
    public TextMeshProUGUI instructionsTextP1;  // The main instructions text
    public TextMeshProUGUI keyStatusTextP1;     // The text showing key status
    public TextMeshProUGUI instructionsTextP2;  // The main instructions text
    public TextMeshProUGUI keyStatusTextP2;     // The text showing key status

    private bool active = false;
    private bool pressedY, pressedG, pressedH, pressedJ;
    private bool pressedP, pressedL, pressedSemicolon, pressedQuote;

    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Y)) pressedY = true;
            if (Input.GetKeyDown(KeyCode.G)) pressedG = true;
            if (Input.GetKeyDown(KeyCode.H)) pressedH = true;
            if (Input.GetKeyDown(KeyCode.J)) pressedJ = true;
            if (Input.GetKeyDown(KeyCode.P)) pressedP = true;
            if (Input.GetKeyDown(KeyCode.L)) pressedL = true;
            if (Input.GetKeyDown(KeyCode.Semicolon)) pressedSemicolon = true;
            if (Input.GetKeyDown(KeyCode.Quote)) pressedQuote = true;

            // Update the key status text
            UpdateKeyStatus();

            // Check if all keys are pressed
            if (pressedY && pressedG && pressedH && pressedJ && pressedP && pressedL && pressedSemicolon && pressedQuote)
            {
                CompleteTutorial();
            }
        }
    }

    public void Play()
    {
        tutorialPanel.SetActive(true);
        active = true;
        instructionsTextP1.text = "Press Y, G, H, and J to activate solo combo.";
        instructionsTextP2.text = "Press P, L, ;, and ' to activate solo combo.";
        UpdateKeyStatus();
    }
    void UpdateKeyStatus()
    {
        // Update the text to show which keys are pressed
        keyStatusTextP1.text = $"Keys Pressed: " +
                             $"{(pressedY ? "<color=green>Y</color> " : "Y ")}" +
                             $"{(pressedG ? "<color=green>G</color> " : "G ")}" +
                             $"{(pressedH ? "<color=green>H</color> " : "H ")}" +
                             $"{(pressedJ ? "<color=green>J</color> " : "J ")}";


        keyStatusTextP2.text = $"Keys Pressed: " +
                             $"{(pressedP ? "<color=green>P</color> " : "P ")}" +
                             $"{(pressedL ? "<color=green>L</color> " : "L ")}" +
                             $"{(pressedSemicolon ? "<color=green>;</color> " : "; ")}" +
                             $"{(pressedQuote ? "<color=green>'</color> " : "' ")}";
    }

    void CompleteTutorial()
    {
        // Deactivate the tutorial UI and move to the next step
        tutorialPanel.SetActive(false);
        Debug.Log("Tutorial Complete!");
    }
}
