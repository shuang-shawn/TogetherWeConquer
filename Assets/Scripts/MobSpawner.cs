using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject mobPrefab; // The mob prefab to spawn
    public int mobWave = 1;
    public int mobCount = 2;     // Number of mobs to spawn
    public float spawnDistance = 10f; // Distance from the camera's edges to spawn
    public LayerMask groundLayer;     // LayerMask for ground detection
    public int spawnBossIndex = 0;
    public List<GameObject> spawnBossList;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        // SpawnMobs();
    }


    public bool isLastBoss() {
        if (spawnBossIndex >= spawnBossList.Count) {
            return true;

        }
        return false;
    }

    public void SpawnMobs()
    {
        for (int i = 0; i < mobCount * mobWave; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Vector3 groundPosition = GetGroundPosition(spawnPosition);
            if (groundPosition != Vector3.zero) // Ensure ground was detected
            {
                Instantiate(mobPrefab, groundPosition, Quaternion.identity);
            }
        }
        mobWave += 1;
    }

    public GameObject SpawnBoss() {
        // Vector2 spawnPosition = GetRandomSpawnPosition();
        // Vector3 groundPosition = GetGroundPosition(spawnPosition);
        // if (groundPosition != Vector3.zero) // Ensure ground was detected
        // {
        //     Debug.Log("spawning boss at location " + groundPosition);
        //     if (spawnBossList != null && spawnBossIndex < spawnBossList.Count) {
        //         Debug.Log("spawning next boss");
        //         Instantiate(spawnBossList[spawnBossIndex], groundPosition, Quaternion.identity);
        //         spawnBossIndex += 1;
        //     } else {
                
        //     }
        // }

            if (spawnBossList != null && spawnBossIndex < spawnBossList.Count) {
                // Debug.Log("spawning next boss");
                GameObject boss = Instantiate(spawnBossList[spawnBossIndex], new Vector3(0, 0, 0), Quaternion.identity);
                spawnBossIndex += 1;
                return boss;
            } else {
                return null;
            }
    }

    Vector2 GetRandomSpawnPosition()
    {
        // Get the camera's bounds
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        // Choose a random edge (top, bottom, left, right)
        int edge = Random.Range(0, 4);
        Vector2 position = Vector2.zero;

        switch (edge)
        {
            case 0: // Top
                position = new Vector2(
                    Random.Range(-camWidth / 2, camWidth / 2),
                    camHeight / 2 + spawnDistance
                );
                break;
            case 1: // Bottom
                position = new Vector2(
                    Random.Range(-camWidth / 2, camWidth / 2),
                    -camHeight / 2 - spawnDistance
                );
                break;
            case 2: // Left
                position = new Vector2(
                    -camWidth / 2 - spawnDistance,
                    Random.Range(-camHeight / 2, camHeight / 2)
                );
                break;
            case 3: // Right
                position = new Vector2(
                    camWidth / 2 + spawnDistance,
                    Random.Range(-camHeight / 2, camHeight / 2)
                );
                break;
        }

        // Offset by the camera's position
        return position + (Vector2)mainCamera.transform.position;
    }

    Vector3 GetGroundPosition(Vector2 position)
    {
        // Raycast down from above the terrain to find the ground
        RaycastHit hit;
        Vector3 rayStart = new Vector3(position.x, 100f, position.y); // Start the ray above the terrain
        if (Physics.Raycast(rayStart, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            return hit.point; // Return the point where the ray hit the ground
        }

        // Debug.LogWarning("No ground detected at position: " + position);
        return Vector3.zero; // Return zero if no ground was found
    }
}
