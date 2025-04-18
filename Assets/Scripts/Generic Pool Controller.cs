using System.Collections.Generic;
using UnityEngine;

public class GenericPoolController : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
        public string poolName;

        [HideInInspector]
        public List<GameObject> pooledObjects;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            if (string.IsNullOrEmpty(pool.poolName))
            {
                Debug.LogError($"PoolController: Pool with prefab {pool.prefab.name} has no Pool Name defined!");
                continue;
            }

            if (_poolDictionary.ContainsKey(pool.poolName))
            {
                Debug.LogError($"PoolController: Multiple pools with the same Pool Name: {pool.poolName}! Pool Names must be unique.");
                continue;
            }

            pool.pooledObjects = new List<GameObject>(); 
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                pool.pooledObjects.Add(obj);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.poolName, objectPool);
        }
    }

    public GameObject GetObjectFromPool(string poolName)
    {
        if (!_poolDictionary.ContainsKey(poolName))
        {
            Debug.LogError($"Pool with name '{poolName}' not found.");
            return null;
        }

        Queue<GameObject> objectPool = _poolDictionary[poolName];

        if (objectPool.Count > 0)
        {
            GameObject objectToSpawn = objectPool.Dequeue();
            objectToSpawn.SetActive(true);
            return objectToSpawn;
        }
        else
        {
            foreach (var pool in pools)
            {
                if (pool.poolName == poolName)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    return obj;
                }
            }
            Debug.LogError($"Pool with name '{poolName}' is empty and can't find the pool to Instantiate a new object.");
            return null;  
        }
    }


    public void ReturnObjectToPool(string poolName, GameObject objectToReturn)
    {
        if (!_poolDictionary.ContainsKey(poolName))
        {
            Debug.LogError($"Pool with name '{poolName}' not found.");
            Destroy(objectToReturn);
            return;
        }
        objectToReturn.SetActive(false);
        _poolDictionary[poolName].Enqueue(objectToReturn);
    }
}