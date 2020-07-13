using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager _instance = null;

    public static ParticleManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(ParticleManager)) as ParticleManager;
            }

            return _instance;
        }
    }

    public ParticleSystem EnemyDeathFX;
    public ParticleSystem BloodMistFX;
    public ParticleSystem HitFX;
    public ParticleSystem BulletHitFX;
    public ParticleSystem BulletTracer;

    public static void PlayParticleAtPosition(ParticleSystem particle, Vector3 position, Quaternion rotation)
    {
        var particleFX = Instantiate(particle, position, rotation);
        Destroy(particleFX.gameObject, particle.main.startLifetimeMultiplier);

    }
}
