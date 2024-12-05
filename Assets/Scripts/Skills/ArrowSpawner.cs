using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    [SerializeField]
    private float arrowLifetime = 3f; // Time before each arrow is destroyed

    private Vector3 currentArrowDirection = Vector3.right;

    public bool spawnArrows = true;

    [SerializeField]
    private AudioSource arrowSFX;

    [SerializeField]
    private float audioDelay = 2f; // Delay between each sound

    private float audioTimer = 0f; 

    private void Start()
    {
        StartCoroutine(SpawnArrows());
    }

    // Spawns the barrage of arrows in intervals
    private IEnumerator SpawnArrows()
    {
        while (spawnArrows)
        {
            // Instantiate the arrow GameObject at the spawner's position and with rotation
            float randomHeight = Random.Range(minHeightOffset, minHeightOffset + maxHeightOffset);

            Quaternion rotation = Quaternion.identity;

            if (currentArrowDirection == Vector3.left) {

                rotation = Quaternion.Euler(0, 180, 0);
            }

            GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x, randomHeight, transform.position.z), rotation, transform);
            if (audioTimer <= 0f)
            {
                arrowSFX.PlayOneShot(arrowSFX.clip);
                audioTimer = audioDelay; // Reset the timer
            }
            // Destroy the arrow after its lifetime expires
            Destroy(arrow, arrowLifetime);

            float randomSpawnDelay = Random.Range(minSpawnRate, maxSpawnRate);

            // Update the audio timer
            audioTimer -= randomSpawnDelay;

            // Wait for the next spawn interval
            yield return new WaitForSeconds(randomSpawnDelay);
        }
    }

    public void RotateDirection(Vector3 direction)
    {
        currentArrowDirection = direction;
    }
}
