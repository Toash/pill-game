using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{

    [SerializeField] protected float _meleeRange = 5f;
    
    // Start is called before the first frame update
    protected override void  Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        MakeCameraWork();
    }
    
    private void OnEnable()
    {
        UIManager.instance.UpdateUI(UIManager.instance._ammoText, "");
        UIManager.instance.UpdateUI(UIManager.instance._reserveText, "");
    }

    protected override void Fire()
    {
        _nextFire = Time.time + _fireRate;
        if (!WeaponHolder._isSwitching)
        {
            AudioManager.PlaySound(AudioManager.instance.SwordSlashSFX, .1f);
            MeleeRecoil();
            RaycastHit hit;
            LayerMask _excludingPlayer = ~ LayerMask.GetMask("Player");
            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, _meleeRange,_excludingPlayer))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("Enemy"))
                {
                    AudioManager.PlaySoundAtPosition(AudioManager.instance.SplatSFX,1,hit.point,AudioManager.instance.Mixer.FindMatchingGroups("Effects")[0]);
                    
                    ParticleManager.PlayParticleAtPosition(ParticleManager.instance.HitFX, hit.point,
                        Quaternion.LookRotation(hit.normal * -1f));
                    hit.transform.GetComponent<Enemy>().TakeDamage(_damage);
                }

                if (!hit.transform.CompareTag("Enemy"))
                {
                    if (!hit.transform.CompareTag("DynamicObject"))
                    {
                        AudioManager.PlaySoundAtPosition(AudioManager.instance.SwordHitSFX,.1f,hit.point, AudioManager.instance.Mixer.FindMatchingGroups("Effects")[0]);
                        
                        var bullethole = Instantiate(_bulletHole, hit.point, Quaternion.LookRotation(hit.normal * -1f));
                        Destroy(bullethole.gameObject, 3);
                    }

                    ParticleManager.PlayParticleAtPosition(ParticleManager.instance.BulletHitFX, hit.point,
                        Quaternion.LookRotation(hit.normal * -1f));
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * 50, ForceMode.Impulse);
                }
            }
        }
    }
    
}
