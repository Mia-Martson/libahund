using System.Collections;
using UnityEngine;

public class boss_singular_bullet_circle_attack : StateMachineBehaviour
{
    private boss boss; // Reference to the boss script
    private Coroutine attackCoroutine; // To track the coroutine instance
    public float FireRate = 0.2f; // Time between each bullet
    public int NumberOfBullets = 20; // Total number of bullets to fire in a circle


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Singular Bullet Circle Attack state entered");

        // Get the boss component
        boss = animator.GetComponent<boss>();

        // Start the SingularBulletCircleAttack coroutine
        if (boss != null)
        {
            attackCoroutine = boss.StartCoroutine(boss.SingularBulletCircleAttack(FireRate, NumberOfBullets));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting Singular Bullet Circle Attack state");

        // Stop the coroutine when exiting the state
        if (boss != null && attackCoroutine != null)
        {
            boss.StopCoroutine(attackCoroutine);
        }
    }
}
