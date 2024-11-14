using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRingAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab; // Assign the projectile prefab in the Inspector
    [SerializeField]
    private int projectileCount = 12; // Number of projectiles in the burst
    [SerializeField]
    private float burstSpeed = 5f; // Speed of each projectile
    [SerializeField]
    private float projectileLifeTime = 15f;

    public void ExecuteProjectileRingAttack()
    {
        float angleStep = 360f / projectileCount;
        float angle = 0f;
        float yOffset = 0.5f;

        for (int i = 0; i < projectileCount; i++)
        {
            // Calculate the direction for each projectile on the XZ plane
            float projectileDirX = Mathf.Sin(angle * Mathf.Deg2Rad);
            float projectileDirZ = Mathf.Cos(angle * Mathf.Deg2Rad);
            Vector3 projectileDirection = new Vector3(projectileDirX, 0, projectileDirZ);

            Vector3 spawnPosition = transform.position + new Vector3(0, yOffset, 0);

        
            GameObject proj = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            proj.GetComponent<TurtleProjectile>().SetDirection(projectileDirection);
            proj.GetComponent<TurtleProjectile>().speed = burstSpeed;

            angle += angleStep;

            Destroy(proj, projectileLifeTime);
        }
    }
}
