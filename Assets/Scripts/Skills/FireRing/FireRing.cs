using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour
{

    private GameObject Player1;
    private GameObject Player2;
    public ParticleSystem fireRingPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Player1 = GameObject.FindWithTag("Player1");
        Player2 = GameObject.FindWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastFireRing(){
        //Casting logic here

        // Instantiate fire ring at locations
        ParticleSystem fireRingPlayer1 = Instantiate(fireRingPrefab, Player1.transform.position, fireRingPrefab.transform.rotation);
        ParticleSystem fireRingPlayer2 = Instantiate(fireRingPrefab, Player2.transform.position, fireRingPrefab.transform.rotation);

        // Play the fire ring animation
        fireRingPlayer1.Play();
        fireRingPlayer2.Play();

        // Removes particlesystem after it finishes
        Destroy(fireRingPlayer1.gameObject, fireRingPlayer1.main.duration);
        Destroy(fireRingPlayer2.gameObject, fireRingPlayer2.main.duration);
    }
}
