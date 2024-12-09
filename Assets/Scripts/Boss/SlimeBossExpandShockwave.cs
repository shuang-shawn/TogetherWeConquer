using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossExpandShockwave : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float expansionSpeed = 2.0f;
    private ParticleSystem.ShapeModule shapeModule;

    public int shockwaveDamage = 20;

    public float maxRadius = 2;

    private HashSet<GameObject> objectsHit = new HashSet<GameObject>();
    private GameObject slimeBoss;
    public float knockbackStrength = 50f;

    void Start()
    {
        shapeModule = particleSystem.shape;
        slimeBoss = GameObject.Find("SlimeBoss");

        //Ensures players are never hit by this skill
        // objectsHit.Add(GameObject.FindWithTag("Player1"));
        // objectsHit.Add(GameObject.FindWithTag("Player2"));

        //Ensure terrain is never detected as a hit
        // objectsHit.Add(GameObject.Find("Terrain"));
    }

    void Update()
    {
        // Gradually increase the radius of the shape
        // if(shapeModule.radius < maxRadius) {
        shapeModule.radius += expansionSpeed * Time.deltaTime;
        // }

    }

    void OnParticleCollision(GameObject other) {
        // Debug.Log("Particle hit: " + other.name);
        // Debug.Log("Hashset Size: " + objectsHit.Count);

        if (objectsHit.Contains(other)) {
            // Debug.Log("Contains this object");
            return;
        } 
        else {
            // Debug.Log("Particle hit: " + other.name);
            bool addedSuccessfully = objectsHit.Add(other);
        
            // Debug.Log(addedSuccessfully 
            // ? "Added successfully: " + other 
            // : "Failed to add: " + other);

            string otherTag = other.gameObject.tag;
            
            // Debug.Log(otherTag);
            if(otherTag == "Player1" || otherTag == "Player2") {
                //Player takes damage
                other.gameObject.GetComponent<PlayerManager>().TakeDamage(shockwaveDamage);
            }

            //Knockback objects
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            if(rb != null) {

                Vector3 slimebossPosition = new Vector3(slimeBoss.transform.position.x, 0, slimeBoss.transform.position.z);
            
                Vector3 knockbackDirection = (other.gameObject.transform.position - slimebossPosition).normalized;

                rb.AddForce(knockbackDirection * knockbackStrength, ForceMode.Impulse);
            }

            

            // if (target != null) {
            //     target.TakeDamage(skillDamage);
            // }
        }
    }
}
