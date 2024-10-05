using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    //create private internal references
    private InputActions inputActions;
    private InputAction movementP1;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); //get rigidbody, responsible for enabling collision with other colliders
        inputActions = new InputActions(); //create new InputActions
    }

    //called when script enabled
    private void OnEnable()
    {
        movementP1 = inputActions.Player1.MovementP1; //get reference to movement action
        movementP1.Enable();

    }

    //called when script disabled
    private void OnDisable()
    {
        movementP1.Disable();

    }

    //called every physics update
    private void FixedUpdate()
    {
        Vector2 v2P1 = movementP1.ReadValue<Vector2>(); //extract 2d input data
        Vector3 v3P1 = new Vector3(v2P1.x, 0, v2P1.y); //convert to 3d space

        //transform.Translate(v3); //moves transform, ignoring physics
        rb.AddForce(v3P1, ForceMode.VelocityChange); //apply instant physics force, ignoring mass

    }
}
