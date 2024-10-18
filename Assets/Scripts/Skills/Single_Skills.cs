using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_Skills : MonoBehaviour
{
    public float dashDistance = 50f;      // The distance the character will dash
    public float dashSpeed = 60f;        // The speed at which the character dashes
    public float dashCooldown = 1f;      // Cooldown time between dashes
    public GameObject player;
    public ParticleSystem dashParticlesPrefab;
    private bool isDashing = false;      // Tracks if the character is currently dashing
    private float dashTime = 0.2f;       // Time duration for the dash
    private float lastDashTime = -1f;    // Time when the last dash was executed
    private Rigidbody rb;
    private Vector3 lastMoveDirection = Vector3.right;  // Default to right (X-axis)
    private Vector3 currentMoveDirection = Vector3.zero;

    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        playerMovement = player.GetComponent<PlayerMovement>();
        currentMoveDirection = playerMovement.currentMoveDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            Debug.Log("dashing");
            Dash();
        }
    }

    public void Dash()
    {
        Debug.Log("dash");
        // Set the dash state and apply the force
        isDashing = true;
        lastDashTime = Time.time;
        playerMovement.canMove = false;


        // Calculate the dash direction based on the character's facing direction
        Vector3 dashDirection = playerMovement.currentMoveDirection != Vector3.zero ? playerMovement.currentMoveDirection : playerMovement.lastDirectionX;

        SpawnDashParticles(dashDirection);

        // Push the player forward for a short time using a Coroutine to handle duration
        StartCoroutine(DashEffect(dashDirection));
    }

    IEnumerator DashEffect(Vector3 direction)
    {
        float startTime = Time.time;

        // Apply the dash for a set duration
        while (Time.time < startTime + dashTime)
        {
            // Move the character in the dash direction
            // rb.velocity = direction * dashSpeed;
            // Apply the velocity based on time progression (smooth stop)
            float t = (Time.time - startTime) / dashTime;
            rb.velocity = direction * Mathf.Lerp(dashSpeed, 0, t);
            yield return null; // Wait for the next frame
        }

        // Reset the velocity after dashing
        rb.velocity = Vector3.zero;
        isDashing = false;
        playerMovement.canMove = true;
    }

    private void SpawnDashParticles(Vector3 dashDirection)
    {
        // Instantiate the particle system at the player's position
        ParticleSystem dashParticles = Instantiate(dashParticlesPrefab, player.transform.position, Quaternion.identity);

        // Calculate the opposite direction for the particle effect (invert dash direction)
        Vector3 oppositeDirection = -dashDirection;

        // Use LookRotation to ensure the particle system faces the opposite direction
        // Vector3 up = Vector3.up ensures that the particle system has an up vector to align correctly
        dashParticles.transform.rotation = Quaternion.LookRotation(oppositeDirection, Vector3.up);

        // Make the particle system a child of the player so it moves with them
        dashParticles.transform.SetParent(player.transform);

        dashParticles.Play();

        // Destroy the particle system after a short duration (optional)
        Destroy(dashParticles.gameObject, dashParticles.main.duration + dashParticles.main.startLifetime.constantMax);
    }

}
