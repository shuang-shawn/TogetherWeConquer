using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboList : MonoBehaviour
{
    // Static Solo Combo List
    public List<Combo> soloComboList = new List<Combo>()
    {
        new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "dash"),
        new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "dash"),
        // new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "dash"),
        new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow }, "dash"),
        new Combo(ComboType.Solo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "iceground"),

        new Combo(ComboType.Solo, new List<KeyCode> {KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow}, "swap")
    };

    // Static Duo Combo List
    // *Note* Duo Combos should have min 6 keys sequences or problems will happen
    public List<Combo> duoComboList = new List<Combo>()
    {
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "tether"),
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "tether"),
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow }, "tether"),
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow }, "tether"),
        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow }, "tether"),

        new Combo(ComboType.Duo, new List<KeyCode> { KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow, KeyCode.LeftArrow }, "fireRing")
    };
}
