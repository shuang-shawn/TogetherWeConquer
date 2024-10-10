using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static int maxHealth = 100;
    int currentHealth = maxHealth;  

    public void playerGotHit(int damage){
        currentHealth -= damage;

        Debug.Log("Current Health: " + currentHealth);
        
        //Set an invulnerability window
    }

}
