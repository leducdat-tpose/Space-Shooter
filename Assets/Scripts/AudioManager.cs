using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : ISingletonMonoBehaviour<AudioManager>
{
    [Header("AudioClip")]
    [SerializeField]
    private AudioClip _explosion;
    [SerializeField]
    private AudioClip _laserShot;
    
    public AudioClip playExplosion()
    {
        return _explosion;
    }
    public AudioClip playLaserShot()
    {
        return _laserShot;
    }
}
