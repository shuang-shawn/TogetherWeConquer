using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class ComboList : MonoBehaviour
{
    // Static Solo Combo List
    public List<Combo> soloComboList = new List<Combo>();

    // Static Duo Combo List
    public List<Combo> duoComboList = new List<Combo>();

    public List<Combo> currentP1ComboList = new List<Combo>();
    public List<Combo> currentP2ComboList = new List<Combo>();

    void Awake()
    {
        soloComboList = new List<Combo>()
        {
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "dash"), 
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "shadow", Resources.Load<Sprite>("Skill Icons/shadow")),
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "stone", Resources.Load<Sprite>("Skill Icons/stone")),
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow }, "dash"),
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow }, "helpinghand", Resources.Load<Sprite>("Skill Icons/InvisSkillIcon")),
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "iceground", Resources.Load<Sprite>("Skill Icons/ice"))
        };

        duoComboList = new List<Combo>()
        {
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "tether", Resources.Load<Sprite>("Skill Icons/tether")),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "arrow_barrage",  Resources.Load<Sprite>("Skill Icons/Arrow_0")),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "tether", Resources.Load<Sprite>("Skill Icons/tether")),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow }, "tether", Resources.Load<Sprite>("Skill Icons/tether")),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "snipe", Resources.Load<Sprite>("Skill Icons/snipe")),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow }, "drain", Resources.Load<Sprite>("Skill Icons/drain"))
        };
    }

    public void AddToCurrentList(Combo combo)
    {
        currentP1ComboList.Add(combo);
        currentP2ComboList.Add(combo);
    }
}
