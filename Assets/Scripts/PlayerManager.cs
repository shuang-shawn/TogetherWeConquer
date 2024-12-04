using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public ParticleSystem hurtParticlesPrefab;
    public ParticleSystem deathParticlesPrefab;
    public Crosshair crosshair;
    public float flashDuration = 0.6f;
    public float flashInterval = 0.05f;

    [SerializeField]
    private GameObject tombstone;

    private GameObject duoComboManager;
    [SerializeField]
    private GameObject timer;
    // Start is called before the first frame update

    private SpriteRenderer spriteRenderer;
    private Material material;

    void Start()
    {
        currentHealth = maxHealth;
        duoComboManager = GameObject.FindGameObjectWithTag("DuoComboManager");
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null) {
            material = spriteRenderer.material;
        }
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

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage!");

        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        FlashHurt();
        // SpawnHurtParticles();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healing)
    {
        UnityEngine.Debug.Log(healing);
        UnityEngine.Debug.Log(Math.Min(currentHealth + healing, maxHealth));

        if (currentHealth != maxHealth)
        {
            currentHealth = Math.Min(currentHealth + healing, maxHealth);

            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    void Die()
    {
        ExitComboLoop();
        SpawnDeathParticles();
      
        Debug.Log(gameObject.name + " has died!");

        // Add death handling here (destroy, play animation, etc.)
        // Destroy(gameObject);
        
        gameObject.SetActive(false);
        SpawnTombstone();
    }
    public bool IsDead() {
        return currentHealth <= 0;
    }

    private void FlashHurt() {
        if (material != null) {
            StartCoroutine(FlashRoutine(flashDuration, flashInterval));
        }
    }

    private IEnumerator FlashRoutine(float duration, float interval)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Toggle the tint on
            material.SetFloat("_isFlashing", 1f);
            yield return new WaitForSeconds(interval / 2);

            // Toggle the tint off
            material.SetFloat("_isFlashing", 0f);
            yield return new WaitForSeconds(interval / 2);

            elapsedTime += interval;
        }

        // Ensure tint is off at the end
        material.SetFloat("_UseTint", 0f);
    }

    private void SpawnHurtParticles()
    {
        ParticleSystem hurtParticles = Instantiate(hurtParticlesPrefab, gameObject.transform.position, Quaternion.identity);

        hurtParticles.transform.SetParent(gameObject.transform);

        hurtParticles.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f));

        hurtParticles.Play();

        Destroy(hurtParticles.gameObject, hurtParticles.main.duration + hurtParticles.main.startLifetime.constantMax);
    }

    private void SpawnDeathParticles()
    {
        ParticleSystem hurtParticles = Instantiate(deathParticlesPrefab, gameObject.transform.position, Quaternion.identity);

        hurtParticles.transform.Translate(new Vector3(0.0f, 2.0f, 0.0f));

        hurtParticles.Play();

        Destroy(hurtParticles.gameObject, hurtParticles.main.duration + hurtParticles.main.startLifetime.constantMax);
    }

    private void ExitComboLoop()
    {
        duoComboManager.GetComponent<DuoComboManager>()?.ForceResetDuoCombo();
        GetComponentInChildren<ComboInput>()?.RestartCombo();
        timer?.GetComponent<ComboTimer>().ResetTimer();
    }

    private void SpawnTombstone()
    {
        GameObject spawnedTombstone = Instantiate(tombstone, gameObject.transform.position, Quaternion.identity);
        Vector3 position = spawnedTombstone.transform.position;
        position.y -= 0.33f;
        spawnedTombstone.transform.position = position;
        spawnedTombstone.GetComponent<ReviveLogic>().SetPlayerInfo(gameObject.tag, gameObject);
        spawnedTombstone.GetComponent<ReviveLogic>().enabled = true;
    }
}
