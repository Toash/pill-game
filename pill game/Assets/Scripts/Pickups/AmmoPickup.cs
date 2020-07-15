using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class AmmoPickup : MonoBehaviour
{


    private GameObject _weaponHolder;
    [SerializeField] private int _weaponID;
    [SerializeField] private int _amount = 10;


    private bool doLoop;
    private Light _light;
    private float _randomTime;
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        _weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        _light = GetComponent<Light>();
    }

    void Start()
    {
        doLoop = true;
        transform.position += Vector3.up * .1f;
        _randomTime = Random.Range(9f, 11f);
        
    }

    // Update is called once per frame
    void Update()
    {
        while (doLoop)
        {
            int i = 0;
            foreach (Transform bulletMesh in transform)
            {
                if (i == _weaponID)
                {
                    bulletMesh.gameObject.SetActive(true);
                    doLoop = false;
                }
                else
                {
                    bulletMesh.gameObject.SetActive(false);
                }

                i++;
            }
        }

        _light.intensity = Mathf.PingPong(Time.time * _randomTime, 4);
        _light.range = Mathf.PingPong(Time.time * 5, 2);


    }

    
    public void AddPickup()
    {
        int i = 0;
        foreach (Transform weapon in _weaponHolder.transform)
        {

            if (i == _weaponID)
            {
                weapon.GetComponent<Gun>().AddAmmo(_amount);
                UIManager.instance.UpdatePickupText(_amount + " " + weapon.name.ToString() + " ammo");
            }
            i++;
        }
    }
}
