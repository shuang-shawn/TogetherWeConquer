using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReviveLogic : MonoBehaviour
{

    private string playerTag;
    private GameObject playerInstance;

    [SerializeField]
    private InputActionAsset playerControls;
    private InputActionMap actionMap;

    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;
    private InputAction duoToggle;

    private ComboUI comboUI;

    [SerializeField]
    private GameObject timer;

    [SerializeField]
    private float timeLimit;

    [SerializeField]
    private TextMeshProUGUI reviveHeader;
    [SerializeField]
    private TextMeshProUGUI progressHeader;
    [SerializeField]
    private Animator progressHeaderSuccess;

    private int maxComboSequence = 5;
    private int currentSequenceIndex = 0;
    private int currentComboLoop = 0;
    private int maxComboLoop = 5;

    private List<KeyCode> masterComboSequence = new List<KeyCode>{ KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    
    [SerializeField]
    private List<KeyCode> currentCombo;

    private bool preventDisable = false;

    private Coroutine currentTimer;

    private System.Random random = new System.Random();

    private void Start()
    {
        comboUI = GetComponent<ComboUI>();
    }

    public void SetPlayerInfo(string tag, GameObject playerInstance)
    {
        playerTag = tag;
        this.playerInstance = playerInstance;
        InitializeInputActions();
    }

    private void InitializeInputActions()
    {
        actionMap = (playerTag == "Player1") ? playerControls.FindActionMap("ComboP1") : playerControls.FindActionMap("ComboP2");
        up = actionMap.FindAction("Up");
        down = actionMap.FindAction("Down");
        left = actionMap.FindAction("Left");
        right = actionMap.FindAction("Right");
        duoToggle = actionMap.FindAction("DuoToggle");
        SetHeader();
    }

    private void OnEnable()
    {
            up.Enable();
            down.Enable();
            left.Enable();
            right.Enable();
            duoToggle.Enable();
            duoToggle.performed += StartReviveCombo;
    }

    private void OnDisable()
    {
        if (!preventDisable)
        {
            preventDisable = true;
            up.Disable();
            down.Disable();
            left.Disable();
            right.Disable();
            duoToggle.Disable();

            // Removing callback from each input key
            up.performed -= ComboSequence;
            down.performed -= ComboSequence;
            left.performed -= ComboSequence;
            right.performed -= ComboSequence;
            duoToggle.performed -= StartReviveCombo;
        }
    }

    private void StartReviveCombo(InputAction.CallbackContext context)
    {
        reviveHeader.gameObject.SetActive(false);
        progressHeader.gameObject.SetActive(true);
        UpdateProgressUI();
        duoToggle.Disable();
     
        StartComboLoop();
        up.performed += ComboSequence;
        down.performed += ComboSequence;
        left.performed += ComboSequence;
        right.performed += ComboSequence;
    }

    private void StartComboLoop()
    {
        if (currentComboLoop >= maxComboLoop)
        {
            ResetSequence();
            OnDisable();
            playerInstance.SetActive(true);
            playerInstance.GetComponent<PlayerManager>().Heal(100);
            Destroy(gameObject);

            playerInstance.GetComponentInChildren<ComboData>().revived = true;
            StartCoroutine(ResetRevivedFlag());
        } else
        {
            currentCombo = GenerateRandomCombo();
            comboUI.InitializeUI(currentCombo, 0);
            StartTimer();
        }
    }

    private IEnumerator ResetRevivedFlag()
    {
        yield return null;
        playerInstance.GetComponentInChildren<ComboData>().revived = false;    
    }

    private void ComboSequence(InputAction.CallbackContext context)
    {
        KeyCode inputKey = GetKeyFromContext(context);

        // Checks if current input is correct or wrong
        if (inputKey == currentCombo[currentSequenceIndex])
        {
            comboUI.UpdateArrow(currentSequenceIndex, true);
        }
        else
        {
            comboUI.UpdateArrow(currentSequenceIndex, false);
            StartCoroutine(Scoring(true, 1f));
            actionMap.Disable();
            return;
        }
        currentSequenceIndex++;

        // Check if combo is done
        if (currentSequenceIndex == currentCombo.Count)
        {
            StartCoroutine(Scoring(false, 0.5f));
        }
    }
    
    private IEnumerator Scoring(bool mistake, float delay)
    {
        StopCoroutine(currentTimer);
        currentTimer = null;
        timer.GetComponent<ComboTimer>().ResetTimer();
        if (mistake)
        {
            comboUI.ShowScore(1, 1);
            currentComboLoop = 0;
            progressHeaderSuccess.SetTrigger("Fail");
        }
        else
        {
            comboUI.ShowScore(0, 0);
            currentComboLoop++;
            progressHeaderSuccess.SetTrigger("Success");
        }
       
        UpdateProgressUI();
        yield return new WaitForSeconds(delay);
        ResetSequence();
        StartComboLoop();
    }

    private void ResetSequence()
    {
        comboUI.ResetUI();
        currentSequenceIndex = 0;
        actionMap.Enable();
    }

    private void StartTimer()
    {
        currentTimer = StartCoroutine(StartCountdown(timeLimit));
    }

    private IEnumerator StartCountdown(float countdownValue) 
    {
        timer.GetComponent<ComboTimer>().InitializeTimer(countdownValue, countdownValue);
        while (countdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countdownValue--;
        }
        EndReviveCombo();
    }

    private void EndReviveCombo()
    {
        currentTimer = null;
        timer.GetComponent<ComboTimer>().ResetTimer();
        StopAllCoroutines();
        currentComboLoop = 0;
        ResetSequence();
        duoToggle.Enable();
        up.performed -= ComboSequence;
        down.performed -= ComboSequence;
        left.performed -= ComboSequence;
        right.performed -= ComboSequence;
        reviveHeader.gameObject.SetActive(true);
        progressHeader.gameObject.SetActive(false);
    }

    private List<KeyCode> GenerateRandomCombo()
    {
        List<KeyCode> randomCombo = new List<KeyCode>();

        for (int i = 0; i < maxComboSequence; i++)
        {
            int randomIndex = random.Next(0, masterComboSequence.Count);
      
            randomCombo.Add(masterComboSequence[randomIndex]);
        }

        return randomCombo;
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

    private void SetHeader()
    {
        string bindingPath = duoToggle.bindings[0].effectivePath;
        string keyBind = bindingPath.Replace("<Keyboard>/", "");
       
        reviveHeader.text = "Revive " + keyBind.ToUpper();
    }

    private void UpdateProgressUI()
    {
        progressHeader.text = currentComboLoop + "/" + maxComboLoop;
    }
}
