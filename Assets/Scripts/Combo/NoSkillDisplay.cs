using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoComboDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject p1ComboContent;
    [SerializeField]
    private GameObject p2ComboContent;
    [SerializeField]
    private GameObject temporaryGameObject;

    private bool hasActiveChildCombo1 = false;
    private bool hasActiveChildCombo2 = false;

    private GameObject tempP1Object;
    private GameObject tempP2Object;

    // Start is called before the first frame update
    void Start()
    {
        if (temporaryGameObject != null)
        {
            // Instantiate the temporary object under p1ComboContent
            tempP1Object = Instantiate(temporaryGameObject, p1ComboContent.transform);
            tempP1Object.SetActive(false); // Initially hide it

            // Instantiate the temporary object under p2ComboContent
            tempP2Object = Instantiate(temporaryGameObject, p2ComboContent.transform);
            tempP2Object.SetActive(false); // Initially hide it
        }
    }

    // Update is called once per frame
    void Update()
    {
        hasActiveChildCombo1 = CheckForActiveChild(p1ComboContent, tempP1Object);
        hasActiveChildCombo2 = CheckForActiveChild(p2ComboContent, tempP2Object);

        // Toggle the visibility of temporary objects based on active children
        if (tempP1Object != null)
        {
            tempP1Object.SetActive(!hasActiveChildCombo1); // Show if no active child, hide otherwise
        }

        if (tempP2Object != null)
        {
            tempP2Object.SetActive(!hasActiveChildCombo2); // Show if no active child, hide otherwise
        }
    }

    // Modified method to exclude the temporary game object from the active child check
    private bool CheckForActiveChild(GameObject comboWindow, GameObject tempObject)
    {
        bool hasActiveChild = false;

        if (comboWindow != null)
        {
            foreach (Transform child in comboWindow.transform)
            {
                // Skip the temporary object itself from the active child check
                if (child.gameObject != tempObject && child.gameObject.activeSelf)
                {
                    hasActiveChild = true;
                    break; // Exit loop if an active child is found
                }
            }
        }
        return hasActiveChild;
    }
}
