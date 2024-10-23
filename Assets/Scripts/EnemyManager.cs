using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int damageAmount = 0;
    public int maxHealth = 100;
    public int maxBossHealth = 250;
    public int currentHealth = 0;

    public HealthBar healthBar;
    public ParticleSystem hurtParticlesPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "boss"){
            damageAmount = 25;
            currentHealth = maxBossHealth;
        } else if (gameObject.tag == "mob") {
            damageAmount = 10;
            currentHealth = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage!");

        if (gameObject.tag == "boss")
        {
            healthBar.UpdateHealthBar(currentHealth, maxBossHealth);
        } else if (gameObject.tag == "mob")
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        SpawnHurtParticles();

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

    private void SpawnHurtParticles()
    {
        ParticleSystem hurtParticles = Instantiate(hurtParticlesPrefab, gameObject.transform.position, Quaternion.identity);

        hurtParticles.transform.SetParent(gameObject.transform);

        hurtParticles.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f));

        hurtParticles.Play();

        Destroy(hurtParticles.gameObject, hurtParticles.main.duration + hurtParticles.main.startLifetime.constantMax);
    }
}
