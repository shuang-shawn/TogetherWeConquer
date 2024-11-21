using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuoToggleUI : MonoBehaviour
{
    private InputActionMap comboP1;
    private InputActionMap comboP2;
    private InputAction duoToggleP1;
    private InputAction duoToggleP2;

    [SerializeField]
    private TextMeshProUGUI p1DuoToggleText;

    [SerializeField]
    private TextMeshProUGUI p2DuoToggleText;
    private void Awake()
    {
        // Initialize the generated InputActions class
        var inputActions = new InputActions(); // Ensure this matches your generated class name

        // Access ComboP1 directly if it was generated as a property
        comboP1 = inputActions.ComboP1;
        comboP2 = inputActions.ComboP2;
        duoToggleP1 = comboP1.FindAction("DuoToggle");
        duoToggleP2 = comboP2.FindAction("DuoToggle");

        // Retrieve the binding path for the first binding of DuoToggle
        string bindingPathP1 = duoToggleP1.bindings[0].effectivePath;
        string bindingPathP2 = duoToggleP2.bindings[0].effectivePath;

        // Extract only the key part from the binding path
        string P1KeyBind = bindingPathP1.Replace("<Keyboard>/", "");
        string P2KeyBind = bindingPathP2.Replace("<Keyboard>/", "");
        p1DuoToggleText.text = FormatKeyBind(P1KeyBind);
        p2DuoToggleText.text = FormatKeyBind(P2KeyBind);
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
