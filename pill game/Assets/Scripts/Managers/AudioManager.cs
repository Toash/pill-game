using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;

    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
            }

            return _instance;
        }
    }

    public AudioMixer Mixer;
    public AudioClip GunTickSFX;
    public AudioClip GunShotSFX;
    public AudioClip SilencedGunShotSFX;
    public AudioClip SwordSlashSFX;
    public AudioClip SwordHitSFX;
    public AudioClip PickupSFX;
    public AudioClip EnemyDeathSFX;
    public AudioClip SplatSFX;

    public AudioClip ReloadSFX;
    public AudioClip AlertedSFX;

    public AudioClip ThudSFX;

    public AudioClip ExplosionSFX;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
    //AudioManager.PlaySound(AudioManager.instance.ReloadSFX, .25f);
    //AudioManager.PlaySoundAtPosition(AudioManager.instance.SplatSFX,10,this.transform.position,AudioManager.instance.Mixer.FindMatchingGroups("Enemy")[0]);
    //plays 2d sound
    public static void PlaySound(AudioClip sound, float _volume)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.volume = _volume;
        audioSource.PlayOneShot(sound);
        Destroy(audioSource.gameObject,sound.length);
    }
    public static void PlaySoundAtPosition(AudioClip sound, float _volume,Vector3 position, AudioMixerGroup group)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.transform.position = position;
        audioSource.volume = _volume;
        audioSource.spatialBlend = 1;
        audioSource.outputAudioMixerGroup = group;
        audioSource.PlayOneShot(sound);
        Destroy(audioSource.gameObject,sound.length);
    }

}
