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
    private InputAction duoToggle;

    private InputActionMap actionMap;

    ComboData comboData;
    ComboUI comboUI;
    public GameObject timer;

    //public GameObject otherPlayer;
    public DuoComboManager duoComboManager;
    private void Awake()
    {
        SetActionMap();

        up = actionMap.FindAction("Up");
        down = actionMap.FindAction("Down");
        left = actionMap.FindAction("Left");
        right = actionMap.FindAction("Right");
        cancel = actionMap.FindAction("Cancel");
        duoToggle = actionMap.FindAction("DuoToggle");
        comboData = GetComponent<ComboData>();
        comboUI = GetComponent<ComboUI>();

        duoComboManager = GameObject.FindGameObjectWithTag("DuoComboManager")?.GetComponent<DuoComboManager>();
    }
    private void OnEnable()
    {
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();
        cancel.Enable();
        duoToggle.Enable();

        // Assigning callback to each input key
        up.performed += CheckInitialInput;
        down.performed += CheckInitialInput;
        left.performed += CheckInitialInput;
        right.performed += CheckInitialInput;
        duoToggle.performed += ToggleDuo;
    }
    private void OnDisable()
    {
        up.Disable();
        down.Disable();
        left.Disable();
        right.Disable();
        cancel.Disable();
        duoToggle.Disable();

        // Removing callback from each input key
        up.performed -= CheckInitialInput;
        down.performed -= CheckInitialInput;
        left.performed -= CheckInitialInput;
        right.performed -= CheckInitialInput;
        duoToggle.performed -= ToggleDuo;
    }

    // Toggles between whether a player wants to start duo combos
    private void ToggleDuo(InputAction.CallbackContext context)
    {
        comboData.duoToggle = !comboData.duoToggle;
    }

    // Enable/Disables Input
    public void ToggleInput(bool cond)
    {
        comboData.isInputEnabled = cond;
    }

    public void IsInDuoCombo(bool cond)
    {
        comboData.isInDuoCombo = cond;
    }

    // Checks if first 2 inputs match any combos that start off with the 2 inputs
    void CheckInitialInput(InputAction.CallbackContext context)
    {
        if (!comboData.isInputEnabled)
        {
            return;
        }
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
        var comboList = (comboData.duoToggle) ? comboData.duoComboList : comboData.comboList;
        foreach (var combo in comboList)
        {
            if (combo[0] == comboData.firstInput && combo[1] == comboData.secondInput)
            {
                Debug.Log("Matching Combo" + string.Join(", ", combo));
                if (comboData.duoToggle)
                {
                    duoComboManager.StartDuoCombo(combo, transform.parent.gameObject);
                } else
                {
                    StartCombo(combo, true);
                }
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

    public void StartCombo(List<KeyCode> combo, bool startedCombo)
    {
        comboData.currentSequenceIndex = (startedCombo) ? 2 : 0;
        comboUI.InitializeUI(combo, comboData.currentSequenceIndex);
        comboData.currentCombo = combo;
        comboData.initiatedCombo = true;
        up.performed -= CheckInitialInput;
        down.performed -= CheckInitialInput;
        left.performed -= CheckInitialInput;
        right.performed -= CheckInitialInput;

        up.performed += ComboSequence;
        down.performed += ComboSequence;
        left.performed += ComboSequence;
        right.performed += ComboSequence;
        cancel.performed += CancelCombo;
        if (startedCombo)
        {
            comboData.mistakeOrder.AddRange(new[] { "Correct", "Correct" });
            StartTimer(30);
        } 
    }

    public void StartTimer(float time)
    {
        StartCoroutine(StartCountdown(time));
    }

    public void RestartCombo()
    {
        if (comboData.isInDuoCombo)
        {
            duoComboManager.CompletedHalf(transform.parent.gameObject, comboData.timerVal, comboData.isAbrupt);
        }
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
        if (!comboData.isInputEnabled)
        {
            return;
        }
        // Determine the current input
        KeyCode inputKey = GetKeyFromContext(context);
        Debug.Log(inputKey);
        comboData.lastKeyPressed = inputKey;
        if (comboData.currentSequenceIndex < comboData.currentCombo.Count)
        {
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
        }

        comboData.currentSequenceIndex++;

        if (comboData.currentSequenceIndex == comboData.currentCombo.Count)
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
        if (!comboData.isInputEnabled)
        {
            return;
        }
        Debug.Log("Combo Canceled");
        comboData.isAbrupt = true;
        RestartCombo();
    }

    private void ComboTimerExpired()
    {
        Debug.Log("Ran out of time");
        comboData.isAbrupt = true;
        RestartCombo();
    }
    public IEnumerator StartCountdown(float countdownValue) // 7 seconds
    {
        comboData.timerVal = countdownValue;
        timer.GetComponent<ComboTimer>().InitializeTimer(countdownValue);
        while (comboData.timerVal > 0)
        {
            yield return new WaitForSeconds(1.0f);
            comboData.timerVal--;
        }
        // If out of time
        ComboTimerExpired();
    }

    // Sets action map depending on which player
    private void SetActionMap()
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
    }
    // Helper method to translate the InputAction context to KeyCode for identification
    private KeyCode GetKeyFromContext(InputAction.CallbackContext context)
    {
        if (context.action == up) return KeyCode.UpArrow;
        if (context.action == down) return KeyCode.DownArrow;
        if (context.action == left) return KeyCode.LeftArrow;
        if (context.action == right) return KeyCode.RightArrow;

        return KeyCode.None;
    }
}
