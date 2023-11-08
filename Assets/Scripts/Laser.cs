using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 13.0f;
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
    void outOfBound()
    {
        if (transform.position.y > 13.0f)
        {
            Destroy(this.gameObject);
        }
    }


}
