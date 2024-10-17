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
    // private ComboSkill comboSkill;

    public Combo(ComboType comboType, List<KeyCode> comboSequence)
    {
        this.comboSequence = comboSequence;
        this.comboType = comboType;
    }

    public List<KeyCode> GetComboSequence()
    {
        return comboSequence;
    }

    public ComboType GetComboType() {
        return comboType;
    }
}