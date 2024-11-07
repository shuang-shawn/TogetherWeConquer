using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStone : MonoBehaviour
{
    public GameObject singleStonePrefab;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastStone(int playerNum) {
        player = GameObject.FindWithTag("Player" + playerNum);
        if (player == null) {
            return;
        }

        Instantiate(singleStonePrefab, player.transform.position + new Vector3(0, 0.63f, 0.4f), Quaternion.Euler(0, 0, 0));
    }
}
