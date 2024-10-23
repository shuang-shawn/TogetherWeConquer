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
    public GameObject shadow;
    private Vector3 landingPoint = new Vector3(0,0,0);
    private Coroutine stopwatchCoroutine;

 

    //Maybe put these two functions inside a class that is inherited by enemies?
    
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

    // Returns the distance between the object the script is attached to, and the targetObject
    // Takes in a single GameObject and returns a float representing the distance
    private float findDistance(GameObject targetObject){
        return Vector3.Distance(transform.position, targetObject.transform.position);
    }

    private void controlHopping(){
        hopMotion = Mathf.Sin(Time.time * hopFrequency) * hopHeight;
    }

    //Timer coroutine stuf
    private void StartStopwatch(){
        if(stopwatchCoroutine == null) {
            stopwatchCoroutine = StartCoroutine(StopwatchCoroutine());
        }
    }

    private void StopStopwatch(){
        if(stopwatchCoroutine != null) {
            StopCoroutine(StopwatchCoroutine());
            stopwatchCoroutine = null;
            jumpAttackLandingTimer = 0;
        }
    }
    
    private IEnumerator StopwatchCoroutine() {
        while (true) {
            jumpAttackLandingTimer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPoint, float moveSpeed){
        while (Vector3.Distance(transform.position, targetPoint) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPoint;
    }

    private IEnumerator TrackPlayer(){
        Debug.Log("TrackPlayer started");
        StartStopwatch(); 
        while(jumpAttackLandingTimer < 5) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(closestPlayerObj.transform.position.x, jumpAttackHeight, closestPlayerObj.transform.position.z), speed * Time.deltaTime *2);
            yield return null;
        }
        
        Debug.Log("5 seconds passed");
        StopStopwatch();
        
        // yield return null;
    }


    private IEnumerator JumpAttackSequence(){
        //jump into air
        yield return StartCoroutine(MoveToPosition(new Vector3(transform.position.x, jumpAttackHeight, transform.position.y), dropSpeed));
        GameObject instantiatedShadow = Instantiate(shadow, new Vector3(transform.position.x, 0.1f, transform.position.z), Quaternion.identity);

        //Track closest player for 5 seconds
        yield return StartCoroutine(TrackPlayer());
        //Land
        yield return StartCoroutine(MoveToPosition(new Vector3(transform.position.x, 1, transform.position.z), dropSpeed));
        Destroy(instantiatedShadow);
        //Daze effect
        yield return new WaitForSeconds(3);
        jumpAttacking = false;
        
    }

    private void revisedJumpAttack(){
        //jump into air
        // Coroutine jumpIntoAir = StartCoroutine(MoveToPosition(new Vector3(transform.position.x, jumpAttackHeight, transform.position.y)));
        //timer and lock into last player position
        //draw shadow at last player position
        //land

        StartCoroutine(JumpAttackSequence());
    }

    // private void jumpAttack(){
    //     GameObject instantiatedShadow = GameObject.FindWithTag("JumpAttackShadow");

    //     //Jump up into the sky
    //     transform.position = new Vector3(transform.position.x, jumpAttackHeight, transform.position.z);
    //     jumpAttackLandingTimer += Time.deltaTime;

    //     if(transform.position.y <= 1) {
    //         jumpAttacking = false;
    //         jumpAttackLandingTimer = 0f;

    //         //Destroy shadow
    //         if(instantiatedShadow != null){
    //                 Destroy(instantiatedShadow);
    //                 // instantiatedShadow = null;
    //             } else{
    //                 Debug.Log("No shadow to destroy");
    //             }
    //     }

    //     else if(jumpAttackLandingTimer > 5 && transform.position.y > 1){
    //         //Stop jump attacking
    //         // jumpAttacking = false;
    //         // jumpAttackLandingTimer = 0f;
            
    //         Debug.Log("5 seconds passed");
    //         Debug.Log(jumpAttacking);
            
    //         // transform.position = new Vector3(closestPlayerObj.transform.position.x, transform.position.y, closestPlayerObj.transform.position.z);
    //         // Vector3 landingPoint = new Vector3(closestPlayerObj.transform.position.x, closestPlayerObj.transform.position.y, closestPlayerObj.transform.position.z);
    //         Vector3 direction = (landingPoint - transform.position).normalized;

    //         float drop = dropSpeed * Time.deltaTime;
    //         transform.position += direction * drop;
    //         // transform.position = Vector3.MoveTowards(transform.position, landingPoint, dropSpeed * Time.deltaTime);
            

    //         // Debug.Log(instantiatedShadow == null);
    //     } else if (jumpAttackLandingTimer < 3) {

    //         // Track position until timer = 3s

    //         transform.position = new Vector3(closestPlayerObj.transform.position.x, transform.position.y, closestPlayerObj.transform.position.z);
    //         landingPoint = new Vector3(closestPlayerObj.transform.position.x, closestPlayerObj.transform.position.y, closestPlayerObj.transform.position.z);
    //     } else {
    //         //do stuff between finish tracking and landing
    //         //Draw circle
    //         if(instantiatedShadow == null) {
    //             Instantiate(shadow, new Vector3(landingPoint.x, 0, landingPoint.z), Quaternion.identity);
    //         }
    //     }

    //     // Wait a few seconds OR
    //     // Move towards closest player (faster than normal)
    //     // Pause when above last player location, wait a bit, then slam down
    // }
    private void HopToPlayer(){
        controlHopping();
            
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
        
        if(Input.GetKeyDown(KeyCode.Space) && !jumpAttacking){
            jumpAttacking = true;

            Debug.Log("Space pressed");
            Debug.Log(jumpAttacking);
            // jumpAttack();
            revisedJumpAttack();
        }

        if(!jumpAttacking) {
            HopToPlayer();
        }

        // if(jumpAttacking){
        //     jumpAttack();
        // } else {
        //     HopToPlayer();
        // }
        
        
        


    }
}
