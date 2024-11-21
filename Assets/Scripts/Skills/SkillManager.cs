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
    public HelpingHand singleHelpingHand;
    public DuoSnipe duoSnipe;
    public SingleStone singleStone;

    public ParticleSystem dashParticlesPrefab;
    public Material tetherMaterial;
    public ParticleSystem onHit;
    public GameObject iceGroundPrefab;
    public GameObject snipeBulletPrefab;
    public AudioController audioController;
    public GameObject singleStonePrefab;

    public ShadowSkill shadowSkill;
    public GameObject shadowSkillPrefab;
    public GameObject shadowSkillPlaceEffect;
    public GameObject shadowSkillTeleportEffect;
    public FireRing fireRing;
    public ParticleSystem fireRingPrefab;

    public Swap swap;


    public ArrowSkill arrowSkill;
    public GameObject arrowSpawnerPrefab;

    public Drain drainSkill;
    public GameObject drainCirclePrefab;
    // private Dictionary<string, Delegate> skillDictionary;
    // Start is called before the first frame update
    void Start()
    {
        singleDash1 = gameObject.AddComponent<SingleDash>();
        singleDash1.dashParticlesPrefab = dashParticlesPrefab;
        singleDash2 = gameObject.AddComponent<SingleDash>();
        singleDash2.dashParticlesPrefab = dashParticlesPrefab;

        singleIceGround = gameObject.AddComponent<SingleIceGround>();
        singleIceGround.iceGroundPrefab = iceGroundPrefab; 

        tether = gameObject.AddComponent<Tether>();
        tether.tetherMaterial = tetherMaterial;
        tether.onHit = onHit;

        singleHelpingHand = gameObject.AddComponent<HelpingHand>();

        duoSnipe = gameObject.AddComponent<DuoSnipe>();
        duoSnipe.snipeBulletPrefab = snipeBulletPrefab;

        singleStone = gameObject.AddComponent<SingleStone>();
        singleStone.singleStonePrefab = singleStonePrefab;

        shadowSkill = gameObject.AddComponent<ShadowSkill>();
        shadowSkill.shadowPrefab = shadowSkillPrefab;
        shadowSkill.placeEffect = shadowSkillPlaceEffect;
        shadowSkill.teleportEffect = shadowSkillTeleportEffect;

        arrowSkill = gameObject.AddComponent<ArrowSkill>();
        arrowSkill.arrowSpawnerPrefab = arrowSpawnerPrefab;

        drainSkill = gameObject.AddComponent<Drain>();
        drainSkill.drainCirclePrefab = drainCirclePrefab;
        fireRing = gameObject.AddComponent<FireRing>();
        fireRing.fireRingPrefab = fireRingPrefab;

        swap = gameObject.AddComponent<Swap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastFirstHalfDuoSkill(string skillName, string playerTag, ComboType comboType=ComboType.Duo) {
        int playerNum = 0;
        if (playerTag == "Player1") {
            playerNum = 1;
        } else if (playerTag == "Player2") {
            playerNum = 2;
        }
        switch (skillName) {
            case "snipe": 
            duoSnipe.CastSnipePrep(playerNum);
            audioController.PlaySnipePrep();
            break;
        }
    }

    public void CastSkill(string skillName, string playerTag, ComboType comboType=ComboType.Solo) {
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
            case "iceground": 
                singleIceGround.CastIceGround(playerNum);
                break;
            case "tether": 
                tether.CastTether(); 
                break;
            case "helpinghand": 
                singleHelpingHand.CastHelpingHand((playerNum == 1) ? 2 : 1);
                break;
            case "snipe": 
                duoSnipe.CastSnipe(playerNum); 
                audioController.PlaySnipeFinish(); 
                break;
            case "stone": 
                singleStone.CastStone(playerNum); 
                break;
            case "shadow":
                shadowSkill.CastShadow(playerTag);
                break;
            case "arrowbarrage":
                arrowSkill.CastArrowBarrage();
                break;
            case "drain":
                drainSkill.CastDrain();
                break;
            case "fireRing":
                fireRing.CastFireRing();
                break;
            case "swap":
                swap.CastSwap();
                break;
            default:
                break;
        }
    }



    
}
