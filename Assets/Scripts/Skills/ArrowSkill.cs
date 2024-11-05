using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ArrowSkill : MonoBehaviour
{
    const string P1_TAG = "Player1";
    const string P2_TAG = "Player2";

    private GameObject player1;
    private GameObject player2;

    private ArrowSpawner player1ArrowSpawner;
    private ArrowSpawner player2ArrowSpawner;

    private PlayerMovement player1Movement;
    private PlayerMovement player2Movement;

    public GameObject arrowSpawnerPrefab;

    [SerializeField]
    private float skillDuration = 10f;

    private void Start()
    {
        player1 = GameObject.Find(P1_TAG);
        player2 = GameObject.Find(P2_TAG);

        player1Movement = player1.GetComponent<PlayerMovement>();
        player2Movement = player2.GetComponent<PlayerMovement>();
    }

    public void CastArrowBarrage()
    {
        // Sets Handle Direction callback function
        player1Movement.OnDirectionChange += HandleDirection;
        player2Movement.OnDirectionChange += HandleDirection;
        player1ArrowSpawner = Instantiate(arrowSpawnerPrefab, player1.transform.position, Quaternion.identity, player1.transform).GetComponent<ArrowSpawner>();
        player2ArrowSpawner = Instantiate(arrowSpawnerPrefab, player2.transform.position, Quaternion.identity, player2.transform).GetComponent<ArrowSpawner>();

        // Calls callback function right away for initial player direction
        HandleDirection(player1Movement.lastDirectionX, P1_TAG);
        HandleDirection(player2Movement.lastDirectionX, P2_TAG);

        StartCoroutine(EndArrowBarrage());
    }

    // Changes the direction of arrows fired depending on players direction
    private void HandleDirection(Vector3 vector, string playerTag)
    {
        ArrowSpawner currentArrowSpawner = (playerTag == P1_TAG) ? player1ArrowSpawner : player2ArrowSpawner;
        if (vector.x == 1)
        {
            currentArrowSpawner.RotateDirection(Vector3.right);
        }
        else
        {
            currentArrowSpawner.RotateDirection(Vector3.left);
        }
    }

    // Ends the arrow barrage after a period of time
    private IEnumerator EndArrowBarrage()
    {
        yield return new WaitForSeconds(skillDuration);
        player1ArrowSpawner.spawnArrows = false;
        player2ArrowSpawner.spawnArrows = false;
        Destroy(player1ArrowSpawner.gameObject, 2f);
        Destroy(player2ArrowSpawner.gameObject, 2f);
        player1Movement.OnDirectionChange -= HandleDirection;
        player2Movement.OnDirectionChange -= HandleDirection;
    }

}
