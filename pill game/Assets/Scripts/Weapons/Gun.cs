using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    protected override void Fire()
    {
        base.Fire();
        _nextFire = Time.time + _fireRate;
        if (_cam != null                 &&!PauseMenu.isPaused)
        {
            AudioManager.PlaySound(AudioManager.instance.GunShotSFX, .1f);
            Ray ray;
            RaycastHit hit;
            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("hit enemy");
                    hit.transform.GetComponent<Enemy>().TakeDamage(50f);
                }
            }
        }
    }
}
