using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class ComboList : MonoBehaviour
{
    private const string Player1Tag = "Player1";
    private const string Player2Tag = "Player2";

    // Master List of all Solo Combos
    public List<Combo> soloComboList = new List<Combo>();

    // Master List of all Duo Combos
    public List<Combo> duoComboList = new List<Combo>();

    public List<Combo> currentP1ComboList = new List<Combo>();
    public List<Combo> currentP2ComboList = new List<Combo>();

    private ComboWindowUI comboWindowUI;

    void Awake()
    {
        soloComboList = new List<Combo>()
        {
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "shadow", Resources.Load<Sprite>("Skill Icons/shadow"), "Places a shadow that you can teleport back to"),
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "stone", Resources.Load<Sprite>("Skill Icons/stone"), "Stone Skill"),
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "helpinghand", Resources.Load<Sprite>("Skill Icons/InvisSkillIcon"), "Invis Skill"),
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "iceground", Resources.Load<Sprite>("Skill Icons/ice"), "Ice Skill"),
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.DownArrow}, "swap", Resources.Load<Sprite>("Skill Icons/swap"), "Swap Skill"),
             new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow }, "forceField", Resources.Load<Sprite>("Skill Icons/ForcefieldSkill"), "Forcefield Skill")

        };

        duoComboList = new List<Combo>()
        {
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "tether", Resources.Load<Sprite>("Skill Icons/tether"), "Tether Skill"),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "arrowbarrage",  Resources.Load<Sprite>("Skill Icons/Arrow_0"), "Fires a barrage of arrows"),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "snipe", Resources.Load<Sprite>("Skill Icons/snipe"), "Snipe Skill"),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow }, "drain", Resources.Load<Sprite>("Skill Icons/drain"), "Drain Skill"),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow }, "fireRing", Resources.Load<Sprite>("Skill Icons/fireRing"), "Fire Ring Skill"),
            new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.UpArrow }, "sawblades", Resources.Load<Sprite>("Skill Icons/SawSkill"), "Saws Skill")
        };
        comboWindowUI = GameObject.FindGameObjectWithTag("ComboWindow")?.GetComponent<ComboWindowUI>();
    }

    public void AddP1SoloSkill(Combo newCombo)
    {
        currentP1ComboList.Add(newCombo);
        comboWindowUI.AddNewCombo(Player1Tag, newCombo);
        newCombo.LearnedP1();
    }

    public void AddP2SoloSkill(Combo newCombo)
    {
        currentP2ComboList.Add(newCombo);
        comboWindowUI.AddNewCombo(Player2Tag, newCombo);
        newCombo.LearnedP2();
    }

    public void AddDuoSkill(Combo newCombo)
    {
        currentP1ComboList.Add(newCombo);
        currentP2ComboList.Add(newCombo);
        comboWindowUI.AddNewCombo(Player1Tag, newCombo);
        comboWindowUI.AddNewCombo(Player2Tag, newCombo);
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "tether");
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "arrow_barrage", Resources.Load<Sprite>("Skill Icons/Arrow_0"));
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "tether");
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow }, "tether");
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "tether");
        //new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.UpArrow }, "sawblades", Resources.Load<Sprite>("Skill Icons/SawSkill"));

        }
}

