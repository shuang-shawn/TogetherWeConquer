using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic of Shadow Skill
/// </summary>
public class ShadowSkill : MonoBehaviour
{
    const string P1_TAG = "Player1";
    const string P2_TAG = "Player2";

    private GameObject player1;
    private GameObject player2;
    public GameObject shadowPrefab;
    [SerializeField]
    private GameObject currentP1Shadow;
    private GameObject currentP2Shadow;

    public GameObject placeEffect;
    public GameObject teleportEffect;

    private void Start()
    {
        player1 = GameObject.Find(P1_TAG);
        player2 = GameObject.Find(P2_TAG);
    }

    // Logic for placing a shadow
    public void CastShadow(string playerTag) 
    {
        GameObject player = (playerTag == P1_TAG) ? player1 : player2;
        GameObject currentShadow = (playerTag == P1_TAG) ? currentP1Shadow : currentP2Shadow;
        
        if (currentShadow == null)
        {
            Vector3 shadowPosition = player.transform.position + new Vector3(0, 0, 0.001f);
            currentShadow = Instantiate(shadowPrefab, shadowPosition, Quaternion.identity);
            GameObject currPlaceEffect = Instantiate(placeEffect, player.transform.position, Quaternion.identity);
            Destroy(currPlaceEffect, 1f);
            if (playerTag == P1_TAG)
            {
                currentP1Shadow = currentShadow;
            } else
            {
                currentP2Shadow = currentShadow;
            }
        } else
        {
          TeleportToShadow(player, currentShadow, playerTag);
        }
    }

    // Logic for teleporting to shadow
    private void TeleportToShadow(GameObject player, GameObject currentShadow, string playerTag)
    {
        GameObject currTeleportEffect = Instantiate(teleportEffect, player.transform.position, Quaternion.identity);
        player.transform.position = currentShadow.transform.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Physics.SyncTransforms();
        Destroy(currentShadow);
        Destroy(currTeleportEffect, 1f);
        if (playerTag == P1_TAG)
        {
            currentP1Shadow = null;
        }
        else
        {
            currentP2Shadow = null;
        }
    }
}
