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

    private SpriteRenderer sr;
    private Animator animator;

    Rigidbody rb;

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

        //transform.Translate(v3); //moves transform, ignoring physics

        animator.SetFloat("XInput", v3P1.x);
        animator.SetFloat("ZInput", v3P1.z);
        if (v3P1.x < 0) {
            sr.flipX = true;
        } else if (v3P1.x > 0) {
            sr.flipX = false;
        }
        rb.velocity = v3P1 *movementSpeed;
        // rb.AddForce(v3P1, ForceMode.VelocityChange); //apply instant physics force, ignoring mass

    }
}
