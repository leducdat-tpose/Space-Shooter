using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : ISingletonMonoBehaviour<AudioManager>
{
    [Header("Main Audio Source")]
    [SerializeField]
    private AudioSource _audioSource;
    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip _explosion;
    [SerializeField]
    private AudioClip _laserShot;
    [SerializeField]
    private AudioClip _buff;
    public void playExplosion()
    {
        _audioSource.PlayOneShot(_explosion);
    }
    public void playLaserShot()
    {
        _audioSource.PlayOneShot(_laserShot);
    }
    public void playBuff()
    {
        _audioSource.PlayOneShot(_buff);
    }

}
