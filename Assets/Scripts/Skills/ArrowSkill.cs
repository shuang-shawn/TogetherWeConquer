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

    private GameObject player1ArrowSpawner;
    private GameObject player2ArrowSpawner;

    private PlayerMovement player1Movement;
    private PlayerMovement player2Movement;

    public GameObject arrowSpawnerPrefab;

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
        player1Movement.OnDirectionChange += HandleDirection;
        player2Movement.OnDirectionChange += HandleDirection;
        player1ArrowSpawner = Instantiate(arrowSpawnerPrefab, player1.transform.position, Quaternion.identity, player1.transform);
        player2ArrowSpawner = Instantiate(arrowSpawnerPrefab, player2.transform.position, Quaternion.identity, player2.transform);
        StartCoroutine(EndArrowBarrage());
    }

    private IEnumerator EndArrowBarrage()
    {
        yield return new WaitForSeconds(skillDuration);
        Destroy(player1ArrowSpawner);
        Destroy(player2ArrowSpawner);
        player1Movement.OnDirectionChange -= HandleDirection;
        player2Movement.OnDirectionChange -= HandleDirection;

    }

    private void HandleDirection(Vector3 vector)
    {
        
    }
}
