using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseWindow : MonoBehaviour
{
    private Button closeButton;
    private GameStateManager gameStateManager;
    [SerializeField]
    private OptionSelector optionSelector;

    void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
    }

    void Awake()
    {
        closeButton = GetComponent<Button>();
    }

    void OnEnable()
    {
        // Clear previous listeners
        closeButton.onClick.RemoveAllListeners();

        // Add a new listener to disable the window
        closeButton.onClick.AddListener(AcceptCombo);
        closeButton.onClick.AddListener(CloseImage);
        //Pause game
        Time.timeScale = 0;

        // Restrict Player Inputs
        GameObject.FindGameObjectWithTag("Player1").GetComponentInChildren<ComboInput>()?.ToggleInput(false);
        GameObject.FindGameObjectWithTag("Player2").GetComponentInChildren<ComboInput>()?.ToggleInput(false);
    }

    void CloseImage()
    {
        // Resume game
        Time.timeScale = 1;

        // Allow Player Inputs
        GameObject.FindGameObjectWithTag("Player1").GetComponentInChildren<ComboInput>()?.ToggleInput(true);
        GameObject.FindGameObjectWithTag("Player2").GetComponentInChildren<ComboInput>()?.ToggleInput(true);
        // Set Window inactive
        transform.parent.gameObject.SetActive(false);
    }

    private void AcceptCombo()
    {
        optionSelector.AcceptOption();
    }
}
