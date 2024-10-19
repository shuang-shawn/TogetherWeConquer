using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ComboData : MonoBehaviour
{
    /** First two input listeners for initiating combo. **/
    public KeyCode firstInput = KeyCode.None;
    public KeyCode secondInput = KeyCode.None;

    /** The current combo in play. **/
    public List<KeyCode> currentCombo;

    /** The current sequence index in the combo list. **/
    public int currentSequenceIndex;

    /** Current combo object. **/
    public Combo currentComboObject;

    /** Last key pressed by the player. **/
    public KeyCode lastKeyPressed;

    /** List of wrong mistake keys pressed in current combo. **/
    public List<KeyCode> mistakeKeysPressed;

    /** Total mistake count in current combo. **/
    public int mistakeCount;

    /** Tracks the position of mistakes in the current combo. **/
    public List<string> mistakeOrder;

    /** Indicates if combo was ended abrupting (Ex. cancel, time ran out) **/
    public bool isAbrupt;

    /** Combo timer. **/
    public float timerVal;

    /** Toggle whether a player is in duo combo selection mode. **/
    public bool duoToggle = false;

    /** Indicates whether a player is currently in a solo combo. **/
    public bool isInSoloCombo;

    /** Indicates whether a player is currently in a duo combo state. **/
    public bool isInDuoCombo;

    /** Enables whether a player can input combos keys.  **/
    public bool isInputEnabled = true;

    //void PrintSummary()
    //{
    //    string summary = $"First Input: {firstInput}\n" +
    //                     $"Second Input: {secondInput}\n" +
    //                     $"Combo Selected: {string.Join(", ", currentCombo)}\n" +
    //                     $"Total Mistake Count: {mistakeCount}\n" +
    //                     $"Mistakes: {string.Join(", ", mistakeKeysPressed)}" +
    //                     $"Order of wrong inputs: {string.Join(", ", mistakeOrder)}\n" +
    //                     $"Remaining Time: {timerVal}s";

    //    Debug.Log("--Click to view summary--\n" + summary);
    //}
    public void ResetData()
    {
        //PrintSummary();

        firstInput = KeyCode.None;
        secondInput = KeyCode.None;
        currentCombo = new List<KeyCode>();
        currentComboObject = null;
        currentSequenceIndex = 0;
        lastKeyPressed = KeyCode.None;
        mistakeKeysPressed = new List<KeyCode>();
        mistakeCount = 0;
        mistakeOrder.Clear();
        timerVal = 0;
        isAbrupt = false;
        duoToggle = false;
        isInSoloCombo = false;
        isInDuoCombo = false;
    }
}
