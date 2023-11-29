using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPool : ISingletonMonoBehaviour<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            GameObject container = new GameObject(pool.tag + "Pool");
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = container.transform;
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectpool);
        }
    }

    public GameObject SpawnObject(string tag, Vector2 position, Quaternion rotation)
    {
        if(poolDictionary.ContainsKey(tag) == false)
        {
            Debug.LogWarning("Pool with tag" + tag + " doesn't excist.");
            return null;
        }
        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        return objToSpawn;
    }

    public void DeSpawnObject(string tag, GameObject objToDeSpawn)
    {
        poolDictionary[tag].Enqueue(objToDeSpawn);
        objToDeSpawn.SetActive(false);
    }
}
