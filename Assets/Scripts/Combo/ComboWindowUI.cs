using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the logic of the Combo Windows
/// </summary>
public class ComboWindowUI : MonoBehaviour
{
    /** Player tag constants **/
    private const string Player1Tag = "Player1";
    private const string Player2Tag = "Player2";

    private ComboList comboList;

    /** Root Parent ComboWindow Gameobjects (Most Top Level) **/
    [SerializeField]
    private GameObject p1ComboWindow;
    [SerializeField]
    private GameObject p2ComboWindow;

    /** Reference to UI GameObjects **/
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

    /** Flags determining which state the combo window is in. **/
    private bool p1IsInDuoView = false;
    private bool p2IsInDuoView = false;


    /** Animators **/
    [SerializeField]
    private Animator p1WindowAnimator;

    [SerializeField]
    private Animator p2WindowAnimator;

    // Arrow Image UI
    [SerializeField]
    private GameObject[] arrowImages;

    private Color highlightColor = Color.yellow;

    private Color wrongColor = Color.red;

    // Cached instantiated P1 solo combo sequences
    private List<GameObject> p1SoloCombosCached = new List<GameObject>();

    // Cached instantiated P1 duo combo sequences
    private List<GameObject> p1DuoCombosCached = new List<GameObject>();

    // Cached instantiated P2 solo combo sequences
    private List<GameObject> p2SoloCombosCached = new List<GameObject>();

    // Cached instantiated P2 duo combo sequences
    private List<GameObject> p2DuoCombosCached = new List<GameObject>();

    void Start()
    {
        comboList = GameObject.FindGameObjectWithTag("ComboListManager").GetComponent<ComboList>();
        InitializeComboWindow(comboList.currentP1ComboList, p1SoloCombosCached, p1DuoCombosCached, p1ComboListView); // Instantiates all solo combo gameobjects, set to visible by default 
        InitializeComboWindow(comboList.currentP2ComboList, p2SoloCombosCached, p2DuoCombosCached, p2ComboListView);  // Instantiates all duo combo gameobjects, stored for later use
    }

    // Instantiates all combo sequences for each player and caches them for later use
    private void InitializeComboWindow(List<Combo> comboList, List<GameObject> soloComboCache, List<GameObject> duoComboCache, GameObject comboListView)
    {
        foreach (Combo combo in comboList)
        {
            AddComboToWindow(combo, soloComboCache, duoComboCache, comboListView);
        }
    }

    // Public reference to access AddComboToWindow function
    public void AddNewCombo(string playerTag, Combo newCombo)
    {
        switch (playerTag)
        {
            case Player1Tag:
                AddComboToWindow(newCombo, p1SoloCombosCached, p1DuoCombosCached, p1ComboListView, p1IsInDuoView);
                break;
            case Player2Tag:
                AddComboToWindow(newCombo, p2SoloCombosCached, p2DuoCombosCached, p2ComboListView, p2IsInDuoView);
                break;
        }
    }

    // Adds a new combo instance to the list of cached combos
    private void AddComboToWindow(Combo combo, List<GameObject> soloComboCache, List<GameObject> duoComboCache, GameObject comboListView, bool isInDuo = false)
    {
        GameObject currentComboSequence = Instantiate(comboSequencePrefab, comboListView.transform);
        if (combo.GetComboType() == ComboType.Solo)
        {
            currentComboSequence.SetActive(!isInDuo);
            soloComboCache.Add(currentComboSequence);
        }
        else if (combo.GetComboType() == ComboType.Duo)
        {
            currentComboSequence.SetActive(isInDuo);
            duoComboCache.Add(currentComboSequence);
        }

        if (combo.HasIcon())
        {
            InstantiateIconAtFront(currentComboSequence, combo.GetComboIcon());
        }

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

    // Creates combo icon in front of combo
    private void InstantiateIconAtFront(GameObject comboSequence, Sprite icon)
    {
        // Create a new UI image for the icon and set its sprite
        GameObject iconObject = new GameObject("ComboIcon", typeof(Image));
       
        RectTransform rectTransform = iconObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50, 50);
        iconObject.transform.SetParent(comboSequence.transform, false); // Add it as a child to the combo sequence

        // Set the image component's sprite to the combo's icon
        Image iconImage = iconObject.GetComponent<Image>();
        iconImage.sprite = icon;
        iconImage.preserveAspect = true; // Ensure the icon maintains its aspect ratio
    }



