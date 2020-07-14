using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles weapon switching
public class WeaponHolder : MonoBehaviour
{
    public int selectedWeapon = 0;

    public static bool _isSwitching;
    private Vector3 initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition,  initialPosition,
            Time.deltaTime * 6);
        
        if (_isSwitching)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0,-1,-2) + initialPosition,
                Time.deltaTime * 30);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && !Weapon._isReloading && !_isSwitching)
        {
            StartCoroutine(SwitchWeapon(0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !Weapon._isReloading && !_isSwitching)
        {
            StartCoroutine(SwitchWeapon(1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !Weapon._isReloading && !_isSwitching)
        {
            StartCoroutine(SwitchWeapon(2));
        }
    }

    

    IEnumerator SwitchWeapon(int weaponID)
    {
        _isSwitching = true;
        
        
        
        yield return new WaitForSeconds(.1f);
        selectedWeapon = weaponID;
        SelectWeapon();
        _isSwitching = false;
    }
    
    void SelectWeapon()
    {
        int i = 0;
        //transform is this object
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
