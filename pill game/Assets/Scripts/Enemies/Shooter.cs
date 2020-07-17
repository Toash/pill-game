using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Shooter : Enemy
{
    [SerializeField] protected float _shooterHealth = 100f;
    [SerializeField] private GameObject _bullet;
    
    [SerializeField] private float _shootingDirectionRandomness = .4f;
    [SerializeField] private float _shooterBulletSpeed = 50f;
    [SerializeField] private float _shootSpeed;
    
    [SerializeField] private int _bulletDamage;
    [SerializeField] private bool _shootsDestroyableBullets;
    
    public float _viewDistance = 25f;
    public float _distanceToShoot = 20f;

    

    void Awake()
    {
        _health = _shooterHealth;
    }


    public void StopCoroutines()
    {
        StopAllCoroutines();
    }

    public override void Attack()
    {
        StopAllCoroutines();
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (_anim.GetBool("isShooting"))
        {
            AudioManager.PlaySoundAtPosition(AudioManager.instance.GunShotSFX, 1, this.transform.position,
                AudioManager.instance.Mixer.FindMatchingGroups("Enemy")[0]);
            GameObject bullet = Instantiate(_bullet, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            Bullet bulletRef = bullet.GetComponent<Bullet>();
            bulletRef._damage = _bulletDamage;
            bulletRef._bulletSpeed = _shooterBulletSpeed;
            bulletRef._destroyable = _shootsDestroyableBullets;
            bulletRef._randomNumber = Random.Range(0, _shootingDirectionRandomness);

            yield return new WaitForSeconds(_shootSpeed);
        }


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
