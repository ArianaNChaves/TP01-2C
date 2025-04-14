using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Bullet,
    Enemy,
    
}
public class PoolController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialCountOfBullet;
    [SerializeField] private int initialCountOfEnemy;
    
    
    private Dictionary<ObjectType, List<GameObject>> _poolDeactivated;
    private Dictionary<ObjectType, List<GameObject>> _poolActivated;

    private void Start()
    {
        _poolDeactivated = new Dictionary<ObjectType, List<GameObject>>();
        _poolActivated = new Dictionary<ObjectType, List<GameObject>>();

        _poolDeactivated[ObjectType.Bullet] = new List<GameObject>();
        _poolDeactivated[ObjectType.Enemy] = new List<GameObject>();
        
        for (int i = 0; i < initialCountOfBullet; i++)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            _poolDeactivated[ObjectType.Bullet].Add(bullet); 
        }
        for (int i = 0; i < initialCountOfEnemy; i++)
        {
            var score = Instantiate(enemyPrefab);
            score.SetActive(false);
            _poolDeactivated[ObjectType.Enemy].Add(score);
        }
        
    }

    public GameObject GetObjectFromPool(ObjectType type)
    {
        GameObject objectFromPool = null;
        
        if (_poolDeactivated[type].Count > 0)
        {
            objectFromPool = _poolDeactivated[type][0];
            _poolDeactivated[type].RemoveAt(0);
            _poolActivated[type].Add(objectFromPool);
        }
        else
        {
            switch (type)
            {
                case ObjectType.Bullet:
                    objectFromPool = Instantiate(bulletPrefab);
                    break;
                case ObjectType.Enemy:
                    objectFromPool = Instantiate(enemyPrefab);
                    break;
                default:
                    Debug.LogError("GetObjectFromPool - ELSE - type error");
                    break;
            }
        }

        switch (type)
        {
            case ObjectType.Bullet:
                Debug.Log("Type.Bullet");
                //return objectFromPool;
                break;
            case ObjectType.Enemy:
                Debug.Log("Type.Enemy");
                break;
            default:
                Debug.LogError("GetObjectFromPool - ObjectType error");
                break;
        }
        
        Debug.LogError("GetObjectFromPool - Si llego aca seguramente explota");
        return objectFromPool;
    }
    
    public void ReturnObjectToPool(GameObject objectToReturn, ObjectType type)
    {
        objectToReturn.SetActive(false);
        _poolActivated[type].Remove(objectToReturn);
        _poolDeactivated[type].Add(objectToReturn);
    }
    
}
