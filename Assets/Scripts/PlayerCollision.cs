using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private static int maxHealth = 100;
    int currentHealth = maxHealth;
    public void handleCollision(Collision collision){
        if(collision.gameObject.tag == "BasicEnemy"){
            string hitMessage = "Hit Detected";
            Debug.Log(hitMessage);
        }
    }

    public void changeHealth(int amount){
        currentHealth -= amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        handleCollision(collision);
    }   

}
