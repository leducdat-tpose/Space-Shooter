using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 9.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private float _fireRate = 1.0f;
    private float _canShoot = 0;
    [SerializeField]
    private float _limitedTop = 5.0f;
    [SerializeField]
    private float _limitedBottom = -3.0f;
    [SerializeField]
    private float _limitedRight = 12.0f;
    [SerializeField]
    private float _limitedLeft = -12.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _triplePower = false;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _thurster;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private bool _isHasShield;
    [SerializeField]
    private int _scorePoint;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private Animator _playerAnimator;
    [SerializeField]
    private AudioClip _laserAudio;
    [SerializeField]
    private AudioClip _explosionAudio;
    private AudioSource _playerAudio;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _playerAnimator = gameObject.GetComponent<Animator>();
        _playerAudio = gameObject.AddComponent<AudioSource>();
        _explosionAudio = GameObject.Find("AudioManager").transform.Find("Explosion").GetComponent<AudioSource>().clip;
        _laserAudio = GameObject.Find("AudioManager").transform.Find("LaserShot").GetComponent<AudioSource>().clip;
        _thurster = transform.Find("Thruster").gameObject;
        _isHasShield = false;
        _shield.SetActive(false);
        _scorePoint = 0;
        _thurster.SetActive(true);
        transform.position = new(0, -3, 0);
        if (_spawnManager == null) Debug.LogError("spawnManager is NULL\n");
        if (_playerAudio == null) Debug.LogError("playerAudio is NULL\n");

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        limitRangeMove();
        shoot();
    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canShoot)
        {
            _canShoot = Time.time + _fireRate;
            if (_triplePower == true)
            {
                Instantiate(_tripleShot, transform.position, Quaternion.identity);
            }
            else
            {
                Vector3 position = transform.position + new Vector3(0, 0.8f, 0);
                Instantiate(_laserPrefab, position, Quaternion.identity);
            }
            triggerObjectAudio(_laserAudio);
        }
    }

    void Move()
    {
        if(_lives > 0)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 control = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(control * _speed * Time.deltaTime);
        }
    }

    void limitRangeMove()
    {
        if (transform.position.x >= _limitedRight)
        {
            transform.position = new(_limitedRight, transform.position.y, 0);
        }
        else if (transform.position.x <= _limitedLeft)
        { 
            transform.position = new(_limitedLeft, transform.position.y, 0);
        }
        //Top, Bottom
        if (transform.position.y >= _limitedTop)
        {
            transform.position = new(transform.position.x, _limitedTop, 0);
        }
         else if (transform.position.y <= _limitedBottom)
        {
            transform.position = new(transform.position.x, _limitedBottom, 0);
        }
    }

    void Death()
    {
        if (_lives < 1) 
        {
            Destroy(this.gameObject);
            _spawnManager.playerDead();
        }
    }
    public void Damaged(int damage)
    {
        if (_isHasShield)
        {
            damage -= 1;
            ShieldPower(false);
        } 
        _lives -= damage;
        _uiManager.UpdateLiveSprite(_lives);
        if(_lives == 0)
        {
            _thurster.SetActive(false);
            _playerAnimator.SetTrigger("Death");
            triggerObjectAudio(_explosionAudio);
            _uiManager.DisplayGameOver();
        }
    }

    public int GetLives()
    {
        return _lives;
    }

    public int GetScorePoint()
    {
        return _scorePoint;
    }

    public void receiveTripleBuff()
    {
        _triplePower = true;
        StartCoroutine("tripleShotCoolDown");
    }

    public void ShieldPower(bool option)
    {
        _isHasShield = option;
        _shield.SetActive(_isHasShield);
        if(_isHasShield)
        {
            StartCoroutine("shieldCoolDown");
        }
    }

    IEnumerator tripleShotCoolDown()
    { 
        yield return new WaitForSeconds(5.0f);
        _triplePower = false;
    }
    IEnumerator shieldCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isHasShield = false;
        _shield.SetActive(_isHasShield);
    }
    public void increaseScore(int scorePlus)
    {
        _scorePoint += scorePlus;
        _uiManager.UpdateScoreText(_scorePoint);
    }
    public void triggerObjectAudio(AudioClip audio)
    {
        _playerAudio.clip = audio;
        _playerAudio.Play();
    }
}
