using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class boss_run : StateMachineBehaviour
{
    public float speed = 2f;
    public float attackRange = 5f;
    
    Transform player;
    Rigidbody2D rb;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // if (player == null)
        //{
          //  Debug.LogError("Player not found! Make sure there is a GameObject tagged 'Player' in the scene.");
        //}

        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed  * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        

        //kui peaks ründama
        //if boss should attack
        //otsustamisrünnakutteha
        //teevastavatrünnakut
        //pane vastav attack trigger
        
        if(Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}
