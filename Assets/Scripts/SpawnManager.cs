using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _obstaclePrefab;
    [SerializeField]
    private List<GameObject> _powerUpPrefabs;
    [SerializeField]
    private float _posYToSpawn = 13.0f;
    [SerializeField]
    private GameObject _itemsContainer;
    [SerializeField]
    private GameObject _EnemyContainer;
    private bool _stopSpawning = false;
    void Start()
    {
        StartCoroutine("SpawnDelayEnemy");
        StartCoroutine("SpawnDelayPowerUp");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnDelayEnemy()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new(Random.Range(-12.0f, 12.0f), _posYToSpawn, 0);
            GameObject newEnemy = Instantiate(_obstaclePrefab[Random.Range(0, _obstaclePrefab.Count)], posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(2);
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