using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    public GameObject[] arrowImages;
    public GameObject comboUIParent;

    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;

    public List<GameObject> currentComboUI = new List<GameObject>();

    public void InitializeUI(List<KeyCode> combo, int comboIndex)
    {
        foreach (KeyCode key in combo)
        {
            GameObject arrow = null;
            switch (key)
            {
                case KeyCode.UpArrow:
                    arrow = Instantiate(arrowImages[0], comboUIParent.transform);
                    break;
                case KeyCode.DownArrow:
                    arrow = Instantiate(arrowImages[1], comboUIParent.transform);
                    break;
                case KeyCode.LeftArrow:
                    arrow = Instantiate(arrowImages[2], comboUIParent.transform);
                    break;
                case KeyCode.RightArrow:
                    arrow = Instantiate(arrowImages[3], comboUIParent.transform);
                    break;
                default:
                    break;
            }
            currentComboUI?.Add(arrow);
        }

        // Make first two arrows green
        for (int index = 0; index < comboIndex; index++)
        {
            currentComboUI[index].GetComponent<Image>().color = correctColor;
        }
    }

    // Updates current arrow color depending if input is right or wrong
    public void UpdateArrow(int currentSequenceIndex, bool inputState)
    {
        if (inputState)
        {
            currentComboUI[currentSequenceIndex].GetComponent<Image>().color = correctColor; // If Correct Input
        } else
        {
            currentComboUI[currentSequenceIndex].GetComponent<Image>().color = incorrectColor; // If Wrong Input
        }
    }

    public void ResetUI()
    {
        currentComboUI.Clear();
        // Destroy all child objects of comboUIParent
        foreach (Transform child in comboUIParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
