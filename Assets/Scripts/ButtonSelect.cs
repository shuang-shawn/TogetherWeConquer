using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSelector : MonoBehaviour
{
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button confirmButton;
    public Sprite P1Background;
    public Sprite P2Background;
    public Sprite DuoBackground;

    public GameObject comboListManager;
    private ComboList comboList;
    public GameStateManager stateManager;

    private List<Combo> newCombos;
    private int selectedOption = -1; // No option selected initially

    private void OnEnable()
    {
        if (!stateManager.duoLevel)
        {
            if (stateManager.isPlayer1Level)
            {
                gameObject.GetComponent<Image>().sprite = P1Background;
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = P2Background;
            }
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = DuoBackground;
        }
        confirmButton.gameObject.SetActive(false);

        comboList = comboListManager.GetComponent<ComboList>();

        if (comboList == null)
        {
            UnityEngine.Debug.Log("Missing");
        }

        if (comboList.soloComboList == null)
        {
            UnityEngine.Debug.Log("Missing");
        }

        ResetButtonColors();

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();
        option3Button.onClick.RemoveAllListeners();

        ChooseImages();

        option1Button.onClick.AddListener(() => SelectOption(1));
        option2Button.onClick.AddListener(() => SelectOption(2));
        option3Button.onClick.AddListener(() => SelectOption(3));

        if (newCombos[0].IsDummy() && newCombos[1].IsDummy() && newCombos[2].IsDummy())
        {
            confirmButton.gameObject.SetActive(true);
        }
    }

    private void ChooseImages()
    {
        Image button1Image = option1Button.GetComponent<Image>();
        Image button2Image = option2Button.GetComponent<Image>();
        Image button3Image = option3Button.GetComponent<Image>();

        TextMeshProUGUI button1Description = option1Button.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI button2Description = option2Button.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI button3Description = option3Button.GetComponentInChildren<TextMeshProUGUI>();

        if (!stateManager.duoLevel)
        {
            newCombos = GetRandomCombos(comboList.soloComboList);
        }
        else
        {
            newCombos = GetRandomCombos(comboList.duoComboList);
        }

        button1Image.sprite = newCombos[0].GetComboIcon();
        button2Image.sprite = newCombos[1].GetComboIcon();
        button3Image.sprite = newCombos[2].GetComboIcon();

        UnityEngine.Debug.Log(newCombos[0].GetDescription());

        button1Description.text = newCombos[0].GetDescription();
        button2Description.text = newCombos[1].GetDescription();
        button3Description.text = newCombos[2].GetDescription();
    }

    private List<Combo> GetRandomCombos(List<Combo> typeList)
    {
        List<Combo> unlearnedCombos = new List<Combo>();

        foreach (Combo combo in typeList) 
        {
            if (stateManager.isPlayer1Level && !combo.GetLearnedP1())
            {
                unlearnedCombos.Add(combo);
            }
            else if (!stateManager.isPlayer1Level && !combo.GetLearnedP2())
            {
                unlearnedCombos.Add(combo);
            }
        }

        UnityEngine.Debug.Log(unlearnedCombos.Count);

        // Make a list of numbers to draw from so no duplicates
        List<int> numbers = new List<int>();
        for (int i = 0; i < unlearnedCombos.Count; i++)
        {
            numbers.Add(i);
        }

        // Shuffle the list
        Shuffle(numbers);

        List<Combo> randomCombos = new List<Combo>();

        for (int i = 0; i < Mathf.Min(3, unlearnedCombos.Count); i++)
        {
            randomCombos.Add(unlearnedCombos[numbers[i]]);
        }

        // Fill remaining slots with dummy combos
        while (randomCombos.Count < 3)
        {
            randomCombos.Add(CreateDummyCombo());
        }

        return randomCombos;
    }

    private Combo CreateDummyCombo()
    {
        Combo dummy = new Combo(ComboType.Solo, new List<KeyCode>(), "No Combo", Resources.Load<Sprite>("Skill Icons/noSkill"), "No combo available", true);
        return dummy;
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void SelectOption(int option)
    {
        if (newCombos[option - 1].IsDummy())
        {
            Debug.Log("Cannot select a dummy combo.");
            return; // Ignore the selection
        }

        selectedOption = option;
        Debug.Log("Selected Option: " + selectedOption);

        confirmButton.gameObject.SetActive(true);
        // Update button visuals based on selection
        UpdateButtonColors();
    }

    void UpdateButtonColors()
    {
        // Reset all button colors
        ResetButtonColors();

        // Change color of the selected option (for example, to green)
        switch (selectedOption)
        {
            case 1:
                option1Button.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                option2Button.GetComponent<Image>().color = Color.green;
                break;
            case 3:
                option3Button.GetComponent<Image>().color = Color.green;
                break;
        }
    }

    void ResetButtonColors()
    {
        // Set all button colors to default
        option1Button.GetComponent<Image>().color = Color.white;
        option2Button.GetComponent<Image>().color = Color.white;
        option3Button.GetComponent<Image>().color = Color.white;
    }

    public void AcceptOption()
    {
        if (newCombos[selectedOption - 1].IsDummy())
        {
            return;
        }

        if (!stateManager.duoLevel)
        {
            if (stateManager.isPlayer1Level)
            {
                comboList.AddP1SoloSkill(newCombos[selectedOption - 1]);
            }
            else
            {
                comboList.AddP2SoloSkill(newCombos[selectedOption - 1]);
            }
        }
        else
        {
            comboList.AddP1SoloSkill(newCombos[selectedOption - 1]);
            comboList.AddP2SoloSkill(newCombos[selectedOption - 1]);
        }
    }

    IEnumerator WaitTwo()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
    }
}

