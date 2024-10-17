using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_Skills : MonoBehaviour
{
    public float dashDistance = 50f;      // The distance the character will dash
    public float dashSpeed = 60f;        // The speed at which the character dashes
    public float dashCooldown = 1f;      // Cooldown time between dashes
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
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
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

        void Dash()
    {
        // Set the dash state and apply the force
        isDashing = true;
        lastDashTime = Time.time;
        playerMovement.canMove = false;


        // Calculate the dash direction based on the character's facing direction
        Vector3 dashDirection = playerMovement.currentMoveDirection != Vector3.zero ? playerMovement.currentMoveDirection : playerMovement.lastDirectionX;

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
}
