using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement:MonoBehaviour
{
    //create private internal references
    private InputActions inputActions;
    private InputAction movement;
    public float movementSpeed = 5f;
    public int playerNo = 1;
    public Vector3 currentMoveDirection = Vector3.zero;
    public Vector3 lastDirectionX = Vector3.right;
    private Vector3 previousDirectionX = Vector3.right;

    public bool canMove = true;

    private SpriteRenderer sr;
    private Animator animator;

    Rigidbody rb;

    public event Action<Vector3, string> OnDirectionChange;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>(); //get rigidbody, responsible for enabling collision with other colliders
        inputActions = new InputActions(); //create new InputActions
    }

    //called when script enabled
    private void OnEnable()
    {
        if (playerNo == 1)
        {
            movement = inputActions.Player1.Movement; //get reference to movement action
        }
        else if (playerNo == 2)
            {
            movement = inputActions.Player2.Movement; //get reference to movement action
        }

        movement.Enable();


    }

    //called when script disabled
    private void OnDisable()
    {
        movement.Disable();

    }

    //called every physics update
    private void FixedUpdate()
    {
        Vector2 v2P1 = movement.ReadValue<Vector2>(); //extract 2d input data
        Vector3 v3P1 = new Vector3(v2P1.x, 0, v2P1.y); //convert to 3d space
        currentMoveDirection = v3P1.normalized;
        // save last facing direction for dashing while standing still
        if (currentMoveDirection != Vector3.zero) {
            if (currentMoveDirection.x != 0) {
                lastDirectionX = new Vector3(currentMoveDirection.x, 0, 0).normalized;
                // If direction changed, trigger the event
                if (lastDirectionX != previousDirectionX && OnDirectionChange != null)
                {
                    OnDirectionChange?.Invoke(lastDirectionX, gameObject.tag); // Trigger event
                    previousDirectionX = lastDirectionX; // Update previous direction
                }
            }
        }

        //transform.Translate(v3); //moves transform, ignoring physics

        animator.SetFloat("XInput", v3P1.x);
        animator.SetFloat("ZInput", v3P1.z);
        if (v3P1.x < 0) {
            sr.flipX = true;
        } else if (v3P1.x > 0) {
            sr.flipX = false;
        }
        if (canMove) {
            rb.velocity = v3P1 *movementSpeed;
        }
        // rb.AddForce(v3P1, ForceMode.VelocityChange); //apply instant physics force, ignoring mass

    }


}
