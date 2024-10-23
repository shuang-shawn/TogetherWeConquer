using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{

    private Vector3 startPosition;
    private GameObject closestPlayerObj = null;
    public float speed = 2.0f;
    public float hopHeight = 3.0f;
    public float hopFrequency = 1f;
    public float dropSpeed = 5f;
    private float hopMotion;
    private bool jumpAttacking = false;
    private float jumpAttackHeight = 10f;
    float jumpAttackLandingTimer = 0f;


 

    //Maybe put these two functions inside a class that is inherited by enemies?
    
    private void findClosestPlayer(){
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        float player1Distance = 9999f;
        float player2Distance = 9999f;
        if(player1 != null) {

            player1Distance = findDistance(player1);
        }
        if(player2 != null) {

            player2Distance = findDistance(player2);
        }

        if(player2Distance > player1Distance){
            closestPlayerObj = player1;
        } else if (player2Distance < player1Distance) {
            closestPlayerObj = player2;
        } else {
            closestPlayerObj = null;
        }

    }

    // Returns the distance between the object the script is attached to, and the targetObject
    // Takes in a single GameObject and returns a float representing the distance
    private float findDistance(GameObject targetObject){
        return Vector3.Distance(transform.position, targetObject.transform.position);
    }

    private void controlHopping(){
        hopMotion = Mathf.Sin(Time.time * hopFrequency) * hopHeight;
    }

    private void jumpAttack(){
        if (closestPlayerObj == null) {
            return;
        }
        //Jump up into the sky
        transform.position = new Vector3(transform.position.x, jumpAttackHeight, transform.position.z);
        jumpAttackLandingTimer += Time.deltaTime;

        if(jumpAttackLandingTimer > 5){
            jumpAttacking = false;
            jumpAttackLandingTimer = 0f;
            
            Debug.Log("5 seconds passed");
            Debug.Log(jumpAttacking);
            
            transform.position = new Vector3(closestPlayerObj.transform.position.x, transform.position.y, closestPlayerObj.transform.position.z);
            Vector3 landingPoint = new Vector3(closestPlayerObj.transform.position.x, closestPlayerObj.transform.position.y, closestPlayerObj.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, landingPoint, dropSpeed * Time.deltaTime);


        }

        // Wait a few seconds OR
        // Move towards closest player (faster than normal)
        // Pause when above last player location, wait a bit, then slam down
    }
    private void HopToPlayer(){
        controlHopping();
        if (closestPlayerObj == null) {
            return;
        }
        Vector3 playerPosition = new Vector3(closestPlayerObj.transform.position.x, startPosition.y, closestPlayerObj.transform.position.z);

        // While the sine value is greater than 0, move character
        // Simulates jump, pause, jump
        if(hopMotion > 0){
            //move enemy

            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);

            // Simulate the hopping motion
            transform.position = new Vector3(transform.position.x, startPosition.y + hopMotion, transform.position.z);
        }
    }
    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        findClosestPlayer();
        
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpAttacking = true;

            Debug.Log("Space pressed");
            Debug.Log(jumpAttacking);
        }

        if(jumpAttacking){
            jumpAttack();
        } else {
            HopToPlayer();
        }
        
        
        


    }
}
