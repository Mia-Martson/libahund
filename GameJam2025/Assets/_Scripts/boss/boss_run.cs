using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_run : StateMachineBehaviour
{
    public float speed = 2f;
    public float attackRange = 15f; // Range within which the boss decides to attack
    public float attackCooldown = 5f; // Cooldown for any attack
    private float attackCooldownTimer = 0f; // Tracks time since the last attack

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        attackCooldownTimer = attackCooldown; // Ensure the boss can attack immediately if needed
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Movement logic
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // Increment cooldown timer
        attackCooldownTimer += Time.deltaTime;

        // Decide on attack if within range and cooldown has elapsed
        if (Vector2.Distance(player.position, rb.position) <= attackRange && attackCooldownTimer >= attackCooldown)
        {
            // Randomly decide the attack
            int attackDecision = Random.Range(0, 3); // Random number between 0 and 2
            switch (attackDecision)
            {
                case 0:
                    animator.SetTrigger("StandardAttack"); // Perform Standard Attack
                    break;
                case 1:
                    animator.SetTrigger("CircularAttack"); // Perform Circular Attack
                    break;
                case 2:
                    animator.SetTrigger("SingularBulletAttack"); // Perform Singular Bullet Circle Attack
                    break;
            }

            attackCooldownTimer = 0f; // Reset cooldown
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("StandardAttack");
        animator.ResetTrigger("CircularAttack");
        animator.ResetTrigger("SingularBulletAttack");
    }
}
