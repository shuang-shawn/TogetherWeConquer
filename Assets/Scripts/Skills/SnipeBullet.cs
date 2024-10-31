using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeBullet : MonoBehaviour
{
    public int skillDamage = 50;
    public AudioController audioController;
    private Renderer objectRenderer;
    private int framesToWait = 5;
    // private EnemyManager enemy;
    // private PlayerManager ally;
    
    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (framesToWait > 0)
        {
            framesToWait--;
            return;
        }
        if (!objectRenderer.isVisible)
        {
            Debug.Log($"{gameObject.name} is not visible in the current scene.");
            Destroy(gameObject);

            // Perform any action if the object is not visible
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.TryGetComponent<EnemyManager>(out EnemyManager target)) {
            target.TakeDamage(skillDamage);
            audioController.PlayBossSnipeHit();
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.TryGetComponent<PlayerManager>(out PlayerManager ally)) {
            ally.TakeDamage(skillDamage);
            audioController.PlayBossSnipeHit();
            Destroy(gameObject);
            return;
        }
        
    }
}
