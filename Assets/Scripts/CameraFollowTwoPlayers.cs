using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTwoPlayers : MonoBehaviour
{
    public Transform player1; // Reference to Player 1
    public Transform player2; // Reference to Player 2
    public Vector3 offset; // Camera's offset from the center point
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement
    private Quaternion fixedRotation;

    private Vector3 fixedPosition;

    private void Start() {
        fixedRotation = transform.rotation;
    }
    void LateUpdate()
    {
        // Calculate the center point between Player 1 and Player 2
        Vector3 centerPoint = (player1.position + player2.position) / 2;

        // Desired position of the camera based on center point + offset
        Vector3 desiredPosition = centerPoint + offset;
        Debug.Log(desiredPosition);

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Apply the new position to the camera
        transform.position = smoothedPosition;
        
        // Optionally, the camera can look at the center point if needed
        transform.rotation = fixedRotation;
    }
}
