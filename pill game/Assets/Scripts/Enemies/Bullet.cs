using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [HideInInspector]public int _damage;
    [HideInInspector] public float _bulletSpeed;
    [HideInInspector] public bool _destroyable;
    [HideInInspector] public float _randomNumber;


    private Rigidbody rb;
    private bool _isClose;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,3f);
        if (_destroyable)
        {
            gameObject.layer = 0;
        }
        rb = GetComponent<Rigidbody>();
        //_randomNumber = Random.Range(-10, 10);
        //Destroy(this.gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce((GameManager.instance.player.transform.position + Vector3.down - transform.localPosition) * 500 * Time.deltaTime,
        //    ForceMode.VelocityChange);

        var dir = GameManager.instance.player.transform.position - transform.position;

        if (Vector3.Distance(GameManager.instance.player.transform.position, transform.position) > _randomNumber && !_isClose)
        {
            rb.velocity = (dir).normalized * _bulletSpeed;
        }
        else
        {
            _isClose = true;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //Debug.Log("hit the player");
            var playerRef = other.gameObject.GetComponent<Player>();
            playerRef.PlayerDamage(_damage);
            Destroy(this.gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //hit a wall or something
        if (!other.transform.CompareTag("Enemy") && !other.transform.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }

        if (other.transform.CompareTag("Shield"))
        {
            other.transform.GetComponent<Shield>().HitShield();
            Destroy(this.gameObject);
        }
    }

    /*private void OnDestroy()
    {
        ParticleManager.PlayParticleAtPosition(ParticleManager.instance.BulletHitFX,transform.position,Quaternion.identity);
    }*/
}
