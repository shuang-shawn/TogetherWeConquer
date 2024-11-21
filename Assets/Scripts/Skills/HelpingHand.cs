using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HelpingHand : MonoBehaviour
{
    private float phaseDuration = 3f;  // Duration of phasing effect
    private int damageOnCast = 10;  // Damage taken by the phased player on cast
    private float slowDuration = 3f;   // Duration of slow debuff on enemies
    private float slowFactor = 0.8f;   // Slow amount

    private GameObject player;
    private SpriteRenderer spriteRenderer;

    public void CastHelpingHand(int playerNum)
    {
        player = GameObject.FindWithTag("Player" +  playerNum);
        spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();

        if (player == null)
        {
            return;
        }

        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.TakeDamage(damageOnCast);
        }

        // Phase the target player through enemies
        StartCoroutine(Phasing(player));
    }

    private IEnumerator Phasing(GameObject player)
    {
        // The target player can pass through enemies for a set duration
        // This might involve disabling their collision temporarily
        Collider playerCollider = player.GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false; // Disable collision with enemies
            SetAlpha(0.5f);
            yield return new WaitForSeconds(phaseDuration);  // Wait while the player is phased
            playerCollider.enabled = true;  // Re-enable collision
            SetAlpha(1f);
        }

        // Eventually make this only on perfect
        Collider[] enemies = Physics.OverlapSphere(player.transform.position, 5f); // Adjust the radius as needed
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("boss") || enemy.CompareTag("mob"))
            {
                enemy.GetComponent<EnemyManager>().Slow(slowFactor, slowDuration);
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        spriteRenderer.material.SetFloat("_Transparency", alpha);
    }
}
