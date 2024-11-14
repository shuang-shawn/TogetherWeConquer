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
    }

    void CloseImage()
    {
        // Resume game
        Time.timeScale = 1;

        // Toggle levelUp in GameStateManager
        gameStateManager.LevelUp();

        // Set Window inactive
        transform.parent.gameObject.SetActive(false);
    }

    private void AcceptCombo()
    {
        optionSelector.AcceptOption();
    }
}
