using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_laser_attack : StateMachineBehaviour
{
    boss boss;
    public float laserDuration = 4f; // Duration for the laser attack
    private float laserTimer = 0f; // Timer to track the laser duration

    public AudioClip screamSound;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("laser attack state reached");
        boss = animator.GetComponent<boss>();
        laserTimer = 0f; // Reset the timer
        boss.LaserAttack(); // Trigger the laser attack
        SoundManager.Instance.PlaySFX(screamSound);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        laserTimer += Time.deltaTime; // Increment the timer

        // Check if the laser duration has elapsed
        if (laserTimer >= laserDuration)
        {
            // Check if the laser duration has elapsed
            if (laserTimer >= laserDuration)
            {
                // Clean up the lasers immediately
                CleanupLasers();

                // Transition to the next state or end the laser attack
                animator.SetTrigger("EnragedIdle"); // Replace with your actual state transition trigger
                animator.ResetTrigger("Enraged");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Clean up the lasers when exiting the state
        foreach (GameObject laser in boss.activeLasers)
        {
            Destroy(laser);
        }
        boss.activeLasers.Clear();
    }

    // Method to clean up lasers
    private void CleanupLasers()
    {
        if (boss.activeLasers != null && boss.activeLasers.Count > 0)
        {
            foreach (GameObject laser in boss.activeLasers)
            {
                Destroy(laser);
            }
            boss.activeLasers.Clear();
        }
    }
}

