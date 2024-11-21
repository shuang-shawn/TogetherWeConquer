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
    private string description;
    private bool learnedP1;
    private bool learnedP2;
    private bool dummy;

    public Combo(ComboType comboType, List<KeyCode> comboSequence, string skillName, Sprite skillIcon = null, string skillDescription = "", bool dummy = false)
    {
        this.comboSequence = comboSequence;
        this.comboType = comboType;
        comboSkill = skillName;
        comboIcon = skillIcon;
        description = skillDescription;
        learnedP1 = false;
        learnedP2 = false;
        this.dummy = dummy;
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

    public string GetDescription()
    {
        return description;
    }

    public void LearnedP1()
    {
        learnedP1 = true;
    }

    public bool GetLearnedP1()
    {
        return learnedP1;
    }

    public void LearnedP2()
    {
        learnedP2 = true;
    }

    public bool GetLearnedP2()
    {
        return learnedP2;
    }

    public bool IsDummy()
    {
        return dummy;
    }

    public override string ToString()
    {
        // Build a string representation of the Combo object
        string comboSequenceStr = string.Join(", ", comboSequence);  // Converts the KeyCode list to a string
        string iconInfo = HasIcon() ? comboIcon.name : "No Icon";  // Checks if Combo has an icon
        return $"Combo Skill: {comboSkill}, Type: {comboType}, Sequence: [{comboSequenceStr}], Icon: {iconInfo}";
    }
}