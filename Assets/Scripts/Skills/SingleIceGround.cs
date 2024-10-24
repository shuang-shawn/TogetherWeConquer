using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIceGround : MonoBehaviour
{
    private GameObject fireGroundObject;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastIceGround(int playerNum) {
        player = GameObject.FindWithTag("Player" + playerNum);
        if (player == null) {
            return;
        }
        fireGroundObject = new GameObject("FireGroundObject");
        fireGroundObject.transform.position = player.transform.position;
        SkillCollisionHandler skillCollisionHandler = fireGroundObject.AddComponent<SkillCollisionHandler>();
        skillCollisionHandler.skillDamage = 0;
    }
}
