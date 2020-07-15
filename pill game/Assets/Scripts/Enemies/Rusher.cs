using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rusher : Enemy
{
    
    [SerializeField] protected float _rusherHealth = 100f;
    // Start is called before the first frame update

    private void Awake()
    {
        _health = _rusherHealth;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
