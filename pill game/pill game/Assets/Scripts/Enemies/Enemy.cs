using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
        protected GameObject _player;
        protected NavMeshAgent _agent;
        protected float _health;

        private void Awake()
        {
        }

        private void Start()
        {
                _player = GameManager.instance.player;
                _agent = GetComponent<NavMeshAgent>();
        }
        


        //function that determines how the enemy reaches the player.
        protected virtual void GoToPlayer()
        {
                
        }

        protected virtual void Die()
        {

        }

        protected void Update()
        {
                if (_player != null && _agent != null)
                {
                        GoToPlayer();
                }


                if (_health <= 0)
                {
                        Die();
                }
        }
        

        public void TakeDamage(float damageAmount)
        {
                _health -= damageAmount;
        }
        
}
