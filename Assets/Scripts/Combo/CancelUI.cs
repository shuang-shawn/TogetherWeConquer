using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the logic of the cancel UI
/// </summary>
public class CancelUI : MonoBehaviour
{
    private InputActionMap comboP1;
    private InputActionMap comboP2;
    private InputAction cancelP1;
    private InputAction cancelP2;

    [SerializeField]
    private TextMeshProUGUI p1CancelText;

    [SerializeField]
    private TextMeshProUGUI p2CancelText;

    private void Awake()
    {
        // Initialize the generated InputActions class
        var inputActions = new InputActions();

        // Access ComboP1 directly if it was generated as a property
        comboP1 = inputActions.ComboP1;
        comboP2 = inputActions.ComboP2;
        cancelP1 = comboP1.FindAction("Cancel");
        cancelP2 = comboP2.FindAction("Cancel");

        // Retrieve the binding path for the first binding of DuoToggle
        string bindingPathP1 = cancelP1.bindings[0].effectivePath;
        string bindingPathP2 = cancelP2.bindings[0].effectivePath;

        // Extract only the key part from the binding path
        string P1KeyBind = bindingPathP1.Replace("<Keyboard>/", "");
        string P2KeyBind = bindingPathP2.Replace("<Keyboard>/", "");
        p1CancelText.text = FormatKeyBind(P1KeyBind);
        p2CancelText.text = FormatKeyBind(P2KeyBind);
    }

    string FormatKeyBind(string bindingPath)
    {
        string keyBind = bindingPath.Replace("<Keyboard>/", "");

      
        if (keyBind.StartsWith("numpad"))
        {
          
            keyBind = keyBind.Substring(6);
        }

        return keyBind.ToUpper();
    }
}
