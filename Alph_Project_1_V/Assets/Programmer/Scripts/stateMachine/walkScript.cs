using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkScript : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 2f;
    public float attackRange = 2f;
    boss b;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player= GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        b = animator.GetComponent<boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        b.lookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos= Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if(Vector2.Distance(player.position,rb.position)<=attackRange)
        {
            animator.SetTrigger("attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }


}
