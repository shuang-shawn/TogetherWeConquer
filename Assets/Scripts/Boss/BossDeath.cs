using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public GameObject portalPrefab; // Reference to the portal prefab

    void Die()
    {
        // Log that the Die() method is called
        Debug.Log("Die() method called. Destroying boss.");

        // Get the boss position before destroying it
        Vector3 bossPosition = transform.parent.position;

        // Destroy the boss
        Destroy(transform.parent.gameObject);

        // Log the boss position
        Debug.Log("Boss position: " + bossPosition);

        // Instantiate the portal immediately for debugging
        if (portalPrefab != null)
        {
            GameObject portal = Instantiate(portalPrefab, bossPosition, Quaternion.identity);
            Debug.Log("Portal instantiated immediately at position: " + bossPosition);
            Debug.Log("Portal instance: " + portal.name);
        }
        else
        {
            Debug.LogError("Portal prefab is not assigned!");
        }
    }
}
