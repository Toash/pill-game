using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    protected override void Fire()
    {
        base.Fire();
        _nextFire = Time.time + _fireRate;
        ModelRecoil();
        if (_cam != null                 &&!PauseMenu.isPaused)
        {
            AudioManager.PlaySound(AudioManager.instance.GunShotSFX, .1f);

            /*GameObject bullet = Instantiate(_bullet, _cam.transform.position, Quaternion.Euler(_cam.transform.forward));

            Rigidbody _bulletRB = bullet.GetComponent<Rigidbody>();
            _bulletRB.AddForce(_cam.transform.forward * 100,ForceMode.Impulse);*/
            

            Ray ray;
            RaycastHit hit;
            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("hit enemy");
                    ParticleManager.PlayParticleAtPosition(ParticleManager.instance.HitFX,hit.point,Quaternion.identity);
                    hit.transform.GetComponent<Enemy>().TakeDamage(_damage);
                }
                if (!hit.transform.CompareTag("Enemy"))
                {
                    if (!hit.transform.CompareTag("DynamicObject"))
                    {
                        Instantiate(_bulletHole, hit.point, Quaternion.LookRotation(hit.normal * -1f));
                    }

                    ParticleManager.PlayParticleAtPosition(ParticleManager.instance.BulletHitFX,hit.point,Quaternion.LookRotation(hit.normal * -1f));
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * 15,ForceMode.Impulse);
                }
            }
        }
    }
}
