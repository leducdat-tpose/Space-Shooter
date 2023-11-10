using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObstacle
{
    [SerializeField]
    private float _speed = 9.0f;
    [SerializeField]
    private Player _player;

    [SerializeField]
    private Animator _enemyAnimation;
    [SerializeField]
    private bool _isMove = true;
    [SerializeField]
    private bool _isDeath = false;
    private int _pointEarn = 10;
    private int _damageToPlayer = 1;
    private AudioSource _enemyAudio;
    private AudioClip _explosionAudio;
    void Start()
    {
        _enemyAudio = GetComponent<AudioSource>();
        _explosionAudio = GameObject.Find("AudioManager").transform.Find("Explosion").GetComponent<AudioSource>().clip;
        if(_player == null) Debug.LogError("Cant get component player form enemy!");
        if (_enemyAudio == null) Debug.LogError("Cant get component audio form enemy!");
        _isMove = true;
        _isDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDeath == false)
        {
            if (other.tag == "Laser")
            {
                _player.increaseScore(_pointEarn);
                _enemyAnimation.SetTrigger("Death");
                triggerObjectAudio(_explosionAudio);
                Destroy(other.gameObject);
            }
            else if (other.tag == "Player")
            {
                _player.Damaged(_damageToPlayer);
                _enemyAnimation.SetTrigger("Death");
                triggerObjectAudio(_explosionAudio);
            }
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void Move()
    {
        if(_isMove) transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }
    public void outOfBound()
    {
        if (transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }
    }
    public void triggerObjectAudio(AudioClip audio)
    {
        _enemyAudio.clip = audio;
        _enemyAudio.Play();
    }
}
