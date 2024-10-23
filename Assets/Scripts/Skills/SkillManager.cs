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
    public Material tetherMaterial;
    public ParticleSystem onHit;


    // private Dictionary<string, Delegate> skillDictionary;
    // Start is called before the first frame update
    void Start()
    {
        singleDash = gameObject.AddComponent<SingleDash>();
        singleDash.dashParticlesPrefab = dashParticlesPrefab;
        tether = gameObject.AddComponent<Tether>();
        tether.tetherMaterial = tetherMaterial;
        tether.onHit = onHit;
        // skillDictionary = new Dictionary<string, Delegate>
        // {
        //     {"dash", new Action<int>(singleDash.Dash)},
        //     {"tether", new Action(tether.CastTether)},
        // };

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
            case "dash": singleDash.Dash(playerNum); break;
            case "tether": tether.CastTether(); break;
        }
    }

    
}
