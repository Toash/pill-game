using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    protected Camera _cam = null;
    
    [SerializeField] protected float _zoomFOV;
    
    [SerializeField] protected bool _semi;
    
    [SerializeField] protected float _fireRate;

    [SerializeField] protected float _damage;

    [SerializeField] protected GameObject _bulletHole;

    [SerializeField] protected int _magAmount = 30;

    [SerializeField] protected int _reserveAmmo = 200;
    
    

    //[SerializeField] protected GameObject _bullet;


    protected int _currentAmount;
    protected float _nextFire;
    protected float _defaultFOV;
    public static bool _isReloading;
    
    
    
    
    [SerializeField] private float _rotationSpeed = 6f;
    
    [SerializeField] private float _returnSpeed = 25f;

    [SerializeField] private Vector3 _recoilRotation = new Vector3(2,2,2);
    
    [SerializeField] private Vector3 _recoilRotationAiming = new Vector3(.5f,.5f,1.5f);
    
    
    private Vector3 currentRotation;
    private Vector3 Rot;
    private Vector3 initialPosition;
    

    [SerializeField] private GameObject _muzzleLightGameObject;




    protected void ModelRecoil()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0,0,-2) + initialPosition,
            Time.deltaTime * 30);
    }
    
    
    
    
    void RecoilAndUpDownMouseMovement()
    {
        //lerps float current rotation from zero to return speed
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        //slerps float rot from current rotation to rot speed
        Rot = Vector3.Slerp(Rot, currentRotation, _rotationSpeed * Time.fixedDeltaTime);
        _cam.transform.localRotation = Quaternion.Euler(new Vector3(Player.xRotation,0,0) + Rot + CameraSway.SwayRot);
    }
    

    protected void Start()
    {
        _cam = GameManager.instance.cam;
        _defaultFOV = _cam.fieldOfView;
        initialPosition = transform.localPosition;
        //sets current amount to the mag amount
        _currentAmount = _magAmount;
        UIManager.instance.UpdateUI(UIManager.instance._ammoText, "AMMO: " + _currentAmount.ToString());
    }

    
    //make sure the ui is updated when switching weapons
    private void OnEnable()
    {
        UIManager.instance.UpdateUI(UIManager.instance._ammoText, "AMMO: " + _currentAmount.ToString());
    }

    protected virtual void Fire()
    {
        StartCoroutine(MuzzleFlash());
        _currentAmount--;
        UIManager.instance.UpdateUI(UIManager.instance._ammoText, "AMMO: " + _currentAmount.ToString());
        if (Player._isAiming || Player._isCrouching)
        {
            
            KickbackRecoilAiming();
        }
        else
        {
            KickbackRecoil();
        }
    }

    private void KickbackRecoilAiming()
    {
        currentRotation += new Vector3(-_recoilRotationAiming.x, Random.Range(-_recoilRotationAiming.y, _recoilRotationAiming.y), Random.Range(-_recoilRotationAiming.z,_recoilRotationAiming.z));
    }

    private void KickbackRecoil()
    {
        currentRotation += new Vector3(-_recoilRotation.x, Random.Range(-_recoilRotation.y,_recoilRotation.y), Random.Range(-_recoilRotation.z,_recoilRotation.z));
    }
    
    

    protected void Zoom()
    {
        if (Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-.5f,0,0) + initialPosition,
                Time.deltaTime * 30);
            
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _defaultFOV - _zoomFOV,5 * Time.deltaTime);
        }
        else
        {
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _defaultFOV,5 * Time.deltaTime);
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
    
    protected void Update()
    {
        UIManager.instance.UpdateUI(UIManager.instance._reserveText, "RESERVE: " + _reserveAmmo.ToString());
        if (!PauseMenu.isPaused)
        {
            RecoilAndUpDownMouseMovement();
            Zoom();
            if (Input.GetMouseButtonDown(0) && _semi && !_isReloading)
            {
                Fire();
            }

            if (Input.GetMouseButton(0) && !_semi && Time.time > _nextFire && !_isReloading)
            {
                Fire();
            }

            if (Input.GetButtonDown("Reload") && !_isReloading && _currentAmount != _magAmount && _reserveAmmo > 0)
            {
                StartCoroutine(Reload());
            }

            if (_isReloading)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition,
                    initialPosition + new Vector3(0, -1f, 0), Time.deltaTime * 5);
            }
        }
    }

    IEnumerator MuzzleFlash()
    {
        _muzzleLightGameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        _muzzleLightGameObject.SetActive(false);
    }
    
    
    public void AddAmmo(int amount)
    {
        _reserveAmmo += amount;
    }
}
