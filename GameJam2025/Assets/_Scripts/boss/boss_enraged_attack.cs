using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_enraged_attack : StateMachineBehaviour
{
    boss boss;
    private int attackCount = 0; // Counter for the number of attacks
    private float attackTimer = 0f; // Timer for attack intervals
    public int maxAttacks = 5; // Maximum number of attacks
    public float attackInterval = 0.1f; // Time between attacks

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("standard attack state reached");
        boss = animator.GetComponent<boss>();
        attackCount = 0; // Reset the attack counter when entering the state
        attackTimer = 0f; // Reset the timer
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackCount < maxAttacks)
        {
            attackTimer += Time.deltaTime; // Increment the timer based on elapsed time

            if (attackTimer >= attackInterval) // Check if the interval has passed
            {
                boss.StandardAttack(); // Perform the attack
                attackCount++; // Increment the attack counter
                attackTimer = 0f; // Reset the timer for the next attack
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
