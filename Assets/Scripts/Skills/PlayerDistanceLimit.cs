using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceLimit : MonoBehaviour
{   public GameObject player1;
    public GameObject player2;
    public float maxDistance = 10f; // Maximum distance allowed
    public float pullBackForce = 200f; // The force to apply when pulling players back
    public bool SmoothPullBack = false;
    public bool TetherMode = false;

    private Rigidbody rb1;
    private Rigidbody rb2;

    private PlayerMovement player1Movement;
    private PlayerMovement player2Movement;

    // Start is called before the first frame update
    void Start()
    {
        rb1 = player1.GetComponent<Rigidbody>();
        rb2 = player2.GetComponent<Rigidbody>();
        player1Movement = player1.GetComponent<PlayerMovement>();
        player2Movement = player2.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TetherMode) {
            maxDistance = 3.5f;
            pullBackForce = 200f;
        } else {
            maxDistance = 10f;
            pullBackForce = 150f;
        }
        // Calculate the current distance between the players
        float currentDistance = Vector3.Distance(player1.transform.position, player2.transform.position);

        // If they exceed the max distance, stop movement in that direction
        if (currentDistance > maxDistance)
        {
            if (!SmoothPullBack){
            player1Movement.canMove = false;
            player2Movement.canMove = false;

            }
            
            Vector3 direction = (player1.transform.position - player2.transform.position).normalized;

            // // Project player velocities onto the direction vector
            // float relativeVelocity1 = Vector3.Dot(rb1.velocity, direction);
            // float relativeVelocity2 = Vector3.Dot(rb2.velocity, -direction);

            // // If player 1 is moving away from player 2, stop that movement
            // if (relativeVelocity1 > 0)
            // {
            //     rb1.velocity -= direction * relativeVelocity1;
            // }

            // // If player 2 is moving away from player 1, stop that movement
            // if (relativeVelocity2 > 0)
            // {
            //     rb2.velocity -= direction * relativeVelocity2;
            // }

            // Apply a smooth force to pull them back within the allowed range
            Vector3 pullBackDirection = direction * (currentDistance - maxDistance);
            rb1.AddForce(-pullBackDirection.normalized * pullBackForce);
            rb2.AddForce(pullBackDirection.normalized * pullBackForce);
        } else {
            if (!SmoothPullBack){
                player1Movement.canMove = true;
                player2Movement.canMove = true;

            }
        }
    }
}
