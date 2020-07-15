using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowBehavior : StateMachineBehaviour
{
    private Transform _playerPos;
    private Enemy _enemy;
    private Shooter _zombie;
    [SerializeField] private float _distanceUntilShooting = 20f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.GetComponent<Enemy>();
        _zombie = animator.GetComponent<Shooter>();
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.position = Vector3.MoveTowards(animator.transform.position, _playerPos.position,
            //_enemySpeed * Time.deltaTime);
            
            _enemy.GoToPlayer();
            if (Vector3.Distance(animator.transform.position, _playerPos.position) < _zombie._distanceToShoot)
            {
                RaycastHit hit;
                LayerMask notsensorMask =~ LayerMask.GetMask("Sensor");
                
                if(Physics.Raycast(animator.transform.position + (Vector3.up * 1), _playerPos.transform.position - animator.transform.position,out hit))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        //Debug.Log("raycasthittheplayer!");
                        animator.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
                        animator.SetBool("isShooting", true);
                        animator.SetBool("isFollowing", false);
                    }
                }
            }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
