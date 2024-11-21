using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWindowScaling : MonoBehaviour
{
    public RectTransform mainWindowP1;    // Main Window for Player 1
    public RectTransform contentAreaP1;   // Content Area for Player 1

    public RectTransform mainWindowP2;    // Main Window for Player 2
    public RectTransform contentAreaP2;   // Content Area for Player 2

    void Update()
    {
        AdjustWindowHeight(mainWindowP1, contentAreaP1);
        AdjustWindowHeight(mainWindowP2, contentAreaP2);
    }

    void AdjustWindowHeight(RectTransform mainWindow, RectTransform contentArea)
    {
        // Get the preferred height of the content area based on its children
        float preferredHeight = contentArea.GetComponent<RectTransform>().rect.height;

        // Set the height of the main window based on the content area height
        mainWindow.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
    }
}
