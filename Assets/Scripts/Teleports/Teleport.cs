using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public float teleportCooldown = 2.0f; // Cooldown period in seconds
    private static bool player1Ready = false;
    private static bool player2Ready = false;
    private bool isTeleported = false;
    private static bool isCooldownActive = false; // Shared cooldown state for both portals
    private Collider portalCollider; // To manage portal collider state
    private SlimeBoss slimeBoss;

    private bool player1InCooldown = false;
    private bool player2InCooldown = false;

    void Start()
    {
        slimeBoss = FindObjectOfType<SlimeBoss>();
        portalCollider = GetComponent<Collider>(); // Get the portal's collider
        Debug.Log("Teleport script initialized.");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.tag);

        if (other.CompareTag("Player1") && !player1InCooldown)
        {
            Debug.Log("Starting cooldown for Player1.");
            StartCoroutine(StartPlayerCooldown("Player1"));
            player1Ready = true;
            Debug.Log("Player1 is ready.");
        }
        else if (other.CompareTag("Player2") && !player2InCooldown)
        {
            Debug.Log("Starting cooldown for Player2.");
            StartCoroutine(StartPlayerCooldown("Player2"));
            player2Ready = true;
            Debug.Log("Player2 is ready.");
        }
        else
        {
            Debug.Log("Conditions not met for teleportation: isTeleported=" + isTeleported + ", isCooldownActive=" + isCooldownActive);
        }

        // Check if both players are ready
        if (!isTeleported && !isCooldownActive && player1Ready && player2Ready)
        {
            Debug.Log("Both players ready. Initiating teleportation.");
            StartCoroutine(TeleportBothPlayers());
        }
    }

    private IEnumerator StartPlayerCooldown(string playerTag)
    {
        if (playerTag == "Player1")
        {
            player1InCooldown = true;
        }
        else if (playerTag == "Player2")
        {
            player2InCooldown = true;
        }

        yield return new WaitForSeconds(teleportCooldown);

        if (playerTag == "Player1")
        {
            player1InCooldown = false;
        }
        else if (playerTag == "Player2")
        {
            player2InCooldown = false;
        }
    }

    private IEnumerator TeleportBothPlayers()
    {
        Debug.Log("Pausing SlimeBoss...");
        PauseSlimeBoss();

        // Disable the portal collider to prevent re-triggering during cooldown
        if (portalCollider != null)
        {
            portalCollider.enabled = false;
            Debug.Log("Portal collider disabled.");
        }

        yield return new WaitForSeconds(0.5f); // Brief pause to ensure SlimeBoss is paused

        GameObject player1 = GameObject.FindWithTag("Player1");
        GameObject player2 = GameObject.FindWithTag("Player2");

        // Disable colliders instead of deactivating players
        if (player1 != null)
        {
            Collider player1Collider = player1.GetComponent<Collider>();
            if (player1Collider != null)
            {
                player1Collider.enabled = false;
                Debug.Log("Player1 collider disabled.");
            }
            else
            {
                Debug.LogWarning("Player1Collider not found!");
            }
        }
        else
        {
            Debug.LogWarning("Player1 not found!");
        }

        if (player2 != null)
        {
            Collider player2Collider = player2.GetComponent<Collider>();
            if (player2Collider != null)
            {
                player2Collider.enabled = false;
                Debug.Log("Player2 collider disabled.");
            }
            else
            {
                Debug.LogWarning("Player2Collider not found!");
            }
        }
        else
        {
            Debug.LogWarning("Player2 not found!");
        }

        yield return new WaitForSeconds(1f); // Short delay before teleportation
        Debug.Log("Starting teleportation process...");
        StartCoroutine(TeleportPlayers());
    }

    private void PauseSlimeBoss()
    {
        if (slimeBoss != null)
        {
            slimeBoss.Pause();
            Debug.Log("SlimeBoss paused.");
        }
        else
        {
            Debug.LogWarning("SlimeBoss not found!");
        }
    }

    private void ResumeSlimeBoss()
    {
        if (slimeBoss != null)
        {
            slimeBoss.Resume();
            Debug.Log("SlimeBoss resumed.");
        }
        else
        {
            Debug.LogWarning("SlimeBoss not found!");
        }
    }

    private IEnumerator TeleportPlayers()
    {
        Debug.Log("TeleportPlayers coroutine started.");
        isTeleported = true;
        isCooldownActive = true;

        GameObject player1 = GameObject.FindWithTag("Player1");
        GameObject player2 = GameObject.FindWithTag("Player2");

        // Teleport Player1
        if (player1 != null)
        {
            Vector3 destinationPosition1 = destination.position;
            destinationPosition1.y = 0.0f; // Set y to ground level
            player1.transform.position = destinationPosition1;
            player1.transform.rotation = destination.rotation;
            Debug.Log("Player1 teleported to: " + player1.transform.position);

            // Re-enable collider
            Collider player1Collider = player1.GetComponent<Collider>();
            if (player1Collider != null)
            {
                player1Collider.enabled = true;
                Debug.Log("Player1 collider re-enabled.");
            }
            else
            {
                Debug.LogWarning("Player1Collider not found!");
            }
        }
        else
        {
            Debug.LogWarning("Player1 not found!");
        }

        // Teleport Player2
        if (player2 != null)
        {
            Vector3 destinationPosition2 = destination.position;
            destinationPosition2.y = 0.0f; // Set y to ground level
            player2.transform.position = destinationPosition2;
            player2.transform.rotation = destination.rotation;
            Debug.Log("Player2 teleported to: " + player2.transform.position);

            // Re-enable collider
            Collider player2Collider = player2.GetComponent<Collider>();
            if (player2Collider != null)
            {
                player2Collider.enabled = true;
                Debug.Log("Player2 collider re-enabled.");
            }
            else
            {
                Debug.LogWarning("Player2Collider not found!");
            }
        }
        else
        {
            Debug.LogWarning("Player2 not found!");
        }

        yield return new WaitForSeconds(1f); // Short delay
        ResumeSlimeBoss(); // Resume SlimeBoss

        yield return new WaitForSeconds(teleportCooldown); // Add cooldown period

        // Re-enable the portal collider
        if (portalCollider != null)
        {
            portalCollider.enabled = true;
            Debug.Log("Portal collider re-enabled.");
        }

        isCooldownActive = false;
        isTeleported = false;
        Debug.Log("Cooldown ended, teleport ready.");
    }
}
