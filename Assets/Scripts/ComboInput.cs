using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ComboInput : MonoBehaviour
{
    // Input System
    public InputActionAsset playerControls;
    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;

    public List<KeyCode> currentCombo; 

    public int currentSequenceIndex = 2;
    public int mistakes = 0;
    
    ComboDataHolder dataholder;

    public bool inCombo = false;
    private void Awake()
    {
        var actionMap = playerControls.FindActionMap("Combo");
        up = actionMap.FindAction("Up");
        down = actionMap.FindAction("Down");
        left = actionMap.FindAction("Left");
        right = actionMap.FindAction("Right");
        dataholder = gameObject.GetComponent<ComboDataHolder>();

        //dataholder.currentComboIndex = currentSequenceIndex;
        Debug.Log(currentCombo.Count);
    }
    private void OnEnable()
    {
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();

        // Assigning callback to each input key
        up.performed += ComboSequence;
        down.performed += ComboSequence;
        left.performed += ComboSequence;
        right.performed += ComboSequence;

    }
    private void OnDisable()
    {
        up.Disable();
        down.Disable();
        left.Disable();
        right.Disable();

        // Removing callback from each input key
        up.performed -= ComboSequence;
        down.performed -= ComboSequence;
        left.performed -= ComboSequence;
        right.performed -= ComboSequence;
    }
    private void ComboSequence(InputAction.CallbackContext context)
    {
        if (inCombo)
        {
            // Determine the current input
            KeyCode inputKey = GetKeyFromContext(context);
            Debug.Log(inputKey);

            if (inputKey == currentCombo[currentSequenceIndex])
            {
                Debug.Log("Correct Input");
            }
            else
            {
                mistakes++;
            }
            currentSequenceIndex++;
            if (currentSequenceIndex >= currentCombo.Count)
            {
                Debug.Log("Combo successful!");
                currentSequenceIndex = 0; // Reset combo
                mistakes = 0;
                gameObject.GetComponent<ComboSelector>().inCombo = false;
                inCombo = false;
            }

            //dataholder.currentComboIndex = currentSequenceIndex;
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
