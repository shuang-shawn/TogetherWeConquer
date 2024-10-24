using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int damageAmount = 0;
    public int maxHealth = 100;
    public int maxBossHealth = 250;
    public int currentHealth = 0;

    public HealthBar healthBar;
    public ParticleSystem hurtParticlesPrefab;
    public Animator animator;
    public SlimeBoss bossScript;

    public bool slowed = false;
    
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

        bossScript = GetComponent<SlimeBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            TakeDamage(10);
        }
    }



    public void TakeDamage(int damage) {
        currentHealth -= damage;
        UnityEngine.Debug.Log(gameObject.name + " took " + damage + " damage!");

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
        UnityEngine.Debug.Log(gameObject.name + " has died!");
        // Add death handling here (destroy, play animation, etc.)
        animator.SetTrigger("Die");
    }

    public bool IsDead() {
        return currentHealth <= 0;
    }

    private void SpawnHurtParticles()
    {
        ParticleSystem hurtParticles = Instantiate(hurtParticlesPrefab, gameObject.transform.position, Quaternion.identity);

        hurtParticles.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f));

        hurtParticles.Play();

        Destroy(hurtParticles.gameObject, hurtParticles.main.duration + hurtParticles.main.startLifetime.constantMax);
    }

    public void Slow(float slowFactor, float slowTime) {
        UnityEngine.Debug.Log("slowing down " + gameObject.name);
        StartCoroutine(SlowDownForDuration(slowFactor, slowTime));

    }

    private IEnumerator SlowDownForDuration(float slowFactor, float slowTime)
    {
        float originalSpeed = bossScript.speedPercent;
        bossScript.speedPercent = slowFactor;
        slowed = true;

        // Wait for the specified duration
        yield return new WaitForSecondsRealtime(slowTime); // Use WaitForSecondsRealtime to ignore the time scale

        // Reset the time scale to the original value
        bossScript.speedPercent = originalSpeed;
        slowed = false;
    }

}
