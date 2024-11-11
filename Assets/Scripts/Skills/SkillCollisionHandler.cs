using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollisionHandler : MonoBehaviour
{
    public int skillDamage = 0;
    public float slowFactor = 1f;
    public float slowTime = 0f;
    public bool oneTimeUse = false;
    private ForceField forceField;
    // private List<string> enemyTags = new(){"boss", "mob"};

    
    // Start is called before the first frame update
    void Start()
    {
        //Find the ForceField script in the parent or the current GameObject
        forceField = GetComponentInParent<ForceField>(); 
        if (forceField == null) 
        { 
            forceField = GetComponent<ForceField>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    { 
        // Debug log to check for collisions
        Debug.Log("Collision detected with: " + other.gameObject.name); 
        // Ensure the collision is with an enemy
        if (other.gameObject.CompareTag("boss")) 
        { 
            EnemyManager target = other.gameObject.GetComponent<EnemyManager>();
            if (target != null) 
            { 
                Debug.Log("Enemy hit! Taking damage.");
                // Log enemy hit 
                target.TakeDamage(skillDamage); 
                if (slowFactor != 1)
                { 
                    target.Slow(slowFactor, slowTime);
                } 
                if (oneTimeUse)
                {
                    // Destroy the forcefield
                    Destroy(gameObject);
                    // Log destruction
                    Debug.Log("Forcefield destroyed.");
                }
            } 
        } 
    }
}






        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
//    private void OnTriggerEnter(Collider other)
//    {
//        EnemyManager target = other.gameObject.GetComponent<EnemyManager>();
//        if (target != null) {
//            target.TakeDamage(skillDamage);
//            if (slowFactor != 1) {
//                target.Slow(slowFactor, slowTime);
//            }
//            if (oneTimeUse) {
//                Destroy(gameObject);
//            }
//        }

//    }
//}
