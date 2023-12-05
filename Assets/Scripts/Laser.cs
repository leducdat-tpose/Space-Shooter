using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float _speed = 13.0f;
    [SerializeField]
    private int _damaged = 1;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        outOfBound();
    }


    void Move()
    {
        transform.Translate(_speed * Vector3.up * Time.deltaTime);
    }


    public int GetDamage() { return _damaged; }

    void DeSpawn()
    {
        ObjectPool.Instance.DeSpawnObject("Laser", this.gameObject);
    }

    void outOfBound()
    {
        if (transform.position.y > 13.0f || transform.position.y <= -5.0f)
        {
            DeSpawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "Player")
        {
            DeSpawn();
        }
    }

}
