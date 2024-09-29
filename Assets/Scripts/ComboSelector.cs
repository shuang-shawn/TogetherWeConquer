using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboSelector : MonoBehaviour
{
    // Input System
    public InputActionAsset playerControls;
    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;

    // Static Combos
    private List<List<KeyCode>> comboList = new List<List<KeyCode>>()
    {
        new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightArrow },
        new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow },
        new List<KeyCode> { KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow },
        new List<KeyCode> { KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow },
        new List<KeyCode> { KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }
    };

    public KeyCode firstInput = KeyCode.None;
    public KeyCode secondInput = KeyCode.None;

    ComboDataHolder dataholder;
    public bool inCombo = false;
    private void Awake()
    {
        var actionMap = playerControls.FindActionMap("Combo");
        up = actionMap.FindAction("Up");
        down = actionMap.FindAction("Down");
        left = actionMap.FindAction("Left");
        right = actionMap.FindAction("Right");
        dataholder = GetComponent<ComboDataHolder>();
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
        if (!inCombo)
        {
            KeyCode inputKey = GetKeyFromContext(context);
            if (firstInput == KeyCode.None)
            {
                firstInput = inputKey;
            }
            else if (secondInput == KeyCode.None)
            {
                secondInput = inputKey;
                CheckComboList();
            }
        }
    }
    public void CheckComboList()
    {
        foreach (var combo in comboList)
        {
            if (combo[0] == firstInput && combo[1] == secondInput)
            {
                Debug.Log("Matching Combo" + string.Join(", ", combo));
                StartCombo(combo);
            }
        }
        firstInput = KeyCode.None;
        secondInput = KeyCode.None;
    }

    void StartCombo(List<KeyCode> combo)
    {
        gameObject.GetComponent<ComboInput>().inCombo = true;
        gameObject.GetComponent<ComboInput>().currentCombo = combo;
        //dataholder.currentCombo = combo;
        inCombo = true;
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
