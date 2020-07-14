using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int _damage;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("hit the player");
            var playerRef = other.gameObject.GetComponent<Player>();
            playerRef.PlayerDamage(_damage);
            Destroy(this.gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //hit a wall or something
        if (!other.transform.CompareTag("Enemy") && !other.transform.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
