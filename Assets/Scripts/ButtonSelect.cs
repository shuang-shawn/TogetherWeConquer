using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSelector : MonoBehaviour
{
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button confirmButton;

    public GameObject comboListManager;
    private ComboList comboList;

    private List<Combo> newCombos;
    private int selectedOption = -1; // No option selected initially

    private void OnEnable()
    {
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
    }

    private void ChooseImages()
    {
        Image button1Image = option1Button.GetComponent<Image>();
        Image button2Image = option2Button.GetComponent<Image>();
        Image button3Image = option3Button.GetComponent<Image>();

        newCombos = GetRandomCombos(comboList.soloComboList);

        button1Image.sprite = newCombos[0].GetComboIcon();
        button2Image.sprite = newCombos[1].GetComboIcon();
        button3Image.sprite = newCombos[2].GetComboIcon();
    }

    private List<Combo> GetRandomCombos(List<Combo> typeList)
    {
        // Make a list of numbers to draw from so no duplicates
        List<int> numbers = new List<int>();
        for (int i = 0; i < typeList.Count; i++)
        {
            numbers.Add(i);
        }

        // Shuffle the list
        Shuffle(numbers);

        List<Combo> randomCombos = new List<Combo>();

        for (int i = 0; i < 3; i++)
        {
            randomCombos.Add(comboList.soloComboList[numbers[i]]);
        }

        return randomCombos;
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
        // Add Here
        //comboList.AddToCurrentList(newCombos[selectedOption - 1]);
    }

    IEnumerator WaitTwo()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
    }
}
