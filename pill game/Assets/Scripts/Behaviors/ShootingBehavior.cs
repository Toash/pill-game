using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingBehavior : StateMachineBehaviour
{
    private Transform _playerPos;
    private Shooter _zombie;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Shooter>().Attack();
        _playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _zombie = animator.GetComponent<Shooter>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit hit;
        LayerMask notenemyMask =~ LayerMask.GetMask("Enemy");
        Debug.DrawRay(animator.transform.position + Vector3.up * 2, _playerPos.transform.position - (animator.transform.position + Vector3.up * 2), Color.red);
        if (Physics.Raycast(animator.transform.position + Vector3.up * 2, _playerPos.transform.position - (animator.transform.position + Vector3.up * 2),
            out hit,1000,GameManager.instance._enemyLayerMask))
            
        {
            if (!hit.transform.CompareTag("Player") && !hit.transform.CompareTag("Shield") && !hit.transform.CompareTag("Bullet") && !hit.transform.CompareTag("Weapon"))
            {
                Debug.Log("player hideing");
                //Debug.Log(hit.transform.name);
                animator.SetBool("isShooting", false);
                animator.SetBool("isFollowing", true);
            }
        }



        //Debug.Log("shooting");
        //when distance from enemy to player is greater than value
        if (Vector3.Distance(animator.transform.position, GameManager.instance.player.transform.position ) > _zombie._distanceToShoot)
        {
            animator.GetComponent<NavMeshAgent>().isStopped = false;
            animator.GetComponent<NavMeshAgent>().SetDestination(_playerPos.position);
        }
        else
        {
            Debug.Log("bruhhhhh");
            animator.GetComponent<NavMeshAgent>().isStopped = true;
            var lookPos = GameManager.instance.player.transform.position - animator.transform.position;
            lookPos.y = 0;
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 500);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Shooter>().StopCoroutines();
        animator.GetComponent<NavMeshAgent>().isStopped = false;
    }

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
