using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;  

    [SerializeField]
    private float minSpawnRate = 0.05f;

    [SerializeField]
    private float maxSpawnRate = 0.1f;

    [SerializeField]
    private float minHeightOffset = 0.22f;

    [SerializeField]
    private float maxHeightOffset = 1f;

    public float arrowLifetime = 2f; // Time before each arrow is destroyed

    private void Start()
    {
        // Start the coroutine to spawn arrows at intervals
        StartCoroutine(SpawnArrows());
    }

    private IEnumerator SpawnArrows()
    {
        while (true)
        {
            // Instantiate the arrow GameObject at the spawner's position and with rotation
             float randomHeight = Random.Range(minHeightOffset, minHeightOffset + maxHeightOffset);
             GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x, randomHeight, transform.position.z), Quaternion.identity, transform);

            // Destroy the arrow after its lifetime expires
             Destroy(arrow, arrowLifetime);

            float randomSpawnDelay = Random.Range(minSpawnRate, maxSpawnRate);
            // Wait for the next spawn interval
            yield return new WaitForSeconds(randomSpawnDelay);
        }
    }
}
