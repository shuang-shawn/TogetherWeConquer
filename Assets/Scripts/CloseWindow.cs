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
    private GameObject player1;
    private GameObject player2;

    void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
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
        player1?.GetComponentInChildren<ComboInput>()?.ToggleInput(false);
        player2?.GetComponentInChildren<ComboInput>()?.ToggleInput(false);
    }

    void CloseImage()
    {
        // Resume game
        Time.timeScale = 1;

        // Allow Player Inputs
        player1?.GetComponentInChildren<ComboInput>()?.ToggleInput(true);
        player2?.GetComponentInChildren<ComboInput>()?.ToggleInput(true);
        // Set Window inactive
        transform.parent.gameObject.SetActive(false);
    }

    private void AcceptCombo()
    {
        optionSelector.AcceptOption();
    }
}
