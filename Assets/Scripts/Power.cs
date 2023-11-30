using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    [SerializeField]
    protected GameObject _objPrefab;
    [SerializeField]
    protected string _tag;
    [SerializeField]
    protected float _speed = 5f;
    [SerializeField]
    protected ObjectPool _objectPool;

    protected virtual void Start()
    {
        _objectPool = ObjectPool.Instance;
    }

    protected virtual void Update()
    {
        MoveDown();
        outOfBound();
    }

    protected virtual void MoveDown()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);
    }

    protected virtual void outOfBound()
    {
        if (transform.position.y <= -5.0f)
        {
            this.DeSpawn();
        }
    }

    protected virtual void DeSpawn()
    {
        _objectPool.DeSpawnObject(_tag, this.gameObject);
    }


}
