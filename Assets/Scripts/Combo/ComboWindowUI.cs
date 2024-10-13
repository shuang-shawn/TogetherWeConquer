using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private TextMeshProUGUI p1Header;
    [SerializeField]
    private TextMeshProUGUI p2Header;
    [SerializeField]
    private GameObject comboSequencePrefab;

    // Arrow Image UI
    [SerializeField]
    private GameObject[] arrowImages;

    // Cached instantiated solo combo sequences
    private List<GameObject> p1SoloCombosCached = new List<GameObject>();

    // Cached instantiated duo combo sequences
    private List<GameObject> p1DuoCombosCached = new List<GameObject>();

    // Cached instantiated solo combo sequences
    private List<GameObject> p2SoloCombosCached = new List<GameObject>();

    // Cached instantiated duo combo sequences
    private List<GameObject> p2DuoCombosCached = new List<GameObject>();

    void Start()
    {
        comboList = GetComponent<ComboList>();
        InitializeComboWindow(comboList.soloComboList, p1SoloCombosCached, p2SoloCombosCached, true);
        InitializeComboWindow(comboList.duoComboList, p1DuoCombosCached, p2DuoCombosCached, false);
    }

    private void InitializeComboWindow(List<Combo> comboList, List<GameObject> p1ComboCache, List<GameObject> p2ComboCache, bool setActive)
    {
        foreach (Combo combo in comboList)
        {
            GameObject p1CurrentComboSequence = Instantiate(comboSequencePrefab, p1ComboListView.transform);
            p1ComboCache.Add(p1CurrentComboSequence);
            p1CurrentComboSequence.SetActive(setActive);

            GameObject p2CurrentComboSequence = Instantiate(comboSequencePrefab, p2ComboListView.transform);
            p2ComboCache.Add(p2CurrentComboSequence);
            p2CurrentComboSequence.SetActive(setActive);

            foreach (KeyCode key in combo.GetComboSequence())
            {
                switch (key)
                {
                    case KeyCode.UpArrow:
                        Instantiate(arrowImages[0], p1CurrentComboSequence.transform);
                        Instantiate(arrowImages[0], p2CurrentComboSequence.transform);
                        break;
                    case KeyCode.DownArrow:
                        Instantiate(arrowImages[1], p1CurrentComboSequence.transform);
                        Instantiate(arrowImages[1], p2CurrentComboSequence.transform);
                        break;
                    case KeyCode.LeftArrow:
                        Instantiate(arrowImages[2], p1CurrentComboSequence.transform);
                        Instantiate(arrowImages[2], p2CurrentComboSequence.transform);
                        break;
                    case KeyCode.RightArrow:
                        Instantiate(arrowImages[3], p1CurrentComboSequence.transform);
                        Instantiate(arrowImages[3], p2CurrentComboSequence.transform);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void SwitchComboList(bool isInDuo, string playerTag)
    { 
        List<GameObject> soloCombosCached = playerTag.Equals("Player1") ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals("Player1") ? p1DuoCombosCached : p2DuoCombosCached;

        foreach (GameObject comboUIElement in soloCombosCached)
        {
            comboUIElement.SetActive(!isInDuo);
        }
        foreach (GameObject comboUIElement in duoCombosCached)
        {
            comboUIElement.SetActive(isInDuo);
        }
        UpdateHeader(isInDuo, playerTag) ;
    }

    private void UpdateHeader(bool isInDuo, string playerTag)
    {
        if (playerTag.Equals("Player1"))
        {
            p1Header.SetText(isInDuo ? "Duo Combos" : "Solo Combos");
        }
        else if (playerTag.Equals("Player2"))
        {
            p2Header.SetText(isInDuo ? "Duo Combos" : "Solo Combos");
        }
    }

    public void FilterCombos(KeyCode inputKey, int inputOrder, bool isInDuo, string playerTag)
    {
        List<GameObject> soloCombosCached = playerTag.Equals("Player1") ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals("Player1") ? p1DuoCombosCached : p2DuoCombosCached;

        foreach (GameObject comboUIElement in (isInDuo) ? duoCombosCached : soloCombosCached)
        {
            Transform firstKey = comboUIElement.transform.GetChild(inputOrder);
            Image image = firstKey.GetComponent<Image>();
            if (inputKey != GetKeyFromImage(image))
            {
                comboUIElement.SetActive(false);
            }
        }
    }

    public void ResetComboList(string playerTag)
    {
        //Defaults to solo combo list
        SwitchComboList(false, playerTag);
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
