using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboWindowUI : MonoBehaviour
{
    private ComboList comboList;
    [SerializeField]
    private GameObject p1ComboListView;
    [SerializeField]
    private GameObject p2ComboListView;
    [SerializeField]
    private GameObject comboSequencePrefab;

    // Arrow Image UI
    [SerializeField]
    private GameObject[] arrowImages;

    // Cached instantiated combo sequences
    private List<GameObject> comboUIElements = new List<GameObject>();
    void Start()
    {
        comboList = GetComponent<ComboList>();
        InitializeWindow();
    }

    private void InitializeWindow()
    {
        foreach (Combo combo in comboList.soloComboList)
        {
            GameObject currentComboSequence = Instantiate(comboSequencePrefab, p1ComboListView.transform);
            comboUIElements.Add(currentComboSequence);
            foreach (KeyCode key in combo.GetComboSequence())
            {
                switch (key)
                {
                    case KeyCode.UpArrow:
                        Instantiate(arrowImages[0], currentComboSequence.transform);
                        break;
                    case KeyCode.DownArrow:
                        Instantiate(arrowImages[1], currentComboSequence.transform);
                        break;
                    case KeyCode.LeftArrow:
                        Instantiate(arrowImages[2], currentComboSequence.transform);
                        break;
                    case KeyCode.RightArrow:
                        Instantiate(arrowImages[3], currentComboSequence.transform);
                        break;
                    default:
                        break;
                }
            }
        }
        foreach (Combo combo in comboList.duoComboList)
        {
            GameObject currentComboSequence = Instantiate(comboSequencePrefab, p1ComboListView.transform);
            comboUIElements.Add(currentComboSequence);
            currentComboSequence.SetActive(false);
            foreach (KeyCode key in combo.GetComboSequence())
            {
                switch (key)
                {
                    case KeyCode.UpArrow:
                        Instantiate(arrowImages[0], currentComboSequence.transform);
                        break;
                    case KeyCode.DownArrow:
                        Instantiate(arrowImages[1], currentComboSequence.transform);
                        break;
                    case KeyCode.LeftArrow:
                        Instantiate(arrowImages[2], currentComboSequence.transform);
                        break;
                    case KeyCode.RightArrow:
                        Instantiate(arrowImages[3], currentComboSequence.transform);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void FilterCombos(KeyCode inputKey, int inputOrder)
    {

        foreach (GameObject comboUIElement in comboUIElements)
        {
            Transform firstKey = comboUIElement.transform.GetChild(inputOrder);
            Image image = firstKey.GetComponent<Image>();
            if (inputKey != GetKeyFromImage(image)) {
                comboUIElement.SetActive(false);
            }
        }
    }

    private KeyCode GetKeyFromImage(Image image)
    {
        // Change name if using new Image
        if (image.sprite.name == "UpArrow.png") return KeyCode.UpArrow;
        if (image.sprite.name == "DownArrow.png") return KeyCode.DownArrow;
        if (image.sprite.name == "LeftArrow.png") return KeyCode.LeftArrow;
        if (image.sprite.name == "RightArrow.png") return KeyCode.RightArrow;

        return KeyCode.None;
    }
}
