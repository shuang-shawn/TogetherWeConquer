using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboType
{
    Solo,
    Duo
}

public class Combo
{
    private List<KeyCode> comboSequence;
    private ComboType comboType;
    private string comboSkill;
    private Sprite comboIcon;

    public Combo(ComboType comboType, List<KeyCode> comboSequence, string skillName, Sprite skillIcon = null)
    {
        this.comboSequence = comboSequence;
        this.comboType = comboType;
        comboSkill = skillName;
        comboIcon = skillIcon;
    }

    public List<KeyCode> GetComboSequence()
    {
        return comboSequence;
    }

    public ComboType GetComboType() {
        return comboType;
    }

    public string GetComboSkill() { 
        return comboSkill;
    }
    public Sprite GetComboIcon()
    {
        return comboIcon;
    }

    public bool HasIcon()
    {
        return comboIcon != null;
    }

    public override string ToString()
    {
        // Build a string representation of the Combo object
        string comboSequenceStr = string.Join(", ", comboSequence);  // Converts the KeyCode list to a string
        string iconInfo = HasIcon() ? comboIcon.name : "No Icon";  // Checks if Combo has an icon
        return $"Combo Skill: {comboSkill}, Type: {comboType}, Sequence: [{comboSequenceStr}], Icon: {iconInfo}";
    }
}