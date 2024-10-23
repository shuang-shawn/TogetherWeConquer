using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth; 
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision detected");
        Debug.Log(collision.gameObject.name);
        // Check if the object that collided with this one has an "Attacker" component
        EnemyManager attacker = collision.gameObject.GetComponent<EnemyManager>();
        if (attacker != null)
        {
            TakeDamage(attacker.damageAmount); // Apply damage based on the attacker's damage
        }
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage!");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        // Add death handling here (destroy, play animation, etc.)
        Destroy(gameObject);
    }


}
