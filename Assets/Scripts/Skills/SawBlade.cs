using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;

    public GameObject sawbladePrefab; // Reference to the prefab
    private GameObject activeSawblade1; // Currently active sawblade for player 1
    private GameObject activeSawblade2; // Currently active sawblade for player 2
    public float heightOffset = 0.5f; // Adjust this value to raise the sawblade
    public float rotationSpeed = 100f;
    public float duration = 5f;
    public int damage = 5;

    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
    }

    void Update()
    {
        // If sawblades are active, make them follow the respective players with an offset and rotate
        if (activeSawblade1)
        {
            Vector3 offsetPosition1 = new Vector3(player1.transform.position.x, player1.transform.position.y + heightOffset, player1.transform.position.z);
            activeSawblade1.transform.position = offsetPosition1;
            activeSawblade1.transform.RotateAround(player1.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (activeSawblade2)
        {
            Vector3 offsetPosition2 = new Vector3(player2.transform.position.x, player2.transform.position.y + heightOffset, player2.transform.position.z);
            activeSawblade2.transform.position = offsetPosition2;
            activeSawblade2.transform.RotateAround(player2.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void SpawnSawblades()
    {
        if (sawbladePrefab == null)
        {
            Debug.LogError("Sawblade prefab is not assigned!");
            return;
        }

        // If there's an existing sawblade for player 1, destroy it first
        if (activeSawblade1 != null)
        {
            Destroy(activeSawblade1);
            Debug.Log("Previous sawblade for player 1 destroyed.");
        }

        // If there's an existing sawblade for player 2, destroy it first
        if (activeSawblade2 != null)
        {
            Destroy(activeSawblade2);
            Debug.Log("Previous sawblade for player 2 destroyed.");
        }

        // Instantiate the sawblade prefab around player 1 with an offset
        Vector3 offsetPosition1 = new Vector3(player1.transform.position.x, player1.transform.position.y + heightOffset, player1.transform.position.z);
        activeSawblade1 = Instantiate(sawbladePrefab, offsetPosition1, Quaternion.identity);
        activeSawblade1.transform.parent = player1.transform;

        SkillCollisionHandler skillCollisionHandler1 = activeSawblade1.GetComponent<SkillCollisionHandler>();
        if (skillCollisionHandler1 == null)
        {
            Debug.LogError("SkillCollisionHandler component not found on Sawblade prefab for player 1!");
            return;
        }
        skillCollisionHandler1.skillDamage = damage;
        skillCollisionHandler1.oneTimeUse = true;

        // Instantiate the sawblade prefab around player 2 with an offset
        Vector3 offsetPosition2 = new Vector3(player2.transform.position.x, player2.transform.position.y + heightOffset, player2.transform.position.z);
        activeSawblade2 = Instantiate(sawbladePrefab, offsetPosition2, Quaternion.identity);
        activeSawblade2.transform.parent = player2.transform;

        SkillCollisionHandler skillCollisionHandler2 = activeSawblade2.GetComponent<SkillCollisionHandler>();
        if (skillCollisionHandler2 == null)
        {
            Debug.LogError("SkillCollisionHandler component not found on Sawblade prefab for player 2!");
            return;
        }
        skillCollisionHandler2.skillDamage = damage;
        skillCollisionHandler2.oneTimeUse = true;

        // Debug log to confirm sawblade instantiation
        Debug.Log("Sawblades instantiated successfully.");

        // Set timers to destroy the sawblades after a duration
        Destroy(activeSawblade1, duration);
        Destroy(activeSawblade2, duration);
    }
}
