using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    private GameObject playerObj = null;

    private GameObject closestPlayerObj = null;
    private float speed = 2.0f;
    private float minDist = 1f;

    private void findClosestPlayer(){
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

        float player1Distance = Vector3.Distance(transform.position, player1.transform.position);
        float player2Distance = Vector3.Distance(transform.position, player2.transform.position);

        if(player2Distance > player1Distance){
            closestPlayerObj = player1;
        } else {
            closestPlayerObj = player2;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null){
            playerObj = GameObject.FindGameObjectWithTag("Player1");
        }
        findClosestPlayer();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(playerObj.transform.position);
        findClosestPlayer();
        
        transform.LookAt(closestPlayerObj.transform);

        float distance = Vector3.Distance(transform.position, closestPlayerObj.transform.position);

        if(distance > minDist){
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        
        
        /**
        transform.LookAt(playerObj.transform);

        float distance = Vector3.Distance(transform.position, playerObj.transform.position);

        if(distance > minDist){
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        */
        // Debug.Log(transform.position);
    }
}
