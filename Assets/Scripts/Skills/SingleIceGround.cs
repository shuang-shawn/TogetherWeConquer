using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleIceGround : MonoBehaviour
{
    private GameObject iceGroundObject;
    private GameObject player;
    public float slowFactor = 0.5f;
    public float slowTime = 3;
    public GameObject iceGroundPrefab;
    

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
        iceGroundObject = Instantiate(iceGroundPrefab, player.transform.position + new Vector3(0, 1.56f, 0), Quaternion.Euler(90, 0, 0));

        SkillCollisionHandler skillCollisionHandler = iceGroundObject.AddComponent<SkillCollisionHandler>();
        skillCollisionHandler.skillDamage = 0;
        skillCollisionHandler.slowFactor = slowFactor;
        skillCollisionHandler.slowTime = slowTime;
        skillCollisionHandler.oneTimeUse = true;
    }
}
