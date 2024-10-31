using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuoSnipe : MonoBehaviour
{
    public GameObject snipeBulletPrefab;
    private GameObject castingPlayer;
    private GameObject prepingPlayer;
    public float shootForce = 25f;
    public float spawnOffset = 1f;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CastSnipePrep(int playerNum) {
        Debug.Log("player " + playerNum + " casting sniper prep");
        prepingPlayer = GameObject.FindWithTag("Player" + playerNum);
        if (prepingPlayer.TryGetComponent<PlayerManager>(out PlayerManager player)) {
            player.crosshair.ToggleCrosshair();
        }
        
    }  

    public void CastSnipe(int playerNum) {
        Debug.Log("player " + playerNum + " casting sniper end");
        castingPlayer = GameObject.FindWithTag("Player" + playerNum);
        if(playerNum == 1) {
            prepingPlayer = GameObject.FindWithTag("Player2");
        } else if (playerNum == 2) {
            prepingPlayer = GameObject.FindWithTag("Player1");
        }
        if (castingPlayer == null || prepingPlayer == null) {
            return;
        }

        // Create a projectile at Player 2's position

        // Calculate the direction to Player 1 and zero out the Y component
        Vector3 direction = (prepingPlayer.transform.position - castingPlayer.transform.position).normalized;
        direction.y = 0f;

        // Rotate the projectile to face Player 1 (only on the X and Z axes)
        GameObject projectile = Instantiate(snipeBulletPrefab, castingPlayer.transform.position + direction * spawnOffset, Quaternion.identity);
        projectile.transform.forward = direction;

        // Add force to the projectile to shoot it toward Player 1
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(direction * shootForce, ForceMode.VelocityChange);
        if (prepingPlayer.TryGetComponent<PlayerManager>(out PlayerManager player)) {
            player.crosshair.ToggleCrosshair();
        }



    }
}
