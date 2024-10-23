using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int damageAmount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "boss"){
            damageAmount = 25;
        } else if (gameObject.tag == "mob") {
            damageAmount = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
