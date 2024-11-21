using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;

    public GameObject forceFieldPrefab; // Reference to the prefab
    private GameObject activeForceField1; // Currently active forcefield for player 1
    private GameObject activeForceField2; // Currently active forcefield for player 2
    public float heightOffset = 0.5f; // Adjust this value to raise the forcefield

    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
    }

    void Update()
    {
        // If forcefields are active, make them follow the respective players with an offset
        if (activeForceField1 && player1)
        {
            Vector3 offsetPosition1 = new Vector3(player1.transform.position.x, player1.transform.position.y + heightOffset, player1.transform.position.z);
            activeForceField1.transform.position = offsetPosition1;
        }

        if (activeForceField2 && player2)
        {
            Vector3 offsetPosition2 = new Vector3(player2.transform.position.x, player2.transform.position.y + heightOffset, player2.transform.position.z);
            activeForceField2.transform.position = offsetPosition2;
        }
    }

    public void CastForceField(int playerNum)
    {
        GameObject castingPlayer;
        GameObject activeForceField;

        if (playerNum == 1)
        {
            castingPlayer = player1;
            activeForceField = activeForceField1;
        }
        else if (playerNum == 2)
        {
            castingPlayer = player2;
            activeForceField = activeForceField2;
        }
        else
        {
            Debug.LogError("Invalid player number!");
            return;
        }

        if (castingPlayer == null)
        {
            Debug.LogError("Player is not assigned in the inspector!");
            return;
        }

        if (forceFieldPrefab == null)
        {
            Debug.LogError("ForceField prefab is not assigned!");
            return;
        }

        // If there's an existing forcefield, destroy it first
        if (activeForceField != null)
        {
            Destroy(activeForceField);
            Debug.Log("Previous forcefield destroyed.");
        }

        // Instantiate the forcefield prefab around the player with an offset
        Vector3 offsetPosition = new Vector3(castingPlayer.transform.position.x, castingPlayer.transform.position.y + heightOffset, castingPlayer.transform.position.z);
        activeForceField = Instantiate(forceFieldPrefab, offsetPosition, Quaternion.identity);
        activeForceField.transform.parent = castingPlayer.transform;

        // Assign the active forcefield back to the correct player variable
        if (playerNum == 1)
        {
            activeForceField1 = activeForceField;
        }
        else if (playerNum == 2)
        {
            activeForceField2 = activeForceField;
        }

        // Debug log to confirm forcefield instantiation
        Debug.Log("ForceField instantiated successfully for Player " + playerNum);

        SkillCollisionHandler skillCollisionHandler = activeForceField.GetComponent<SkillCollisionHandler>();
        if (skillCollisionHandler == null)
        {
            Debug.LogError("SkillCollisionHandler component not found on ForceField prefab!");
            return;
        }

        skillCollisionHandler.skillDamage = 0;
        skillCollisionHandler.oneTimeUse = true;
    }
}
