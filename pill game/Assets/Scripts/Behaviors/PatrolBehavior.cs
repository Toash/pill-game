using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : StateMachineBehaviour
{

    [SerializeField] private float _fieldOfView = 165;
    [SerializeField] private float _viewDistance = 25f;
    
    private GameObject _playerPos;
    private NavMeshAgent _agent;

    private bool _alerted;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player");
        _agent = animator.GetComponent<NavMeshAgent>();
        
        animator.GetComponent<Enemy>().DelayedSetDestination();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var _toPlayer = (animator.transform.position - _playerPos.transform.position);
        float angleToPlayer = (Vector3.Angle(_toPlayer, -animator.transform.forward));

        if (angleToPlayer >= -(_fieldOfView/2) && angleToPlayer <= (_fieldOfView/2) && !_alerted)
        {
            RaycastHit hit;
            if(Physics.Raycast(animator.transform.position, _playerPos.transform.position - animator.transform.position,out hit,_viewDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    _alerted = true;
                    AudioManager.PlaySound(AudioManager.instance.AlertedSFX, .05f);
                    animator.SetBool("isFollowing", true);
                    animator.SetBool("isPatrolling",false);
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
