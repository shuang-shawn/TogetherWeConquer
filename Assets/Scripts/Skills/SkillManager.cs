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
    public ParticleSystem fire;
    public GameObject iceGroundPrefab;

    public ShadowSkill shadowSkill;
    public GameObject shadowSkillPrefab;
    public GameObject shadowSkillPlaceEffect;
    public GameObject shadowSkillTeleportEffect;

    public ForceField forceFieldSkill;
    public GameObject forceFieldSkillPrefab;

    public ArrowSkill arrowSkill;
    public GameObject arrowSpawnerPrefab;

    public Sawblade sawbladeSkill;
    public GameObject sawbladePrefab;

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

        shadowSkill = gameObject.AddComponent<ShadowSkill>();
        shadowSkill.shadowPrefab = shadowSkillPrefab;
        shadowSkill.placeEffect = shadowSkillPlaceEffect;
        shadowSkill.teleportEffect = shadowSkillTeleportEffect;

        arrowSkill = gameObject.AddComponent<ArrowSkill>();
        arrowSkill.arrowSpawnerPrefab = arrowSpawnerPrefab;

        forceFieldSkill = gameObject.AddComponent<ForceField>();
        forceFieldSkill.forceFieldPrefab = forceFieldSkillPrefab;

        sawbladeSkill = gameObject.AddComponent<Sawblade>();
        sawbladeSkill.sawbladePrefab = sawbladePrefab;
    }

    void Update()
    {

    }

    public void CastSkill(string skillName, string playerTag, ComboType comboType)
    {
        int playerNum = 0;
        if (playerTag == "Player1")
        {
            playerNum = 1;
        }
        else if (playerTag == "Player2")
        {
            playerNum = 2;
        }
        switch (skillName)
        {
            case "dash":
                if (playerNum == 1)
                {
                    singleDash1.Dash(playerNum);
                }
                else if (playerNum == 2)
                {
                    singleDash2.Dash(playerNum);
                }
                break;
            case "iceground":
                singleIceGround.CastIceGround(playerNum);
                break;
            case "tether":
                tether.CastTether();
                break;
            case "shadow":
                shadowSkill.CastShadow(playerTag);
                break;
            case "arrow_barrage":
                arrowSkill.CastArrowBarrage();
                break;
            case "forceField":
                forceFieldSkill.CastForceField(playerNum);
                break;
            case "sawblades":
                sawbladeSkill.SpawnSawblades();
                break;
            default:
                break;
        }
    }
}
