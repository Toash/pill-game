using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    [SerializeField] protected float _zombieHealth = 100f;


    void Awake()
    {
        _health = _zombieHealth;
    }
    


    public override void GoToPlayer()
    {
        
            _agent.SetDestination(_player.transform.position);
        
    }

    protected override void Die()
    {
        AudioManager.PlaySoundAtPosition(AudioManager.instance.EnemyDeathSFX,1,this.transform, AudioManager.instance.Mixer.FindMatchingGroups("Enemy")[0]);
        AudioManager.PlaySoundAtPosition(AudioManager.instance.SplatSFX,10,this.transform,AudioManager.instance.Mixer.FindMatchingGroups("Enemy")[0]);
        ParticleManager.PlayParticleAtPosition(ParticleManager.instance.EnemyDeathFX,this.transform.position,Quaternion.identity);
        ParticleManager.PlayParticleAtPosition(ParticleManager.instance.BloodMistFX,this.transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var _playerComponent = _player.GetComponent<Player>();
            
            _playerComponent.PlayerDeath();
        }
    }
}
