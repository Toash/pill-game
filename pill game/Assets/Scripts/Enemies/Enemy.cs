﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
        protected GameObject _player;
        protected NavMeshAgent _agent;
        protected float _health;

        protected Animator _anim;


        public bool enteredTrigger;



        private void Start()
        {
                _player = GameObject.FindGameObjectWithTag("Player");
                _agent = GetComponent<NavMeshAgent>();
                _anim = GetComponent<Animator>();
        }
        


        //function that determines how the enemy reaches the player.
        public virtual void GoToPlayer()
        {
                
        }

        protected virtual void Die()
        {

        }

        public virtual void Attack()
        {
                
        }

        protected void Update()
        {
                //if (_player != null && _agent != null)
                //{
                        //GoToPlayer();
                //}


                if (_health <= 0)
                {
                        Die();
                }
        }
        

        public void TakeDamage(float damageAmount)
        {
                _health -= damageAmount;
        }

        public void DelayedSetDestination()
        {
                //StopAllCoroutines();
                StartCoroutine(DelayedSetDestinationCoroutine());
        }
        private IEnumerator DelayedSetDestinationCoroutine()
        {
                while (true)
                {
                        Vector3 movePos = GameManager.instance.GenerateRandomMoveVector(transform.position);
                        yield return new WaitForSeconds(Random.Range(3, 7));
                        _agent.SetDestination(movePos);
                }
                        
        }
        
        


}
