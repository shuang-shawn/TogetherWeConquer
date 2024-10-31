using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeBullet : MonoBehaviour
{
    public int skillDamage = 50;
    // private EnemyManager enemy;
    // private PlayerManager ally;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.TryGetComponent<EnemyManager>(out EnemyManager target)) {
            target.TakeDamage(skillDamage);
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.TryGetComponent<PlayerManager>(out PlayerManager ally)) {
            ally.TakeDamage(skillDamage);
            Destroy(gameObject);
            return;
        }
        
    }
}
