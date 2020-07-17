using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLogic
{
    public static Vector3 GetRandomDirection(float randomness)
    {
        Vector3 direction = new Vector3(Random.Range(-randomness,randomness),Random.Range(-randomness,randomness),Random.Range(-randomness,randomness)
        );

        return direction;

    }


    public static void PlayExplosion(Vector3 pos, float explosionRadius)
    {
        ParticleManager.PlayParticleAtPosition(ParticleManager.instance.ExplosionFX,pos,Quaternion.identity);
        AudioManager.PlaySoundAtPosition(AudioManager.instance.ExplosionSFX,1f,pos,AudioManager.instance.Mixer.FindMatchingGroups("Effects")[0]);
        RaycastHit hit;
        if (Physics.Raycast(pos,GameManager.instance.player.transform.position-pos,out hit,explosionRadius))
        {
            if (hit.transform.CompareTag("Player"))
            {
                GameManager.instance.playerClass.PlayerDamage(10);
            }
        }
    }
    
}
