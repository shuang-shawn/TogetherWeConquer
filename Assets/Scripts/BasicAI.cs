using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    private GameObject closestPlayerObj = null;
    public float speed = 2.0f;
    private float minDist = 1f;

    // Returns the distance between the object the script is attached to, and the targetObject
    // Takes in a single GameObject and returns a float representing the distance
    private float findDistance(GameObject targetObject){
        return Vector3.Distance(transform.position, targetObject.transform.position);
    }

    // FindClosestPlayer determines which player is the closest, and sets it to that player
    private void findClosestPlayer(){
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

        float player1Distance = findDistance(player1);
        float player2Distance = findDistance(player2);

        if(player2Distance > player1Distance){
            closestPlayerObj = player1;
        } else {
            closestPlayerObj = player2;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // findClosestPlayer();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(playerObj.transform.position);
        findClosestPlayer();
        
        // Trying without transform.LookAt

        Vector3 playerPosition = new Vector3(closestPlayerObj.transform.position.x, transform.position.y, closestPlayerObj.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

        
        // transform.LookAt(closestPlayerObj.transform);

        // float distance = findDistance(closestPlayerObj);

        // if(distance > minDist){
        //     transform.position += transform.forward * speed * Time.deltaTime;
        // }
        // Debug.Log(transform.position);
    }
}
