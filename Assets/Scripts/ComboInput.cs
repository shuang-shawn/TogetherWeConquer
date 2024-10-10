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
    private InputAction cancel;

    private InputActionMap actionMap;

    ComboData comboData;
    ComboUI comboUI;
    public GameObject timer;
   
    private void Awake()
    {
        // Assigns different Action Map depending on player
        if (transform.parent.gameObject.name.Equals("Player1"))
        {
            actionMap = playerControls.FindActionMap("ComboP1");
        } 
        else if (transform.parent.gameObject.name.Equals("Player2"))
        {
            actionMap = playerControls.FindActionMap("ComboP2");
        }
    
        up = actionMap.FindAction("Up");
        down = actionMap.FindAction("Down");
        left = actionMap.FindAction("Left");
        right = actionMap.FindAction("Right");
        cancel = actionMap.FindAction("Cancel");
        comboData = GetComponent<ComboData>();
        comboUI = GetComponent<ComboUI>();
    }

    private void OnEnable()
    {
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();
        cancel.Enable();

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
        cancel.Disable();

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
        bool foundMatch = false;
        foreach (var combo in comboData.comboList)
        {
            if (combo[0] == comboData.firstInput && combo[1] == comboData.secondInput)
            {
                Debug.Log("Matching Combo" + string.Join(", ", combo));
                StartCombo(combo);
                foundMatch = true;
                break;
            }
        }
        if (!foundMatch)
        {
            comboData.firstInput = KeyCode.None;
            comboData.secondInput = KeyCode.None;
            Debug.Log("No matching combo");
        }
    }

    void StartCombo(List<KeyCode> combo)
    {
        comboData.currentCombo = combo;
        comboData.initiatedCombo = true;
        comboData.currentSequenceIndex = 2;
        comboData.mistakeOrder.AddRange(new[] { "Correct", "Correct" });

        comboUI.InitializeUI(combo, 2);

        up.performed -= CheckInitialInput;
        down.performed -= CheckInitialInput;
        left.performed -= CheckInitialInput;
        right.performed -= CheckInitialInput;

        up.performed += ComboSequence;
        down.performed += ComboSequence;
        left.performed += ComboSequence;
        right.performed += ComboSequence;
        cancel.performed += CancelCombo;

        StartCoroutine(StartCountdown());
    }

    void RestartCombo()
    {
        comboData.ResetData();
        comboUI.ResetUI();
        StopAllCoroutines();
        timer.GetComponent<ComboTimer>().ResetTimer();
        up.performed -= ComboSequence;
        down.performed -= ComboSequence;
        left.performed -= ComboSequence;
        right.performed -= ComboSequence;
        cancel.performed -= CancelCombo;

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
            comboUI.UpdateArrow(comboData.currentSequenceIndex, true);
        }
        else
        {
            Debug.Log("Incorrect Input");
            comboData.mistakeCount++;
            comboData.mistakeKeysPressed.Add(inputKey);
            comboData.mistakeOrder.Add("Incorrect");
            comboUI.UpdateArrow(comboData.currentSequenceIndex, false);
        }
        comboData.currentSequenceIndex++;
        if (comboData.currentSequenceIndex >= comboData.currentCombo.Count)
        {
            StartCoroutine(Scoring());
        }
    }

    private IEnumerator Scoring()
    {
        comboUI.ShowScore(comboData.mistakeCount, comboData.currentCombo.Count);
        Debug.Log("Combo Completed");

        yield return new WaitForSeconds(1.0f);

        RestartCombo();
    }

    private void CancelCombo(InputAction.CallbackContext context)
    {
        Debug.Log("Combo Canceled");
        RestartCombo();
    }

    private void ComboTimerExpired()
    {
        Debug.Log("Ran out of time");
        RestartCombo();
    }
    public IEnumerator StartCountdown(float countdownValue = 7) // 7 seconds
    {
        comboData.timerVal = countdownValue;
        timer.GetComponent<ComboTimer>().InitializeTimer(countdownValue);
        while(comboData.timerVal > 0)
        {
            yield return new WaitForSeconds(1.0f);
            comboData.timerVal--;
        }
        // If out of time
        ComboTimerExpired();
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
