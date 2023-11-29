using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeLaser
{
    SingleLaser,
    TripleLaser,
}

public enum TypeObject
{
    Player,
    Enemy,
}


public class SpawnManager : ISingletonMonoBehaviour<SpawnManager>
{
    [SerializeField]
    private List<GameObject> _obstaclePrefab;
    [SerializeField]
    private List<GameObject> _powerUpPrefabs;
    [SerializeField]
    private List<GameObject> _laserPrefabs;
    [SerializeField]
    private float _posYToSpawn = 13.0f;
    [SerializeField]
    private ObjectPool _objectPool;
    [SerializeField]
    private GameObject _itemsContainer;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject _fromPlayer;
    private bool _stopSpawning = false;

    void Start()
    {
        _objectPool = ObjectPool.Instance;
        StartCoroutine("SpawnDelayEnemy");
        StartCoroutine("SpawnDelayPowerUp");
        StartCoroutine("SpawnDelayAsteroid");
    }
    // Update is called once per frame
    void Update()
    {
    }


    public void SpawnLaser(Vector2 position, Quaternion rotation)
    {
        _objectPool.SpawnObject("Laser", position, rotation);
    }




    IEnumerator SpawnDelayEnemy()
    {
        while (_stopSpawning == false)
        {

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            Vector2 posToSpawn = new(Random.Range(-12.0f, 12.0f), _posYToSpawn);
            _objectPool.SpawnObject("Enemy", posToSpawn, Quaternion.identity);
        }
    }
    IEnumerator SpawnDelayAsteroid()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(10f, 17f));
            Vector2 posToSpawn = new(Random.Range(-12.0f, 12.0f), _posYToSpawn);
            _objectPool.SpawnObject("Asteroid", posToSpawn, Quaternion.identity);
        }
    }

    IEnumerator SpawnDelayPowerUp()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new(Random.Range(-12.0f, 12.0f), _posYToSpawn, 0);
            GameObject newPowerUp = Instantiate(_powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Count)], posToSpawn, Quaternion.identity);
            newPowerUp.transform.parent = _itemsContainer.transform;
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    public void playerDead()
    {
        _stopSpawning = true;
    }
}
