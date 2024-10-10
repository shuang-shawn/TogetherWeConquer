using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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
    public float duoComboTime;
    public float comboTime;

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

    public bool IsInSoloCombo()
    {
        return comboData.isInSoloCombo;
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
                UnityEngine.Debug.Log("Matching Combo" + string.Join(", ", combo));
                if (comboData.duoToggle)
                {
                    if (!duoComboManager.IsOtherPlayerInSoloCombo(transform.parent.gameObject)) {
                        duoComboManager.StartDuoCombo(combo, transform.parent.gameObject);
                    }
                    break;
                } else
                {
                    StartCombo(combo, true);
                    comboData.isInSoloCombo = true;
                }
                foundMatch = true;
                break;
            }
        }
        if (!foundMatch)
        {
            comboData.firstInput = KeyCode.None;
            comboData.secondInput = KeyCode.None;
            UnityEngine.Debug.Log("No matching combo");
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
            if (comboData.isInDuoCombo)
            {
                StartTimer(duoComboTime);
            }
            else
            {
                StartTimer(comboTime);
            }
        } 
    }

    public void StartTimer(float remainingTime)
    {
        UnityEngine.Debug.Log(comboData.isInDuoCombo);

        if (comboData.isInDuoCombo)
        {
            StartCoroutine(StartCountdown(duoComboTime, remainingTime));
        }
        else
        {
            StartCoroutine(StartCountdown(comboTime, remainingTime));
        }
    }

    public void RestartCombo()
    {
        comboData.ResetData();
        comboUI.ResetUI();
        StopAllCoroutines();
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
        UnityEngine.Debug.Log(inputKey);
        comboData.lastKeyPressed = inputKey;
        if (comboData.currentSequenceIndex < comboData.currentCombo.Count)
        {
            if (inputKey == comboData.currentCombo[comboData.currentSequenceIndex])
            {
                comboData.mistakeOrder.Add("Correct");
                UnityEngine.Debug.Log("Correct Input");
                comboUI.UpdateArrow(comboData.currentSequenceIndex, true);
            }
            else
            {
                UnityEngine.Debug.Log("Incorrect Input");
                comboData.mistakeCount++;
                comboData.mistakeKeysPressed.Add(inputKey);
                comboData.mistakeOrder.Add("Incorrect");
                comboUI.UpdateArrow(comboData.currentSequenceIndex, false);
            }
        }

        comboData.currentSequenceIndex++;

        if (comboData.currentSequenceIndex == comboData.currentCombo.Count)
        {
            StartCoroutine(Scoring(true));
            if (comboData.isInDuoCombo)
            {
                duoComboManager.CompletedHalf(transform.parent.gameObject, comboData.timerVal, comboData.isAbrupt);
            }
        }

    }

    private IEnumerator Scoring(bool isComplete)
    {
        if (isComplete)
        {
            comboUI.ShowScore(comboData.mistakeCount, comboData.currentCombo.Count);
            UnityEngine.Debug.Log("Combo Completed");
        }
        else
        {
            comboUI.comboUIParent.SetActive(false);

            comboUI.ShowCancel();
            UnityEngine.Debug.Log("Combo Cancelled");
        }

        timer.GetComponent<ComboTimer>().ResetTimer();

        yield return new WaitForSeconds(1.0f);

        comboUI.comboUIParent.SetActive(true);

        RestartCombo();
    }

    private void CancelCombo(InputAction.CallbackContext context)
    {
        if (!comboData.isInputEnabled)
        {
            return;
        }
        UnityEngine.Debug.Log("Combo Canceled");
        comboData.isAbrupt = true;
        timer.GetComponent<ComboTimer>().ResetTimer();
        if (comboData.isInDuoCombo)
        {
            duoComboManager.CompletedHalf(transform.parent.gameObject, comboData.timerVal, comboData.isAbrupt);
        }

        StartCoroutine(Scoring(false));
    }

    private void ComboTimerExpired()
    {
        UnityEngine.Debug.Log("Ran out of time");
        comboData.isAbrupt = true;
        if (comboData.isInDuoCombo)
        {
            duoComboManager.CompletedHalf(transform.parent.gameObject, comboData.timerVal, comboData.isAbrupt);
        }
        RestartCombo();
    }
    public IEnumerator StartCountdown(float countdownValue, float remainingTime) // 7 seconds
    {
        comboData.timerVal = countdownValue;
        timer.GetComponent<ComboTimer>().InitializeTimer(countdownValue, remainingTime);
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
