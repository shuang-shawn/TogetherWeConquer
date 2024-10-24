using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDash : MonoBehaviour
{
    public float dashDistance = 50f;      // The distance the character will dash
    public float dashSpeed = 60f;        // The speed at which the character dashes
    public float dashCooldown = 1f;      // Cooldown time between dashes
    private GameObject player1;
    private GameObject player2;
    private GameObject castingPlayer;
    public ParticleSystem dashParticlesPrefab;
    // private bool isDashing = false;      // Tracks if the character is currently dashing
    private float dashTime = 0.2f;       // Time duration for the dash
    private float lastDashTime = -1f;    // Time when the last dash was executed
    private Rigidbody rb;
    private Vector3 lastMoveDirection = Vector3.right;  // Default to right (X-axis)
    private Vector3 currentMoveDirection = Vector3.zero;

    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");

        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        // {
        //     Debug.Log("dashing");
        //     Dash();
        // }
    }

    public void Dash(int playerNum)
    {
        if (playerNum == 1) {
            castingPlayer = player1;
        } else if (playerNum == 2) {
            castingPlayer = player2;
        }
        if (castingPlayer == null)
        {
            Debug.LogError("Player is not assigned in the inspector!");
            return;
        }

        rb = castingPlayer.GetComponent<Rigidbody>();
        playerMovement = castingPlayer.GetComponent<PlayerMovement>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the player!");
        }
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component is missing on the player!");
        }
        // rb = player.GetComponent<Rigidbody>();
        // playerMovement = player.GetComponent<PlayerMovement>();
        currentMoveDirection = playerMovement.currentMoveDirection;

        Debug.Log("dash");
        // Set the dash state and apply the force
        // isDashing = true;
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
        // isDashing = false;
        playerMovement.canMove = true;
    }

    private void SpawnDashParticles(Vector3 dashDirection)
    {
        ParticleSystem dashParticles = Instantiate(dashParticlesPrefab, castingPlayer.transform.position, Quaternion.identity);

        Vector3 oppositeDirection = -dashDirection;

        dashParticles.transform.rotation = Quaternion.LookRotation(oppositeDirection, Vector3.up);

        dashParticles.transform.SetParent(castingPlayer.transform);

        dashParticles.Play();

        Destroy(dashParticles.gameObject, dashParticles.main.duration + dashParticles.main.startLifetime.constantMax);
    }
}
