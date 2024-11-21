using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollisionHandler : MonoBehaviour
{
    public int skillDamage = 0;
    public float slowFactor = 1f;
    public float slowTime = 0f;
    public bool oneTimeUse = false;
    // private List<string> enemyTags = new(){"boss", "mob"};

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


        /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        EnemyManager target = other.gameObject.GetComponent<EnemyManager>();
        if (target != null && !other.isTrigger) {
            Vector3 hitPosition = other.ClosestPoint(transform.position);
            target.TakeDamage(skillDamage, hitPosition);
            if (slowFactor != 1) {
                target.Slow(slowFactor, slowTime);
            }
            if (oneTimeUse) {
                Destroy(gameObject);
            }
        }
        
    }
}
