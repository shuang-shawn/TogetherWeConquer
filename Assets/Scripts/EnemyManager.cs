using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int damageAmount = 0;
    public int maxHealth = 100;
    public int maxBossHealth = 250;
    public int currentHealth = 0;

    public HealthBar healthBar;
    public ParticleSystem hurtParticlesPrefab;
    public ParticleSystem slowParticlesPrefab;
    public Animator animator;

    public bool slowed = false;
    private SlimeBoss slimeBoss;
    private TurtleBoss turtleBoss;
    
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

        slimeBoss = GetComponent<SlimeBoss>();
        turtleBoss = GetComponent<TurtleBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            TakeDamage(100);
        }
    }

    // Needed for slime split mechanic
    public void setMaxHealth(int newMaxHealth){
        // maxHealth = newMaxHealth;
        currentHealth = newMaxHealth;
    }

    public int getMaxBossHealth(){
        return maxBossHealth;
    }

    public void TakeDamage(int damage, Vector3 hitPosition=default) {
        if (turtleBoss && hitPosition != default) {
            UnityEngine.Debug.Log("this is a turtle");
            float damageFactor = turtleBoss.CalculateDamageFactor(hitPosition);
            currentHealth -=  Mathf.FloorToInt(damageFactor * damage);
            UnityEngine.Debug.Log(gameObject.name + " took " + damageFactor * damage + " modified damage!");

        } else {
            currentHealth -= damage;
            UnityEngine.Debug.Log(gameObject.name + " took " + damage + " damage!");
        }

        if (gameObject.tag == "boss")
        {
            healthBar.UpdateHealthBar(currentHealth, maxBossHealth);
            if (currentHealth / maxHealth < 0.5f && slimeBoss != null) {
                slimeBoss.speedPercent = 4;
            }
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
        //Destroy this mob
        if(gameObject.tag == "mob") {
            Destroy(gameObject);
        } else {            

            if(gameObject.name == "SlimeBoss") {
                slimeBoss.IsDead = true;
            }
            // Add death handling here (destroy, play animation, etc.)
            // bossScript.IsDead = true;
            animator.SetTrigger("Die");
        }
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
        float originalSpeed = slimeBoss.speedPercent;
        slimeBoss.speedPercent = slowFactor;
        slowed = true;

        SlowParticles(slowTime);

        // Wait for the specified duration
        yield return new WaitForSecondsRealtime(slowTime); // Use WaitForSecondsRealtime to ignore the time scale

        // Reset the time scale to the original value
        slimeBoss.speedPercent = originalSpeed;
        slowed = false;
    }

    private void SlowParticles(float duration)
    {
        ParticleSystem slowParticles = Instantiate(slowParticlesPrefab, gameObject.transform.position, Quaternion.identity);

        slowParticles.transform.SetParent(gameObject.transform);

        Vector3 collider = GetComponent<Collider>().bounds.size;
        float size = Mathf.Max(collider.x, collider.y, collider.z);
        float radius = size * 2f;

        UnityEngine.Debug.Log("Size " + size);
        UnityEngine.Debug.Log("Radius " + radius);

        var slowMain = slowParticles.main;
        var slowShape = slowParticles.shape;
        slowMain.duration = duration;
        slowMain.startSize = size;
        slowShape.radius = radius;

        slowParticles.Play();

        Destroy(slowParticles.gameObject, slowParticles.main.duration + slowParticles.main.startLifetime.constantMax);
    }
}
