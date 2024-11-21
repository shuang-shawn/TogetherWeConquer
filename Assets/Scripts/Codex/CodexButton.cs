using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodexButton : MonoBehaviour
{

    private Combo combo;
    private Button button;
    private Image iconImage;
    public void SetupButton(Combo combo, System.Action<Combo> onClickCallback)
    {
        this.combo = combo;

        // Set up the button onClick listener
        button = GetComponent<Button>();
    
        button.onClick.AddListener(() => onClickCallback(combo));
        iconImage = transform.GetChild(0).GetComponent<Image>();
        // Set the icon for the button if available
        if (combo.HasIcon())
        {
            iconImage.sprite = combo.GetComboIcon();
        }
    }
}
