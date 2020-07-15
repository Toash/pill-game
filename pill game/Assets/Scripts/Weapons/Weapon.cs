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
    
    
    

    //[SerializeField] protected GameObject _bullet;


    protected int _currentAmount;
    protected float _nextFire;
    protected float _defaultFOV;
    public static bool _isReloading;
    
    
    
    protected Vector3 initialPosition;
    
    




    protected void ModelRecoil()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, -_modelRecoil + initialPosition,
            Time.deltaTime * 30);
    }
    
    protected void MeleeRecoil()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0,-3,0) + initialPosition,
            Time.deltaTime * 30);
    }


    protected void MakeCameraWork()
    {
        _cam.transform.localRotation = Quaternion.Euler(new Vector3(Player.xRotation,0,0)+CameraSway.SwayRot);
    }


    private void Awake()
    {
        //_cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void OnEnable()
    {
        
    }

    protected virtual  void Start()
    {
        if (!_cam)
        {
            _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        _defaultFOV = _cam.fieldOfView;
        initialPosition = transform.localPosition;
    }


    protected virtual void Fire()
    {

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

    protected Vector3 GetRandomDirection(float randomness)
    {
        Vector3 direction = new Vector3(Random.Range(-randomness,randomness),Random.Range(-randomness,randomness),Random.Range(-randomness,randomness)
        );

        return direction;

    }

}
