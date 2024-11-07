using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandRing : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particleSystem;
    public float expansionSpeed = 1.0f;
    private ParticleSystem.ShapeModule shapeModule;

    public int skillDamage = 100;

    public float maxRadius = 2;

    private HashSet<GameObject> objectsHit = new HashSet<GameObject>();

    void Start()
    {
        shapeModule = particleSystem.shape;

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

        if (objectsHit.Contains(other)) {
            return;
        } else {
            Debug.Log("Particle hit: " + other.name);

            // Temporary do damage
            EnemyManager target = other.gameObject.GetComponent<EnemyManager>();

            if (target != null) {
                target.TakeDamage(skillDamage);
            }


        }

        objectsHit.Add(other);
    }
}
