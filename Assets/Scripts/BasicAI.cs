using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    private GameObject playerObj = null;
    private float speed = 2.0f;
    private float minDist = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null){
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(playerObj.transform.position);
        transform.LookAt(playerObj.transform);

        float distance = Vector3.Distance(transform.position, playerObj.transform.position);

        if(distance > minDist){
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        // Debug.Log(transform.position);
    }
}
