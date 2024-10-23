using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Tether tether;
    public SingleDash singleDash;
    public ParticleSystem dashParticlesPrefab;
    private Dictionary<string, Action<GameObject>> skillDictionary;
    // Start is called before the first frame update
    void Start()
    {
        singleDash = gameObject.AddComponent<SingleDash>();
        singleDash.dashParticlesPrefab = dashParticlesPrefab;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastSkill(string skillName, string playerTag, ComboType comboType) {
        int playerNum = 0;
        if (playerTag == "Player1") {
            playerNum = 1;
        } else if (playerTag == "Player2") {
            playerNum = 2;
        }
        singleDash.Dash(playerNum);
        
       
    }
}
