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
        Debug.Log("Boss has entered spike mode!");
    }

    private void ExitSpikeMode()
    {
        animator.SetTrigger("spike_in");
        isSpikeOut = false;
        Debug.Log("Boss has exited spike mode.");
    }
}
