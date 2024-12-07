using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is Player1 or Player2
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            TeleportBothPlayers();
        }
    }

    private void TeleportBothPlayers()
    {
        GameObject player1 = GameObject.FindWithTag("Player1");
        GameObject player2 = GameObject.FindWithTag("Player2");

        if (player1 != null)
        {
            Vector3 player1NewPosition = destination.position + new Vector3(1.0f, 0.0f, 1.0f);
            player1NewPosition.y = 0.0f; // Ensure y position is 0
            player1.transform.position = player1NewPosition;
            if (player1.TryGetComponent<Rigidbody>(out Rigidbody rb1))
            {
                rb1.velocity = Vector3.zero;
            }
        }

        if (player2 != null)
        {
            Vector3 player2NewPosition = destination.position + new Vector3(-1.0f, 0.0f, -1.0f);
            player2NewPosition.y = 0.0f; // Ensure y position is 0
            player2.transform.position = player2NewPosition;
            if (player2.TryGetComponent<Rigidbody>(out Rigidbody rb2))
            {
                rb2.velocity = Vector3.zero;
            }
        }
    }
}
