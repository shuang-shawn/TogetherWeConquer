using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoComboManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public ComboInput startedCombo;
    public ComboInput endingCombo;

    public Combo currentCombo;
    public GameObject skillManagerObject; // temp holder for skill system
    private SkillManager skillManager;

    [SerializeField]
    private GameObject p1ComboWindow;
    [SerializeField]
    private GameObject p2ComboWindow;

    private void Start()
    {
        FindPlayers();
        skillManager = skillManagerObject.GetComponent<SkillManager>();

    }

    // Checks if the other player is currently performing a solo combo
    public bool IsOtherPlayerInSoloCombo(GameObject player) 
    {
        GameObject otherPlayer = (player == player1) ? player2 : player1;
        if (otherPlayer.activeInHierarchy) {
            if (player == player1)
            {
                return player2.GetComponentInChildren<ComboInput>().IsInSoloCombo();
            }
            else
            {
                return player1.GetComponentInChildren<ComboInput>().IsInSoloCombo();
            }
        } else
        {
            return true;
        }
      
    }

    // Handles logic for starting duo combos
    public void StartDuoCombo(Combo combo, GameObject player)
    {
        currentCombo = combo;
        AssignPlayerOrder(player);
        startedCombo.IsInDuoCombo(true);
        endingCombo.IsInDuoCombo(true);
        //if (player == player1)
        //{
        //    p2ComboWindow.SetActive(false);
        //} else
        //{
        //    p1ComboWindow.SetActive(false);
        //}
        var (firstHalf, secondHalf) = SplitCombo(combo.GetComboSequence());
        startedCombo.StartCombo(combo, firstHalf, true);
        endingCombo.ToggleInput(false);
        endingCombo.StartCombo(combo, secondHalf, false);
    }

    // Handles logic for when either player completed their half of the duo combo
    public void CompletedHalf(GameObject player, float remainingTime, bool abrupt)
    {
        if (player == startedCombo.transform.parent.gameObject && !abrupt)  // If initial player finishes, allow other player to begin
        {
            startedCombo.ToggleInput(false);
            endingCombo.ToggleInput(true);
            endingCombo.StartTimer(remainingTime);
            skillManager.CastFirstHalfDuoSkill(currentCombo.GetComboSkill(), player.tag, currentCombo.GetComboType());
            return;
        }
        else if (player == startedCombo.transform.parent.gameObject && abrupt) // If initial players stops combo unexpecetedly, force other play to end combo
        {
            endingCombo.RestartCombo();
            startedCombo.ToggleInput(true);
            endingCombo.ToggleInput(true);
            //p1ComboWindow.SetActive(true);
            //p2ComboWindow.SetActive(true);
            return;
        }
        if (!abrupt)
        {
            Debug.Log("Duo Skill ended with: " + player.gameObject.name);
            skillManager.CastSkill(currentCombo.GetComboSkill(), player.tag, currentCombo.GetComboType());
        }
        
        currentCombo = null;
        startedCombo.ToggleInput(true);
        endingCombo.ToggleInput(true);
        //p1ComboWindow.SetActive(true);
        //p2ComboWindow.SetActive(true);
    }

    public void ForceResetDuoCombo()
    {
        if (startedCombo != null && endingCombo != null)
        {
            startedCombo.RestartCombo();
            endingCombo.RestartCombo();
            currentCombo = null;
            startedCombo.ToggleInput(true);
            endingCombo.ToggleInput(true);
        }
    }



    private void FindPlayers()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }
    
    // Determines which player initiated the duo combo and which player is on the receiving end
    private void AssignPlayerOrder(GameObject player)
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
    }

    // Utility function that splits the combo into two halves
    private (List<KeyCode>, List<KeyCode>) SplitCombo(List<KeyCode> combo)
    {
        int midPoint = combo.Count / 2;

        List<KeyCode> firstHalf = combo.GetRange(0, midPoint);
        List<KeyCode> secondHalf = combo.GetRange(midPoint, combo.Count - midPoint);

        return (firstHalf, secondHalf);
    }

}
