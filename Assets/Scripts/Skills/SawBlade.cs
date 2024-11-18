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
    public float heightOffset = 1.5f; // Adjust this value to raise the sawblade
    public float distanceOffset = 5f; // Adjust this value to control the radius

    public float rotationSpeed = 8f; // Further reduced speed for slower rotation
    public float duration = 5f;
    public int damage = 5;

    private float angle1 = 0f; // Angle for player 1's sawblade
    private float angle2 = 0f; // Angle for player 2's sawblade

    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
    }

    void FixedUpdate()
    {
        // If sawblades are active, make them rotate around the respective players with an offset
        if (activeSawblade1)
        {
            angle1 += rotationSpeed * Time.fixedDeltaTime;
            Vector3 offsetPosition1 = player1.transform.position + new Vector3(distanceOffset * Mathf.Cos(angle1), heightOffset, distanceOffset * Mathf.Sin(angle1));
            activeSawblade1.transform.position = offsetPosition1;
            activeSawblade1.transform.Rotate(Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
            //Debug.Log("Updating Sawblade for Player 1");
        }

        if (activeSawblade2)
        {
            angle2 += rotationSpeed * Time.fixedDeltaTime;
            Vector3 offsetPosition2 = player2.transform.position + new Vector3(distanceOffset * Mathf.Cos(angle2), heightOffset, distanceOffset * Mathf.Sin(angle2));
            activeSawblade2.transform.position = offsetPosition2;
            activeSawblade2.transform.Rotate(Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
            //Debug.Log("Updating Sawblade for Player 2");
        }
    }

    public void SpawnSawblades()
    {
        if (sawbladePrefab == null)
        {
            //Debug.LogError("Sawblade prefab is not assigned!");
            return;
        }

        // Destroy previous sawblades if they exist
        if (activeSawblade1 != null)
        {
            Destroy(activeSawblade1);
           // Debug.Log("Previous sawblade for player 1 destroyed.");
        }

        if (activeSawblade2 != null)
        {
            Destroy(activeSawblade2);
            //Debug.Log("Previous sawblade for player 2 destroyed.");
        }

        // Instantiate the sawblade prefab around player 1 with an offset
        Vector3 offsetPosition1 = player1.transform.position + new Vector3(distanceOffset, heightOffset, 0);
        activeSawblade1 = Instantiate(sawbladePrefab, offsetPosition1, Quaternion.Euler(90, 0, 0)); // Rotate by 90 degrees
        activeSawblade1.transform.parent = player1.transform;

        SkillCollisionHandler skillCollisionHandler1 = activeSawblade1.GetComponentInChildren<SkillCollisionHandler>();
        if (skillCollisionHandler1 == null)
        {
            //Debug.LogError("SkillCollisionHandler component not found on Sawblade prefab for player 1!");
            return;
        }
        skillCollisionHandler1.skillDamage = damage;
        skillCollisionHandler1.oneTimeUse = false;

        // Instantiate the sawblade prefab around player 2 with an offset
        Vector3 offsetPosition2 = player2.transform.position + new Vector3(distanceOffset, heightOffset, 0);
        activeSawblade2 = Instantiate(sawbladePrefab, offsetPosition2, Quaternion.Euler(90, 0, 0)); // Rotate by 90 degrees
        activeSawblade2.transform.parent = player2.transform;

        SkillCollisionHandler skillCollisionHandler2 = activeSawblade2.GetComponentInChildren<SkillCollisionHandler>();
        if (skillCollisionHandler2 == null)
        {
            //Debug.LogError("SkillCollisionHandler component not found on Sawblade prefab for player 2!");
            return;
        }
        skillCollisionHandler2.skillDamage = damage;
        skillCollisionHandler2.oneTimeUse = false;

        // Debug log to confirm sawblade instantiation
        //Debug.Log("Sawblades instantiated successfully.");

        // Set timers to destroy the sawblades after a duration
        Destroy(activeSawblade1, duration);
        Destroy(activeSawblade2, duration);
    }
}
