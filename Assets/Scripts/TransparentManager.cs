using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float transparency = 0.5f;

    private SpriteRenderer spriteRenderer;
    private List<GameObject> playersInTrigger = new List<GameObject>();
    private int counter;
    // private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player1 = GameObject.FindWithTag("Player1").transform;
        player2 = GameObject.FindWithTag("Player2").transform;
    }

    // Update is called once per frame
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        playersInTrigger.RemoveAll(player => player == null || !player.activeInHierarchy);
        if (playersInTrigger.Count == 0)
        {
            spriteRenderer.material.SetFloat("_Transparency", 1.0f);
            
        }
        
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2")) {
            Debug.Log("Player visually blocked by boss");
            playersInTrigger.Add(other.gameObject);
            spriteRenderer.material.SetFloat("_Transparency", transparency);

        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2")) {
            Debug.Log("Player exited boss collider");
            playersInTrigger.Remove(other.gameObject);
        }
    }
}
