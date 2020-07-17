using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    
    protected Camera _cam;
    
    
    [SerializeField] protected bool _semi;
    
    [SerializeField] protected float _fireRate;

    [SerializeField] protected float _damage;

    [SerializeField] protected GameObject _bulletHole;

    [SerializeField] private Vector3 _modelRecoil;

    private GameObject _weaponHolder;
    
    

    //[SerializeField] protected GameObject _bullet;


    protected int _currentAmount;
    protected float _nextFire;
    public static bool _isReloading;
    
    
    
    protected Vector3 initialPosition;


    private void Awake()
    {
        _weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }


    protected void ModelRecoil()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, -_modelRecoil + initialPosition,
            Time.deltaTime * 30);
    }
    
    

    protected virtual  void Start()
    {
        if (!_cam)
        {
            _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        
        initialPosition = transform.localPosition;
    }


    protected virtual void Fire()
    {

    }


    public void AddWeapon(int weaponToAdd)
    {
        switch (weaponToAdd)
        {
            case 0:
                Player.hasShield = true;
                break;
            case 1:
                Player.hasMelee = true;
                break;
            case 2:
                Player.hasPistol = true;
                break;
            case 3:
                Player.hasSmg = true;
                break;
            case 4:
                Player.hasShotgun = true;
                break;
        }
    }
    
    
    
    
    protected virtual void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetMouseButtonDown(0) && _semi && Time.time > _nextFire && !_isReloading)
            {
                Fire();
            }

            if (Input.GetMouseButton(0) && !_semi && Time.time > _nextFire && !_isReloading)
            {
                Fire();
            }
            

            if (_isReloading)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition,
                    initialPosition + new Vector3(0, -1f, 0), Time.deltaTime * 5);
            }
        }
    }
    

}
