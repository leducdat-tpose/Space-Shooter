using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour, IObstacle
{
    [SerializeField]
    private float _speedRotation = 5f;
    [SerializeField]
    private float _speed = 7f;
    [SerializeField]
    private int _damageToPlayer = 2;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Animator _asteroidAnimator;
    [SerializeField]
    private bool _isMove = true;
    [SerializeField]
    private bool _isDestroy = true;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _asteroidAnimator = GetComponent<Animator>();
        _isMove = true;
        _isDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        outOfBound();
        Move();
    }
    public void Move()
    {
        if (_isMove == true)
        {
            transform.Rotate(Time.deltaTime * _speedRotation * new Vector3(0,0,1) * 40);
            transform.Translate(Time.deltaTime * _speed * Vector3.down, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDestroy == false)
        {
            if (other.tag == "Player")
            {
                _player.Damaged(_damageToPlayer);
                _isMove = false;
                _isDestroy = true;
                _asteroidAnimator.SetTrigger("Destroy");
                AudioManager.Instance.playExplosion();
            }
        }

    }

    public void DeSpawn()
    {
        ObjectPool.Instance.DeSpawnObject("Asteroid", this.gameObject);
    }
    public void outOfBound()
    {
        if (transform.position.y <= -5.0f)
        {
            this.DeSpawn();
        }
    }

}
