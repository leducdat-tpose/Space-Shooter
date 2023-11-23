using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 13.0f;
    private bool _isEnemy = false;
    void Start()
    {
        if(transform.parent.name == "EnemyContainer") _isEnemy = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        outOfBound();
    }


    void Move()
    {
        if(_isEnemy != true) transform.Translate(_speed * Vector3.up * Time.deltaTime);
        else if(_isEnemy) transform.Translate(_speed * Vector3.down * Time.deltaTime);
    }
    void outOfBound()
    {
        if (transform.position.y > 13.0f || transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }
    }


}
