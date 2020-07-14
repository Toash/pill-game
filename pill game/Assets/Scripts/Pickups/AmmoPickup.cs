using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{


    private GameObject _weaponHolder;
    [SerializeField] private int _weaponID;


    private bool doLoop;
    private Light _light;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        _light = GetComponent<Light>();
    }

    void Start()
    {
        doLoop = true;
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

        _light.intensity = Mathf.PingPong(Time.time * 10, 4);
        _light.range = Mathf.PingPong(Time.time * 5, 2);


    }

    
    public void AddPickup(int amount)
    {
        int i = 0;
        foreach (Transform weapon in _weaponHolder.transform)
        {

            if (i == _weaponID)
            {
                weapon.GetComponent<Gun>().AddAmmo(amount);
            }
            i++;
        }
    }
}
