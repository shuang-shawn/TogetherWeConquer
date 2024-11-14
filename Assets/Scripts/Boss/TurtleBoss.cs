using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBoss : MonoBehaviour
{
    [Header("Rage Settings")]
    public float rageDuration = 5f; // Duration in seconds for the rage state
    public float chillDuration = 5f;
    private Animator animator;
    private bool isSpikeOut = false;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpikeCycle());
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<ProjectileRingAttack>().ExecuteProjectileRingAttack();
        }  
    }

        // Coroutine to handle the rage state cycle
    private IEnumerator SpikeCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(chillDuration); // Optional delay before entering rage mode

            EnterSpikeMode();

            yield return new WaitForSeconds(rageDuration); // Wait for the rage duration

            ExitSpikeMode();

            
        }
    }

    private void EnterSpikeMode()
    {
        animator.SetTrigger("spike_out");
        isSpikeOut = true;
        // Debug.Log("Boss has entered spike mode!");
    }

    private void ExitSpikeMode()
    {
        animator.SetTrigger("spike_in");
        isSpikeOut = false;
        // Debug.Log("Boss has exited spike mode.");
    }

    public float CalculateDamageFactor(Vector3 hitPosition) {
                // Calculate the direction vector from boss to hit position
        Vector3 hitDirection = hitPosition - transform.position;
        hitDirection.y = 0; // Ignore vertical difference to focus on 2D plane
        hitDirection.Normalize();

        // Determine the angle between the forward direction of the boss and the hit direction
        float angle = Vector3.SignedAngle(transform.forward, hitDirection, Vector3.up);
        Debug.Log("angle from turtle is: " + angle);    

        // Determine which direction (front, back, left, right) the hit came from
        if (angle >= -45 && angle <= 45)
        {
            // ReactToBackHit(damage);
            Debug.Log("back");

            return 2.0f;
        }
        else if (angle > 45 && angle <= 135)
        {
            // ReactToRightHit(damage);
            Debug.Log("right");

            return 1.0f;
        }
        else if (angle < -45 && angle >= -135)
        {
        
            // ReactToLeftHit(damage);
            Debug.Log("left");

            return 1.0f;
        }
        else
        {
            

            // ReactToFrontHit(damage);
            Debug.Log("front");
            return -1.0f;
        }
    }
}
