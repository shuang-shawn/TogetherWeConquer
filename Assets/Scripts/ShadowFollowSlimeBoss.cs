using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollowSlimeBoss : MonoBehaviour
{
    private GameObject slimeBoss;
    // Start is called before the first frame update
    void Start()
    {
        slimeBoss = GameObject.FindWithTag("SlimeBoss");
        // transform.localScale.x = slimeBoss.transform.localScale.x;
        // transform.localScale.z = slimeBoss.transform.localScale.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(slimeBoss.transform.position.x, transform.position.y, slimeBoss.transform.position.z);
    }
}