﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : Weapon
{

    [SerializeField] private bool _silenced;
    
    [SerializeField] protected int _magAmount = 30;

    [SerializeField] protected int _reserveAmmo = 200;

    [SerializeField] protected Transform _muzzlePosition;

    
    [SerializeField] protected GameObject _muzzleLightGameObject;
    
    
    [SerializeField] private float _rotationSpeed = 6f;
    
    [SerializeField] private float _returnSpeed = 25f;

    [SerializeField] private Vector3 _recoilRotation = new Vector3(2,2,2);
    
    [SerializeField] private Vector3 _recoilRotationAiming = new Vector3(.5f,.5f,1.5f);
    
    [SerializeField] protected float _zoomFOV;

    [SerializeField] private float _soundRange;
    
    
    private Vector3 currentRotation;
    private Vector3 Rot;
    private Player player;

    private void Awake()
    {
        _currentAmount = _magAmount;
    }

    protected override void Start()
    {
        base.Start();
        UIManager.instance.UpdateUI(UIManager.instance._ammoText, "AMMO: " + _currentAmount.ToString());
        player = GameManager.instance.player.GetComponent<Player>();
    }
    
    void RecoilAndUpDownMouseMovement()
    {
        //lerps float current rotation from zero to return speed
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        //slerps float rot from current rotation to rot speed
        Rot = Vector3.Slerp(Rot, currentRotation, _rotationSpeed * Time.fixedDeltaTime);
        _cam.transform.localRotation = Quaternion.Euler(new Vector3(Player.xRotation,0,0) + Rot + CameraSway.SwayRot);
    }
    
    private void KickbackRecoilAiming()
    {
        currentRotation += new Vector3(-_recoilRotationAiming.x, Random.Range(-_recoilRotationAiming.y, _recoilRotationAiming.y), Random.Range(-_recoilRotationAiming.z,_recoilRotationAiming.z));
    }

    private void KickbackRecoil()
    {
        currentRotation += new Vector3(-_recoilRotation.x, Random.Range(-_recoilRotation.y,_recoilRotation.y), Random.Range(-_recoilRotation.z,_recoilRotation.z));
    }
    
    private void Zoom()
    {
        if (Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-.5f,0,0) + initialPosition,
                Time.deltaTime * 30);
            
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _defaultFOV - _zoomFOV,25 * Time.deltaTime);
        }
        else
        {
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _defaultFOV,15 * Time.deltaTime);
        }
    }

    protected override void Update()
    {
        base.Update();
        RecoilAndUpDownMouseMovement();
        UIManager.instance.UpdateUI(UIManager.instance._reserveText, "RESERVE: " + _reserveAmmo.ToString());
        Zoom();
        if (Input.GetButtonDown("Reload") && !_isReloading && _currentAmount != _magAmount && _reserveAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }
    
    
    //make sure the ui is updated when switching weapons
    private void OnEnable()
    {
        UIManager.instance.UpdateUI(UIManager.instance._ammoText, "AMMO: " + _currentAmount.ToString());
    }
    
    public void AddAmmo(int amount)
    {
        _reserveAmmo += amount;
    }
    
    
    protected override void Fire()
    {
        _nextFire = Time.time + _fireRate;
        if (_currentAmount > 0 && !WeaponHolder._isSwitching)
        {
            _currentAmount--;
            UIManager.instance.UpdateUI(UIManager.instance._ammoText, "AMMO: " + _currentAmount.ToString());
            StartCoroutine(MuzzleFlash());
            
            
            
            player.ActivateSensorTrigger(_soundRange);
            
            
            if (Player._isAiming || Player._isCrouching)
            {
            
                KickbackRecoilAiming();
            }
            else
            {
                KickbackRecoil();
            }
            ModelRecoil();
            if (!_silenced)
            {
                    AudioManager.PlaySound(AudioManager.instance.GunShotSFX, .1f);
            }

            if (_silenced)
            {
                    AudioManager.PlaySound(AudioManager.instance.SilencedGunShotSFX, .1f);
            }

            /*GameObject bullet = Instantiate(_bullet, _cam.transform.position, Quaternion.Euler(_cam.transform.forward));
    
            Rigidbody _bulletRB = bullet.GetComponent<Rigidbody>();
            _bulletRB.AddForce(_cam.transform.forward * 100,ForceMode.Impulse);*/


            Ray ray;
            RaycastHit hit;
            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit))
            {
                ParticleManager.PlayParticleAtPosition(ParticleManager.instance.BulletTracer, _muzzlePosition.transform.position, 
                    Quaternion.LookRotation(hit.point - _muzzlePosition.transform.position).normalized);
                if (hit.transform.CompareTag("Enemy"))
                {
                    ParticleManager.PlayParticleAtPosition(ParticleManager.instance.HitFX, hit.point,
                        Quaternion.LookRotation(hit.normal * -1f));
                    hit.transform.GetComponent<Enemy>().TakeDamage(_damage);
                }

                if (!hit.transform.CompareTag("Enemy"))
                {
                    if (!hit.transform.CompareTag("DynamicObject"))
                    {
                        var bullethole = Instantiate(_bulletHole, hit.point, Quaternion.LookRotation(hit.normal * -1f));
                        Destroy(bullethole.gameObject, 3);
                    }

                    ParticleManager.PlayParticleAtPosition(ParticleManager.instance.BulletHitFX, hit.point,
                        Quaternion.LookRotation(hit.normal * -1f));
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * 15, ForceMode.Impulse);
                }
            }
            
        }
        else
        {
            AudioManager.PlaySound(AudioManager.instance.GunTickSFX, .1f);
        }
    }
    
    
    IEnumerator Reload()
    {
        AudioManager.PlaySound(AudioManager.instance.ReloadSFX, .25f);
        _isReloading = true;
        
               


        yield return new WaitForSeconds(.6f);
        _isReloading = false;



        var _roundsNeeded = _magAmount - _currentAmount;


        if (_roundsNeeded <= _reserveAmmo)
        {
            _currentAmount += _roundsNeeded;
            _reserveAmmo -= _roundsNeeded;
        }
        else
        {
            _currentAmount += _reserveAmmo;
            _reserveAmmo = 0;
        }




        
        UIManager.instance.UpdateUI(UIManager.instance._ammoText, "AMMO: " + _currentAmount.ToString());
    }
    
    IEnumerator MuzzleFlash()
    {
        _muzzleLightGameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        _muzzleLightGameObject.SetActive(false);
    }
    
}
