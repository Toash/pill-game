using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{


    private GameObject _weaponHolder;
    [SerializeField] private int _weaponID;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (Transform bulletMesh in transform)
        {
            if (i == _weaponID)
            {
                bulletMesh.gameObject.SetActive(true);
            }
            else
            {
                bulletMesh.gameObject.SetActive(false);
            }

            i++;
        }
    }

    
    public void AddPickup(int amount)
    {
        int i = 0;
        foreach (Transform weapon in _weaponHolder.transform)
        {

            if (i == _weaponID)
            {
                weapon.GetComponent<Weapon>().AddAmmo(amount);
            }
            i++;
        }
    }
}
