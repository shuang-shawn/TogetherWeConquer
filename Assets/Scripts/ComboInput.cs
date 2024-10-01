using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboInput : MonoBehaviour
{
    // Input System
    public InputActionAsset playerControls;
    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;

    ComboData comboData;
    private void Awake()
    {
        var actionMap = playerControls.FindActionMap("Combo");
        up = actionMap.FindAction("Up");
        down = actionMap.FindAction("Down");
        left = actionMap.FindAction("Left");
        right = actionMap.FindAction("Right");
        comboData = GetComponent<ComboData>();
    }

    private void OnEnable()
    {
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();

        // Assigning callback to each input key
        up.performed += CheckInitialInput;
        down.performed += CheckInitialInput;
        left.performed += CheckInitialInput;
        right.performed += CheckInitialInput;

    }
    private void OnDisable()
    {
        up.Disable();
        down.Disable();
        left.Disable();
        right.Disable();

        // Removing callback from each input key
        up.performed -= CheckInitialInput;
        down.performed -= CheckInitialInput;
        left.performed -= CheckInitialInput;
        right.performed -= CheckInitialInput;
    }

    // Checks if first 2 inputs match any combos that start off with the 2 inputs
    void CheckInitialInput(InputAction.CallbackContext context)
    {
            KeyCode inputKey = GetKeyFromContext(context);
            if (comboData.firstInput == KeyCode.None)
            {
                comboData.firstInput = inputKey;
            }
            else if (comboData.secondInput == KeyCode.None)
            {
                comboData.secondInput = inputKey;
                CheckComboList();
            }
    }

    public void CheckComboList()
    {
        foreach (var combo in comboData.comboList)
        {
            if (combo[0] == comboData.firstInput && combo[1] == comboData.secondInput)
            {
                Debug.Log("Matching Combo" + string.Join(", ", combo));
                StartCombo(combo);
                break;
            }
        }
        comboData.firstInput = KeyCode.None;
        comboData.secondInput = KeyCode.None;
        Debug.Log("No matching combo");
    }

    void StartCombo(List<KeyCode> combo)
    {
        comboData.currentCombo = combo;
        comboData.initiatedCombo = true;
        comboData.currentSequenceIndex = 2;
        comboData.mistakeOrder.AddRange(new[] { "Correct", "Correct" });

        up.performed -= CheckInitialInput;
        down.performed -= CheckInitialInput;
        left.performed -= CheckInitialInput;
        right.performed -= CheckInitialInput;

        up.performed += ComboSequence;
        down.performed += ComboSequence;
        left.performed += ComboSequence;
        right.performed += ComboSequence;
    }

    void RestartCombo()
    {
        comboData.ResetData();
        up.performed -= ComboSequence;
        down.performed -= ComboSequence;
        left.performed -= ComboSequence;
        right.performed -= ComboSequence;

        up.performed += CheckInitialInput;
        down.performed += CheckInitialInput;
        left.performed += CheckInitialInput;
        right.performed += CheckInitialInput;
    }
    private void ComboSequence(InputAction.CallbackContext context)
    {
            // Determine the current input
            KeyCode inputKey = GetKeyFromContext(context);
            Debug.Log(inputKey);
            comboData.lastKeyPressed = inputKey;
            if (inputKey == comboData.currentCombo[comboData.currentSequenceIndex])
            {
            comboData.mistakeOrder.Add("Correct");
                Debug.Log("Correct Input");
            }
            else
            {
            Debug.Log("Incorrect Input");
                comboData.mistakeCount++;
                comboData.mistakeKeysPressed.Add(inputKey);
                comboData.mistakeOrder.Add("Incorrect");
        }
            comboData.currentSequenceIndex++;
        if (comboData.currentSequenceIndex >= comboData.currentCombo.Count)
        {
            Debug.Log("Combo Completed");
            RestartCombo();
        }
    }

    // Helper method to translate the InputAction context to KeyCode
    private KeyCode GetKeyFromContext(InputAction.CallbackContext context)
    {
        if (context.action == up) return KeyCode.UpArrow;
        if (context.action == down) return KeyCode.DownArrow;
        if (context.action == left) return KeyCode.LeftArrow;
        if (context.action == right) return KeyCode.RightArrow;

        return KeyCode.None;
    }
}
