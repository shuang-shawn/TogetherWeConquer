using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private static int maxHealth = 100;
    private static int damage = 10;
    int currentHealth = maxHealth;
    public void handleCollision(GameObject gameObject){
        
        // Enemy gets hit by a combo
        if(gameObject.tag == "TestCombo"){
            string hitMessage = "Hit by Combo";
            Debug.Log(hitMessage);
        }

        // Enemy hits player
        else if(gameObject.tag == "Player1"){
            gameObject.GetComponent<PlayerStats>().playerGotHit(damage);

            //Add gets knocked back a certain amount
        }
    }

    // For when enemy gets hit by a combo
    public void changeHealth(int amount){
        currentHealth -= amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        handleCollision(collision.gameObject);
    }   
}
