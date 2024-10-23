using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Tether tether;
    public SingleDash singleDash1;
    public SingleDash singleDash2;
    public SingleIceGround singleIceGround;

    public ParticleSystem dashParticlesPrefab;
    public Material tetherMaterial;
    public ParticleSystem onHit;


    // private Dictionary<string, Delegate> skillDictionary;
    // Start is called before the first frame update
    void Start()
    {
        singleDash1 = gameObject.AddComponent<SingleDash>();
        singleDash1.dashParticlesPrefab = dashParticlesPrefab;
        singleDash2 = gameObject.AddComponent<SingleDash>();
        singleDash2.dashParticlesPrefab = dashParticlesPrefab;


        singleIceGround = gameObject.AddComponent<SingleIceGround>();



        tether = gameObject.AddComponent<Tether>();
        tether.tetherMaterial = tetherMaterial;
        tether.onHit = onHit;


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
        switch (skillName) {
            case "dash": 
            if(playerNum == 1) {
                singleDash1.Dash(playerNum); 
            } else if (playerNum == 2) {
                singleDash2.Dash(playerNum);
            }
                break;
            case "iceground": singleIceGround.CastIceGround(playerNum);
                break;
            case "tether": tether.CastTether(); break;
        }
    }



    
}
