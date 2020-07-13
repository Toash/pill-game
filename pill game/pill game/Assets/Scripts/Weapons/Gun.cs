using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{

    [SerializeField] private bool _silenced;
    protected override void Fire()
    {
        _nextFire = Time.time + _fireRate;
        if (_currentAmount > 0 && !WeaponHolder._isSwitching)
        {
            base.Fire();
            ModelRecoil();
            if (_cam != null && !PauseMenu.isPaused)
            {
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
        }
        else
        {
            AudioManager.PlaySound(AudioManager.instance.GunTickSFX, .1f);
        }
    }
    
}
