using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{

    private GameObject player1;
    private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
    }

    // Update is called once per frame
    public void CastSwap() {

        Debug.Log("Swapping Positions");

        Rigidbody player1RigidBody = player1.GetComponent<Rigidbody>();
        Rigidbody player2RigidBody = player2.GetComponent<Rigidbody>();

        Vector3 tempPosition = player1RigidBody.position;

        player1RigidBody.position = player2RigidBody.position;
        player2RigidBody.position = tempPosition;

        // Vector3 tempPosition = player1.transform.position;

        // player1.transform.position = player2.transform.position;
        // player2.transform.position = tempPosition;
    }
}
