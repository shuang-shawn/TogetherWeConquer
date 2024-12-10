using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollisionHandler : MonoBehaviour
{
    public int skillDamage = 0;
    public float slowFactor = 1f;
    public float slowTime = 0f;
    public bool oneTimeUse = false;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug log to check for collisions
        // Debug.Log("Collision detected with: " + other.gameObject.name);

        // Ensure the collision is with an enemy
        if (other.gameObject.CompareTag("boss") || other.gameObject.CompareTag("mob")) // or "mob"
        {
            EnemyManager target = other.gameObject.GetComponent<EnemyManager>();
            if (target != null && !other.isTrigger)
            {
                // Log enemy hit
                // Debug.Log("Enemy hit! Taking damage.");
                Vector3 hitPosition = other.ClosestPoint(transform.position);
                target.TakeDamage(skillDamage, hitPosition);

                if (slowFactor != 1)
                {
                    target.Slow(slowFactor, slowTime);
                }

                if (oneTimeUse)
                {
                    // Destroy the forcefield
                    Destroy(gameObject);
                    // Log destruction
                    Debug.Log("Sawblade destroyed after one-time use.");
                }
            }
            else
            {
                Debug.LogWarning("EnemyManager component not found on the enemy.");
            }
        }
        else
        {
            // Debug.Log("Collision with non-enemy object.");
        }
    }
}