    // Alternates between solo and duo combo lists for each player
    public void SwitchComboList(bool isInDuo, string playerTag)
    { 
        List<GameObject> soloCombosCached = playerTag.Equals(Player1Tag) ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals(Player1Tag) ? p1DuoCombosCached : p2DuoCombosCached;
        bool isPlayer1 = playerTag.Equals(Player1Tag);
        p1IsInDuoView = isPlayer1 ? isInDuo : p1IsInDuoView;
        p2IsInDuoView = isPlayer1 ? p2IsInDuoView : isInDuo;
        foreach (GameObject comboUIElement in soloCombosCached)
        {
            comboUIElement.SetActive(!isInDuo);
         
        }
        foreach (GameObject comboUIElement in duoCombosCached)
        {
            comboUIElement.SetActive(isInDuo);
        }
        UpdateHeader(isInDuo, playerTag);
    }

    // Updates the header for either solo or duo combos
    private void UpdateHeader(bool isInDuo, string playerTag)
    {
        p1WindowAnimator.ResetTrigger("window_open");
        p1WindowAnimator.ResetTrigger("window_close");
        p2WindowAnimator.ResetTrigger("window_open");
        p2WindowAnimator.ResetTrigger("window_close");
        if (playerTag.Equals(Player1Tag))
        {
            p1Header.SetText(isInDuo ? "Duo Combos" : "Solo Combos");
            p1WindowAnimator.SetTrigger(isInDuo ? "window_open" : "window_close");
        }
        else if (playerTag.Equals(Player2Tag))
        {
            p2Header.SetText(isInDuo ? "Duo Combos" : "Solo Combos");
            p2WindowAnimator.SetTrigger(isInDuo ? "window_open" : "window_close");
        }
    }

    // When player inputs their first two keys, filters through all possible matching combos
    public void FilterCombos(KeyCode inputKey, int inputOrder, bool isInDuo, string playerTag)
    {
        List<GameObject> soloCombosCached = playerTag.Equals(Player1Tag) ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals(Player1Tag) ? p1DuoCombosCached : p2DuoCombosCached;

        foreach (GameObject comboUIElement in (isInDuo) ? duoCombosCached : soloCombosCached)
        {
            Transform firstKey = comboUIElement.transform.GetChild(inputOrder+1);
            Image image = firstKey.GetComponent<Image>();
            if (inputKey != GetKeyFromImage(image))
            {
                comboUIElement.SetActive(false);
            } 
        }
    }

    // This function highlights the keys after the first two, sequentially based on input order
    public void HighlightSequentialKeys(KeyCode inputKey, int inputOrder, bool isInDuo, string playerTag)
    {
        List<GameObject> soloCombosCached = playerTag.Equals(Player1Tag) ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals(Player1Tag) ? p1DuoCombosCached : p2DuoCombosCached;

        foreach (GameObject comboUIElement in (isInDuo) ? duoCombosCached : soloCombosCached)
        {
            Transform firstKey = comboUIElement?.transform.GetChild(inputOrder+1);
            Image image = firstKey.GetComponent<Image>();
            if (inputKey == GetKeyFromImage(image))
            {
                image.color = highlightColor;
            }
            else
            {
                image.color = wrongColor;
            }
        }

    }

    // Resets state of combo windows
    public void ResetComboList(bool isInDuo, string playerTag)
    {
        ResetHighlight(isInDuo, playerTag);
        SwitchComboList(isInDuo, playerTag);
    }

    // Resets highlights in combos
    private void ResetHighlight(bool isInDuo, string playerTag)
    {
        List<GameObject> soloCombosCached = playerTag.Equals(Player1Tag) ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals(Player1Tag) ? p1DuoCombosCached : p2DuoCombosCached;

        foreach (GameObject comboUIElement in (isInDuo) ? duoCombosCached : soloCombosCached)
        {
            for (int i = 1; i < comboUIElement.transform.childCount; i++)  // Start loop at index 1
            {
                Transform key = comboUIElement.transform.GetChild(i);
                Image image = key.GetComponent<Image>();
                if (image != null)
                {
                    image.color = Color.white; // Reset to white
                }
            }
        }
    }

    private KeyCode GetKeyFromImage(Image image)
    {
        // Change name if using new Image
        if (image.sprite.name == "2021-05-09-103929-Zeichnung 10.png_9") return KeyCode.UpArrow;
        if (image.sprite.name == "2021-05-09-103929-Zeichnung 10.png_10") return KeyCode.DownArrow;
        if (image.sprite.name == "2021-05-09-103929-Zeichnung 10.png_8") return KeyCode.LeftArrow;
        if (image.sprite.name == "2021-05-09-103929-Zeichnung 10.png_11") return KeyCode.RightArrow;

        return KeyCode.None;
    }


    public GameObject GetComboWindow(string playerTag)
    {
        return (playerTag == Player1Tag ? p1ComboWindow : p2ComboWindow);
    }

}
