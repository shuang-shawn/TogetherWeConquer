using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoComboManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public ComboInput startedCombo;
    public ComboInput endingCombo;
    //public bool 
    private void Start()
    {
        FindPlayers();
    }
    public void StartDuoCombo(List<KeyCode> combo, GameObject player)
    {
        // Check which player initiated the combo
        if (player == player1)
        {
            startedCombo = player1?.GetComponentInChildren<ComboInput>();
            endingCombo = player2?.GetComponentInChildren<ComboInput>();
            Debug.Log("Player 1 initiated the combo!");
        }
        else if (player == player2)
        {
            startedCombo = player2?.GetComponentInChildren<ComboInput>();
            endingCombo = player1?.GetComponentInChildren<ComboInput>();
            Debug.Log("Player 2 initiated the combo!");
        }
        startedCombo.IsInDuoCombo(true);
        endingCombo.IsInDuoCombo(true);
        var (firstHalf, secondHalf) = SplitCombo(combo);
        startedCombo.StartCombo(firstHalf, true);
        endingCombo.ToggleInput(false);
        endingCombo.StartCombo(secondHalf, false);
    }

    public void CompletedHalf(GameObject player, float remainingTime)
    {
        if (player == startedCombo.transform.parent.gameObject)
        {
            startedCombo.ToggleInput(false);
            endingCombo.ToggleInput(true);
            endingCombo.StartTimer(remainingTime);
        }
        else if (player == endingCombo.transform.parent.gameObject)
        {
            startedCombo.ToggleInput(true);
            endingCombo.ToggleInput(true);
            startedCombo.IsInDuoCombo(false);
            endingCombo.IsInDuoCombo(false);
        }
    }
    private void FindPlayers()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    // Utility function that splits combo into two halves
    private (List<KeyCode>, List<KeyCode>) SplitCombo(List<KeyCode> combo)
    {
        int midPoint = combo.Count / 2;

        List<KeyCode> firstHalf = combo.GetRange(0, midPoint);
        List<KeyCode> secondHalf = combo.GetRange(midPoint, combo.Count - midPoint);

        return (firstHalf, secondHalf);
    }

}
