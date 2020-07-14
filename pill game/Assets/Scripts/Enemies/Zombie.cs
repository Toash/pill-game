using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Zombie : Enemy
{
    [SerializeField] protected float _zombieHealth = 100f;
    [SerializeField] private GameObject _bullet;

    [SerializeField] private int _bulletDamage;
    

    

    void Awake()
    {
        _health = _zombieHealth;
    }
    


    public override void GoToPlayer()
    {
        
            _agent.SetDestination(_player.transform.position);
        
    }

    public override void Attack()
    {
        StopAllCoroutines();
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        Animator _anim = GetComponent<Animator>();
        
        while (_anim.GetBool("isShooting"))
        {
            yield return new WaitForSeconds(Random.Range(.9f,1.1f));
            GameObject bullet = Instantiate(_bullet, transform.position + Vector3.up, Quaternion.identity);
            bullet.GetComponent<Bullet>()._damage = _bulletDamage;
            var bulletRigidybody = bullet.GetComponent<Rigidbody>();
            bulletRigidybody.AddForce(((_player.transform.position+Vector3.down)- transform.position).normalized * 25,ForceMode.Impulse);
        }
    }

    protected override void Die()
    {
        AudioManager.PlaySoundAtPosition(AudioManager.instance.EnemyDeathSFX,1,this.transform.position, AudioManager.instance.Mixer.FindMatchingGroups("Enemy")[0]);
        AudioManager.PlaySoundAtPosition(AudioManager.instance.SplatSFX,10,this.transform.position,AudioManager.instance.Mixer.FindMatchingGroups("Enemy")[0]);
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
        if (other.CompareTag("Sensor"))
        {
            enteredTrigger = true;
        }
    }

}
