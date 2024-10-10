using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    public GameObject[] arrowImages;
    public GameObject[] scores;
    public GameObject comboUIParent;
    public GameObject scoreUIParent;
    public ComboUIFX shake;

    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;

    public List<GameObject> currentComboUI = new List<GameObject>();

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("FXManager").GetComponent<ComboUIFX>();
    }
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

        shake.TriggerShake(currentComboUI[currentSequenceIndex].GetComponent<Image>());
    }

    public void ShowScore(int mistakeCount, int totalKeys)
    {
        GameObject score = null;
        float ratio = (float)mistakeCount / totalKeys;
        Debug.Log(ratio);

        if (mistakeCount <= 0)
        {
            score = Instantiate(scores[0], scoreUIParent.transform);
            Debug.Log("Perfect!");
        }
        else if (ratio < 0.5f)
        {
            score = Instantiate(scores[1], scoreUIParent.transform);
            Debug.Log("Good");
        }
        else
        {
            score = Instantiate(scores[2], scoreUIParent.transform);
            Debug.Log("Fail");
        }

        currentComboUI?.Add(score);

        shake.TriggerShake(score.GetComponent<Image>());
    }

    public void ResetUI()
    {
        currentComboUI.Clear();
        // Destroy all child objects of comboUIParent
        foreach (Transform child in comboUIParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in scoreUIParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
