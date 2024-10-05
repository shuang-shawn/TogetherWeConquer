using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    //create private internal references
    private InputActions inputActionsP2;
    private InputAction movementP2;

    Rigidbody rbP2;

    private void Awake()
    {
        rbP2 = GetComponent<Rigidbody>(); //get rigidbody, responsible for enabling collision with other colliders
        inputActionsP2 = new InputActions(); //create new InputActions
    }

    //called when script enabled
    private void OnEnable()
    {
        movementP2 = inputActionsP2.Player2.MovementP2; //get reference to movement action
        movementP2.Enable();

    }

    //called when script disabled
    private void OnDisable()
    {
        movementP2.Disable();

    }

    //called every physics update
    private void FixedUpdate()
    {
        Vector2 v2P2 = movementP2.ReadValue<Vector2>(); //extract 2d input data
        Vector3 v3P2 = new Vector3(v2P2.x, 0, v2P2.y); //convert to 3d space

        //transform.Translate(v3); //moves transform, ignoring physics
        rbP2.AddForce(v3P2, ForceMode.VelocityChange); //apply instant physics force, ignoring mass

    }
}
