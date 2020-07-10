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
    public AudioClip GunShotSFX;
    public AudioClip EnemyDeathSFX;
    public AudioClip SplatSFX;

    
    public static void PlaySound(AudioClip sound, float _volume)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.volume = _volume;
        audioSource.PlayOneShot(sound);
        Destroy(audioSource.gameObject,sound.length);
    }
    public static void PlaySoundAtPosition(AudioClip sound, float _volume,Transform position, AudioMixerGroup group)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.transform.position = position.transform.position;
        audioSource.volume = _volume;
        audioSource.spatialBlend = 1;
        audioSource.outputAudioMixerGroup = group;
        audioSource.PlayOneShot(sound);
        Destroy(audioSource.gameObject,sound.length);
    }
    
}
