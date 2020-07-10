using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected Camera _cam = null;
    [SerializeField] protected float _zoomFOV;
    [SerializeField] protected bool _semi;
    [SerializeField] protected float _fireRate;

    protected float _nextFire;
    
    
    protected float _defaultFOV;
    protected bool _aiming;
    
    
    [SerializeField] private float _rotationSpeed = 6f;
    [SerializeField] private float _returnSpeed = 25f;

    [SerializeField] private Vector3 _recoilRotation = new Vector3(2,2,2);
    
    [SerializeField] private Vector3 _recoilRotationAiming = new Vector3(.5f,.5f,1.5f);

    private Vector3 currentRotation;
    private Vector3 Rot;
    
    void RecoilAndUpDownMouseMovement()
    {
        //lerps float current rotation from zero to return speed
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        //slerps float rot from current rotation to rot speed
        Rot = Vector3.Slerp(Rot, currentRotation, _rotationSpeed * Time.fixedDeltaTime);
        _cam.transform.localRotation = Quaternion.Euler(new Vector3(Player.xRotation,0,0) + Rot);
    }
    

    protected void Start()
    {
        _cam = GameManager.instance.cam;
        _defaultFOV = _cam.fieldOfView;
    }

    protected virtual void Fire()
    {
        if (_aiming)
        {
            currentRotation += new Vector3(-_recoilRotationAiming.x, Random.Range(-_recoilRotationAiming.y, _recoilRotationAiming.y), Random.Range(-_recoilRotationAiming.z,_recoilRotationAiming.z));
        }
        else
        {
            currentRotation += new Vector3(-_recoilRotation.x, Random.Range(-_recoilRotation.y,_recoilRotation.y), Random.Range(-_recoilRotation.z,_recoilRotation.z));
        }
    }

    protected void Zoom()
    {
        if (Input.GetMouseButton(1))
        {
            _aiming = true;
            _cam.fieldOfView = _zoomFOV;
        }
        else
        {
            _aiming = false;
            _cam.fieldOfView = _defaultFOV;
        }
    }
    
    protected void Update()
    {
        RecoilAndUpDownMouseMovement();
        Zoom();
        if (Input.GetMouseButtonDown(0) && _semi)
        {
            Fire();
        }
        if (Input.GetMouseButton(0) && !_semi && Time.time > _nextFire)
        {
            Fire();
        }
    }
    
}
