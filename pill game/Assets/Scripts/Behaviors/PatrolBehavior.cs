using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : StateMachineBehaviour
{

    private float _fieldOfView;

    private GameObject _playerPos;
    private NavMeshAgent _agent;
    private Enemy _enemy;
    private Shooter _shooter;

    private bool _alerted;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        
        _playerPos = GameObject.FindGameObjectWithTag("Player");
        _agent = animator.GetComponent<NavMeshAgent>();
        _enemy = animator.GetComponent<Enemy>();
        _shooter = animator.GetComponent<Shooter>();

        _fieldOfView = _enemy._enemyFieldOfView;

        animator.GetComponent<Enemy>().DelayedSetDestination();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var _toPlayer = (_playerPos.transform.position - animator.transform.position);
        float angleToPlayer = (Vector3.Angle(_toPlayer, animator.transform.forward));

        if (_enemy.enteredTrigger && !_alerted)
        {
            Alerted(animator);
        }

        if (_shooter._hitByPlayer && !_alerted)
        {
            Alerted(animator);
        }
        
        
        
        
        if (angleToPlayer >= -(_fieldOfView/2) && angleToPlayer <= (_fieldOfView/2) && !_alerted)
        {
            //Debug.Log("testinmg");
            RaycastHit hit;
            int notenemyMask =~ LayerMask.GetMask("Enemy");
            
            Debug.DrawRay(animator.transform.position + Vector3.up * 2, _playerPos.transform.position - (animator.transform.position + Vector3.up * 2), Color.red);
            if(Physics.Raycast(animator.transform.position + Vector3.up * 2, _playerPos.transform.position - (animator.transform.position + Vector3.up * 2),out hit,_shooter._viewDistance,GameManager.instance._enemyLayerMask))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Player"))
                {
                    //Debug.Log("alerted!");
                    Alerted(animator);
                }
            }
        }


    }

    void Alerted(Animator animator)
    {
        _alerted = true;
        AudioManager.PlaySound(AudioManager.instance.AlertedSFX, .05f);
        animator.SetBool("isFollowing", true);
        animator.SetBool("isPatrolling",false);
    }
    



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        _alerted = false;
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
