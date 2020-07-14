using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingBehavior : StateMachineBehaviour
{
    private Transform _playerPos;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        animator.GetComponent<Zombie>().Attack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit hit;
        int notenemyMask =~ LayerMask.GetMask("Enemy");
        if (Physics.Raycast(animator.transform.position, _playerPos.transform.position - animator.transform.position,
            out hit,1000,notenemyMask))
        {
            if (!hit.transform.CompareTag("Player"))
            {
                //Debug.Log("player hideing");
                Debug.Log(hit.transform.name);
                animator.SetBool("isShooting", false);
                animator.SetBool("isFollowing", true);
            }
        }



        //Debug.Log("shooting");
        if (Vector3.Distance(animator.transform.position, GameManager.instance.player.transform.position) > 12)
        {
            animator.GetComponent<Enemy>().GoToPlayer();
        }
        else
        {
            animator.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            var lookPos = GameManager.instance.player.transform.position - animator.transform.position;
            lookPos.y = 0;
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 5);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
