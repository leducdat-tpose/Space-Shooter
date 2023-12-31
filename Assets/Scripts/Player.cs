using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Player : ISingletonMonoBehaviour<Player>
{
    [Header("Parameters")]
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _scorePoint;
    [SerializeField]
    private float _speed = 9.0f;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canShoot = 0;
    [SerializeField]
    private bool _triplePower = false;

    [Header("Movement Range")]
    [SerializeField]
    private float _limitedTop = 5.0f;
    [SerializeField]
    private float _limitedBottom = -3.0f;
    [SerializeField]
    private float _limitedRight = 12.0f;
    [SerializeField]
    private float _limitedLeft = -12.0f;

    [Header("GameObject/Components")]
    [SerializeField]
    private GameObject _thurster;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private bool _isHasShield;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private Animator _playerAnimator;
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _playerAnimator = gameObject.GetComponent<Animator>();
        _thurster = transform.Find("Thruster").gameObject;
        _shield = transform.Find("Shield").gameObject;
        _isHasShield = false;
        _shield.SetActive(false);
        _thurster.SetActive(true);
        transform.position = new(0, -3, 0);
        _scorePoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            Move();
            shoot();
        }
        limitRangeMove();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            this.Damaged(other.GetComponent<Laser>().GetDamage());
        }
        else return;
    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canShoot)
        {
            _canShoot = Time.time + _fireRate;
            Vector3 position = transform.position + new Vector3(0, 2, 0);
            if (_triplePower == true)
            {
                SpawnManager.Instance.SpawnLaser(position, Quaternion.identity, "Triple");
            }
            else
            {
                SpawnManager.Instance.SpawnLaser(position, Quaternion.identity);
            }
            AudioManager.Instance.playLaserShot();
        }
    }

    void Move()
    {
        if(_lives > 0)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            if (Input.GetKey(KeyCode.A))
            {

                _playerAnimator.SetBool("TurnLeft", true);
                _playerAnimator.SetBool("TurnRight", false);
            }
            else if (Input.GetKey(KeyCode.D))
            {

                _playerAnimator.SetBool("TurnLeft", false);
                _playerAnimator.SetBool("TurnRight", true);
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                _playerAnimator.SetBool("TurnLeft", false);
                _playerAnimator.SetBool("TurnRight", false);
            }
            else 
            {
                _playerAnimator.SetBool("TurnLeft", false);
                _playerAnimator.SetBool("TurnRight", false);
            }
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
            SpawnManager.Instance.playerDead();
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
        if(_lives < 0) _lives = 0;
        _uiManager.UpdateLiveSprite(_lives);
        if(_lives == 0)
        {
            _thurster.SetActive(false);
            _playerAnimator.SetTrigger("Death");
            AudioManager.Instance.playExplosion();
            if(PlayerPrefs.GetInt("BestScore", 0) < this.GetScorePoint())
            {
                PlayerPrefs.SetInt("BestScore", this.GetScorePoint());
                _uiManager.UpdateBestScore(PlayerPrefs.GetInt("BestScore"));
            }
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
}
