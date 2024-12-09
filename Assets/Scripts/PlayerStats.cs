using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;  

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void playerGotHit(int damage){
        currentHealth -= damage;

        // Debug.Log("Current Health: " + currentHealth);
        
        //Set an invulnerability window
    }

}
