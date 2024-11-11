using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;
    private GameObject castingPlayer;

    public GameObject forceFieldPrefab; // Reference to the prefab
    private GameObject activeForceField; // Currently active forcefield
    public float heightOffset = 0.5f; // Adjust this value to raise the forcefield

    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
    }

    void Update()
    {
        // If forcefield is active, make it follow the casting player with an offset
        if (activeForceField && castingPlayer)
        {
            Vector3 offsetPosition = new Vector3(castingPlayer.transform.position.x, castingPlayer.transform.position.y + heightOffset, castingPlayer.transform.position.z);
            activeForceField.transform.position = offsetPosition;
        }
    }

    public void CastForceField(int playerNum)
    {
        if (playerNum == 1)
        {
            castingPlayer = player1;
        }
        else if (playerNum == 2)
        {
            castingPlayer = player2;
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

        // Debug log to confirm forcefield instantiation
        Debug.Log("ForceField instantiated successfully.");

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
