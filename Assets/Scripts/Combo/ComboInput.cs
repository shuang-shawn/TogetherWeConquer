using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class ComboInput : MonoBehaviour
{
    // Input System
    public InputActionAsset playerControls;
    private InputActionMap actionMap;

    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;
    private InputAction cancel;
    private InputAction duoToggle;

    // Attatched combo data related scripts
    private ComboData comboData;
    private ComboList comboList;

    // UI
    private ComboUI comboUI;
    private ComboWindowUI comboWindowUI;
    public GameObject timer;
    public float duoComboTime;
    public float comboTime;

    public DuoComboManager duoComboManager;
    public GameObject skillManagerObject; // temp holder for skill system
    private SkillManager skillManager;

    private string playerTag;
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

        comboList = GameObject.FindGameObjectWithTag("ComboListManager").GetComponent<ComboList>();
        duoComboManager = GameObject.FindGameObjectWithTag("DuoComboManager")?.GetComponent<DuoComboManager>();
        comboWindowUI = GameObject.FindGameObjectWithTag("ComboWindow")?.GetComponent<ComboWindowUI>();
        
        skillManager = skillManagerObject.GetComponent<SkillManager>();
        
    }
    private void OnEnable()
    {
        actionMap.Enable();
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
        if (!comboData.isInputEnabled)
        {
            return;
        }
        comboData.duoToggle = !comboData.duoToggle;
        ResetInitalInputs();
       
    }

    // Listens for players first two input keys
    private void CheckInitialInput(InputAction.CallbackContext context)
    {
        if (!comboData.isInputEnabled)
        {
            return;
        }
        KeyCode inputKey = GetKeyFromContext(context);
        if (comboData.firstInput == KeyCode.None)
        {
            comboData.firstInput = inputKey;
            comboWindowUI.FilterCombos(inputKey, 0, comboData.duoToggle, playerTag);
            comboWindowUI.HighlightSequentialKeys(inputKey, 0, comboData.duoToggle, playerTag);
        }
        else if (comboData.secondInput == KeyCode.None)
        {
            comboData.secondInput = inputKey;
            comboWindowUI.FilterCombos(inputKey, 1, comboData.duoToggle, playerTag);
            comboWindowUI.HighlightSequentialKeys(inputKey, 1, comboData.duoToggle, playerTag);
            CheckComboList();
        }
    }

    // Checks in either the solo or duo combo list based on the users first two inputs for matching combos
    private void CheckComboList()
    {
        var selectedComboList = (transform.parent.gameObject.tag == "Player1") ? comboList.currentP1ComboList : comboList.currentP2ComboList;
        selectedComboList = selectedComboList
        .Where(combo => combo.GetComboType() == (comboData.duoToggle ? ComboType.Duo : ComboType.Solo))
        .ToList();
        var currentPlayer = transform.parent.gameObject;

        foreach (Combo combo in selectedComboList)
        {
            if (combo.GetComboSequence()[0] == comboData.firstInput && combo.GetComboSequence()[1] == comboData.secondInput)
            {
                // For matching combos
                if (comboData.duoToggle)
                {
                    // Doesnt allow player to start duo combo while other player is currently in solo combo
                    if (!duoComboManager.IsOtherPlayerInSoloCombo(currentPlayer)) { 
                        duoComboManager.StartDuoCombo(combo, currentPlayer);
                    } else
                    {
                        UnityEngine.Debug.Log("Other player is busy");
                        ResetInitalInputs();
                    }
 
                } else
                {
                    StartCombo(combo, combo.GetComboSequence(), true);
                    comboData.isInSoloCombo = true;   
                }
                return;
            }
        }
        // For no matching combos
        ResetInitalInputs();
        UnityEngine.Debug.Log("No matching combo");
    }

    private void ResetInitalInputs()
    {
        comboData.firstInput = KeyCode.None;
        comboData.secondInput = KeyCode.None;
        comboWindowUI.ResetComboList(comboData.duoToggle, playerTag);
    }

    // Logic for starting the combo
    public void StartCombo(Combo combo, List<KeyCode> currentComboSequence, bool startedCombo)
    {
        comboData.currentSequenceIndex = (startedCombo) ? 2 : 0; // Make first two inputs in the combo as correct, based off initial inputs
        comboUI.InitializeUI(currentComboSequence, comboData.currentSequenceIndex);
        comboData.currentCombo = currentComboSequence;
        comboData.currentComboObject = combo;

        UpdateComboInputCallbacks(true);

        if (startedCombo)
        {
            comboData.mistakeOrder.AddRange(new[] { "Correct", "Correct" });
            StartTimer(comboData.isInDuoCombo ? duoComboTime : comboTime);
        } 
    }

    public void StartTimer(float remainingTime)
    {
        if (comboData.isInDuoCombo)
        {
            StartCoroutine(StartCountdown(duoComboTime, remainingTime));
        }
        else
        {
            StartCoroutine(StartCountdown(comboTime, remainingTime));
        }
    }

    // Logic for finish combo and resetting data
    public void RestartCombo()
    {
        comboData.ResetData();
        comboUI.ResetUI();
        comboWindowUI.ResetComboList(false, playerTag);
        StopAllCoroutines();

        comboData.finishedCombo = true;
        StartCoroutine(ResetCompleteFlag());

        UpdateComboInputCallbacks(false);
    }

    private IEnumerator ResetCompleteFlag()
    {
        yield return null;
        comboData.finishedCombo = false;
    }

    // Listens for inputs during the combo sequence
    private void ComboSequence(InputAction.CallbackContext context)
    {
        if (!comboData.isInputEnabled)
        {
            return;
        }

        // Determine the current input
        KeyCode inputKey = GetKeyFromContext(context);
        comboData.lastKeyPressed = inputKey;

        // Checks if current input is correct or wrong
        if (comboData.currentSequenceIndex < comboData.currentCombo.Count)
        {
            if (inputKey == comboData.currentCombo[comboData.currentSequenceIndex])
            {
                comboData.mistakeOrder.Add("Correct");
                comboUI.UpdateArrow(comboData.currentSequenceIndex, true);
                comboWindowUI.HighlightSequentialKeys(inputKey, comboData.currentSequenceIndex, comboData.isInDuoCombo, playerTag);
            }
            else
            {
                comboData.mistakeOrder.Add("Incorrect");
                comboData.mistakeKeysPressed.Add(inputKey);
                comboData.mistakeCount++;
                comboUI.UpdateArrow(comboData.currentSequenceIndex, false);
            }
        }

        comboData.currentSequenceIndex++;

        // Check if full combo is done
        if (comboData.currentSequenceIndex == comboData.currentCombo.Count)
        {
            StartCoroutine(Scoring(true));
            if (comboData.isInDuoCombo)
            {
                duoComboManager.CompletedHalf(transform.parent.gameObject, comboData.timerVal, comboData.isAbrupt);
            } else
            {
                UnityEngine.Debug.Log("Solo skill by: " + playerTag);
                string skillToCast = comboData.currentComboObject.GetComboSkill();
                UnityEngine.Debug.Log("Casting solo skill: " + skillToCast);
                skillManager.CastSkill(skillToCast, playerTag, comboData.currentComboObject.GetComboType());

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

    // Handles logic for canceling a combo
    private void CancelCombo(InputAction.CallbackContext context)
    {
        if (!comboData.isInputEnabled)
        {
            return;
        }
        cancel.performed -= CancelCombo;
        comboData.isAbrupt = true;
        timer.GetComponent<ComboTimer>().ResetTimer();
        if (comboData.isInDuoCombo)
        {
            duoComboManager.CompletedHalf(transform.parent.gameObject, comboData.timerVal, comboData.isAbrupt);
        }

        StartCoroutine(Scoring(false));
    }

    // Handles logic for when a player runs out of time to finish their combo
    private void ComboTimerExpired()
    {
        comboData.isAbrupt = true;
        if (comboData.isInDuoCombo)
        {
            duoComboManager.CompletedHalf(transform.parent.gameObject, comboData.timerVal, comboData.isAbrupt);
        }
        RestartCombo();
    }

    // Logic for decreasing the combo timer
    private IEnumerator StartCountdown(float countdownValue, float remainingTime) // 7 seconds
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

    // Sets action map depending on which player
    private void SetActionMap()
    {
        // Assigns different Action Map depending on player
        if (transform.parent.CompareTag("Player1"))
        {
            actionMap = playerControls.FindActionMap("ComboP1");
        }
        else if (transform.parent.CompareTag("Player2"))
        {
            actionMap = playerControls.FindActionMap("ComboP2");
        }
        // Assigns player tag string
        playerTag = transform.parent.tag; 
    }

    private void UpdateComboInputCallbacks(bool startingCombo)
    {
        if (startingCombo)
        {
            up.performed -= CheckInitialInput;
            down.performed -= CheckInitialInput;
            left.performed -= CheckInitialInput;
            right.performed -= CheckInitialInput;

            up.performed += ComboSequence;
            down.performed += ComboSequence;
            left.performed += ComboSequence;
            right.performed += ComboSequence;
            cancel.performed += CancelCombo;
        } else
        {
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
