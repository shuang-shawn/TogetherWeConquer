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
}