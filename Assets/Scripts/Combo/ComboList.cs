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
            new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "dash", Resources.Load<Sprite>("Skill Icons/dash")), 
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
        comboWindowUI = GameObject.FindGameObjectWithTag("ComboWindow")?.GetComponent<ComboWindowUI>();
    }

    // For testing purposes, will remove later
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Pick a random solo combo
            Combo randomSoloCombo = soloComboList[Random.Range(0, soloComboList.Count)];

            // Pick a random duo combo
            Combo randomDuoCombo = duoComboList[Random.Range(0, duoComboList.Count)];

            // Test the three functions
            AddP1SoloSkill(randomSoloCombo);
            AddP2SoloSkill(randomSoloCombo);
            AddDuoSkill(randomDuoCombo);

            Debug.Log("Random solo and duo combos added to Player 1 and Player 2.");
        }
    }

    public void AddP1SoloSkill(Combo newCombo)
    {
        currentP1ComboList.Add(newCombo);
        comboWindowUI.AddNewCombo(Player1Tag, newCombo);
    }

    public void AddP2SoloSkill(Combo newCombo)
    {
        currentP2ComboList.Add(newCombo);
        comboWindowUI.AddNewCombo(Player2Tag, newCombo);
    }

    public void AddDuoSkill(Combo newCombo)
    {
        currentP1ComboList.Add(newCombo);
        currentP2ComboList.Add(newCombo);
        comboWindowUI.AddNewCombo(Player1Tag, newCombo);
        comboWindowUI.AddNewCombo(Player2Tag, newCombo);
    }
}
