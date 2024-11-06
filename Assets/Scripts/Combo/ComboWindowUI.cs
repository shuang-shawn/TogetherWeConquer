using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboWindowUI : MonoBehaviour
{

    private const string Player1Tag = "Player1";
    private const string Player2Tag = "Player2";

    private ComboList comboList;

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
    private Animator p1WindowAnimator;


    [SerializeField]
    private GameObject comboSequencePrefab;

    // Arrow Image UI
    [SerializeField]
    private GameObject[] arrowImages;

    private Color highlightColor = Color.yellow;

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
        InitializeComboWindow(comboList.soloComboList, p1SoloCombosCached, p2SoloCombosCached, true); // Instantiates all solo combo gameobjects, set to visible by default 
        InitializeComboWindow(comboList.duoComboList, p1DuoCombosCached, p2DuoCombosCached, false);  // Instantiates all duo combo gameobjects, stored for later use
    }

    // Instantiates all combo sequences for each player and caches them for later use
    private void InitializeComboWindow(List<Combo> comboList, List<GameObject> p1ComboCache, List<GameObject> p2ComboCache, bool setActive)
    {
        foreach (Combo combo in comboList)
        {
            GameObject p1CurrentComboSequence = Instantiate(comboSequencePrefab, p1ComboListView.transform);
            p1ComboCache.Add(p1CurrentComboSequence);
            p1CurrentComboSequence.SetActive(setActive);

            GameObject p2CurrentComboSequence = Instantiate(comboSequencePrefab, p2ComboListView.transform);

            /** This just aligns combos to the right side for Player 2's List (Optional). **/
            RectTransform rectTransform = p2CurrentComboSequence.GetComponent<RectTransform>();
            // Cache the current anchored position
            Vector2 anchoredPosition = rectTransform.anchoredPosition;
            // Apply the Shift-like behavior by setting the pivot to middle-right
            rectTransform.pivot = new Vector2(1, 0.5f);

            // Apply the Alt-like behavior by maintaining the position relative to the new anchors
            rectTransform.anchorMin = new Vector2(1, 0.5f);  // Set anchor to middle-right
            rectTransform.anchorMax = new Vector2(1, 0.5f);

            // Reapply the anchored position to maintain it relative to the new anchors
            rectTransform.anchoredPosition = anchoredPosition;

            p2ComboCache.Add(p2CurrentComboSequence);
            p2CurrentComboSequence.SetActive(setActive);


            if (combo.HasIcon())
            {
                InstantiateIconAtFront(p1CurrentComboSequence, combo.GetComboIcon());
                InstantiateIconAtFront(p2CurrentComboSequence, combo.GetComboIcon());
            }

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

    // Alternatives between solo and duo combo lists for each player
    public void SwitchComboList(bool isInDuo, string playerTag)
    { 
        List<GameObject> soloCombosCached = playerTag.Equals(Player1Tag) ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals(Player1Tag) ? p1DuoCombosCached : p2DuoCombosCached;

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
        if (playerTag.Equals(Player1Tag))
        {
            p1Header.SetText(isInDuo ? "Duo Combos" : "Solo Combos");
            p1WindowAnimator.SetTrigger(isInDuo ? "window_open" : "window_close");
        }
        else if (playerTag.Equals(Player2Tag))
        {
            p2Header.SetText(isInDuo ? "Duo Combos" : "Solo Combos");
        }
    }

    // When player inputs their first two keys, filters through all possible matching combos
    public void FilterCombos(KeyCode inputKey, int inputOrder, bool isInDuo, string playerTag)
    {
        List<GameObject> soloCombosCached = playerTag.Equals(Player1Tag) ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals(Player1Tag) ? p1DuoCombosCached : p2DuoCombosCached;

        foreach (GameObject comboUIElement in (isInDuo) ? duoCombosCached : soloCombosCached)
        {
            Transform firstKey = comboUIElement.transform.GetChild(inputOrder);
            Image image = firstKey.GetComponent<Image>();
            if (inputKey != GetKeyFromImage(image))
            {
                comboUIElement.SetActive(false);
            } else
            {
                image.color = highlightColor;
            }
        }
    }

    public void ResetComboList(bool isInDuo, string playerTag)
    {
        ResetHighlight(isInDuo, playerTag);
        SwitchComboList(isInDuo, playerTag);
    }

    private void ResetHighlight(bool isInDuo, string playerTag)
    {
        List<GameObject> soloCombosCached = playerTag.Equals(Player1Tag) ? p1SoloCombosCached : p2SoloCombosCached;
        List<GameObject> duoCombosCached = playerTag.Equals(Player1Tag) ? p1DuoCombosCached : p2DuoCombosCached;

        foreach (GameObject comboUIElement in (isInDuo) ? duoCombosCached : soloCombosCached)
        {
            Transform firstKey = comboUIElement.transform.GetChild(0);
            Transform secondKey = comboUIElement.transform.GetChild(1);
            Image first_image = firstKey.GetComponent<Image>();
            Image second_image = secondKey.GetComponent<Image>();
            first_image.color = Color.white;
            second_image.color = Color.white;
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
