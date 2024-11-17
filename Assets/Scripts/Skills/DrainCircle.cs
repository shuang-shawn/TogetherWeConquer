using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainCircle : MonoBehaviour
{
    public int damage = 1;
    public int healing = 1;

    public bool drain = true;

    private List<GameObject> enemyTracker = new List<GameObject>();

    void Start()
    {
        StartCoroutine(Drain());
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is tagged as "enemy" or "boss"
        if ((other.CompareTag("mob") || other.CompareTag("boss")) && !enemyTracker.Contains(other.gameObject))
        {
            enemyTracker.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove enemy from list if they exit the radius
        if (enemyTracker.Contains(other.gameObject))
        {
            enemyTracker.Remove(other.gameObject);
        }
    }

    public IEnumerator Drain()
    {
        while (drain)
        {
            foreach (var enemy in enemyTracker)
            {
                enemy.GetComponent<EnemyManager>()?.TakeDamage(damage);
            }

            if (enemyTracker.Count > 0)
            {
                HealPlayer(healing * enemyTracker.Count);
            }

            yield return new WaitForSeconds(0.2f); // Apply effect every 0.2 seconds
        }

        // Clear the list and reset drain state for next use
        enemyTracker.Clear();
        drain = false;
    }

    void HealPlayer(int amount)
    {
        PlayerManager playerManager = transform.parent.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.Heal(amount);
        }
    }
}
