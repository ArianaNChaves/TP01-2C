using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ObjectType
{
    Bullet,
    TankEnemy,
    NormalEnemy
    
}
public class PoolController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject tankEnemyPrefab;
    [SerializeField] private int initialCountOfBullet;
    [SerializeField] private int initialCountOfNormalEnemy;
    [SerializeField] private int initialCountOfTankEnemy;
    
    
    private Dictionary<ObjectType, List<GameObject>> _poolDeactivated;

    private void Start()
    {
        _poolDeactivated = new Dictionary<ObjectType, List<GameObject>>();

        _poolDeactivated[ObjectType.Bullet] = new List<GameObject>();
        _poolDeactivated[ObjectType.TankEnemy] = new List<GameObject>();
        _poolDeactivated[ObjectType.NormalEnemy] = new List<GameObject>();
        
        for (int i = 0; i < initialCountOfBullet; i++)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            _poolDeactivated[ObjectType.Bullet].Add(bullet); 
        }
        for (int i = 0; i < initialCountOfTankEnemy; i++)
        {
            var enemy = Instantiate(tankEnemyPrefab);
            enemy.SetActive(false);
            _poolDeactivated[ObjectType.TankEnemy].Add(enemy);
        }
        for (int i = 0; i < initialCountOfNormalEnemy; i++)
        {
            var enemy = Instantiate(normalEnemyPrefab);
            enemy.SetActive(false);
            _poolDeactivated[ObjectType.NormalEnemy].Add(enemy);
        }
        
        
    }

    public GameObject GetObjectFromPool(ObjectType type)
    {
        GameObject objectFromPool = null;
        
        if (_poolDeactivated[type].Count > 0)
        {
            objectFromPool = _poolDeactivated[type][0];
            _poolDeactivated[type].RemoveAt(0);
        }
        else
        {
            switch (type)
            {
                case ObjectType.Bullet:
                    objectFromPool = Instantiate(bulletPrefab);
                    objectFromPool.SetActive(false);
                    // _poolDeactivated[ObjectType.Bullet].Add(objectFromPool);
                    break;
                case ObjectType.TankEnemy:
                    objectFromPool = Instantiate(tankEnemyPrefab);
                    objectFromPool.SetActive(false);
                    // _poolDeactivated[ObjectType.TankEnemy].Add(objectFromPool);
                    break;
                case ObjectType.NormalEnemy:
                    objectFromPool = Instantiate(normalEnemyPrefab);
                    objectFromPool.SetActive(false);
                    // _poolDeactivated[ObjectType.NormalEnemy].Add(objectFromPool);
                    break;
                default:
                    Debug.LogError("GetObjectFromPool - ELSE - type error");
                    break;
            }
        }

        switch (type)
        {
            case ObjectType.Bullet:
                // Debug.Log("Type.Bullet");
                return objectFromPool;
            case ObjectType.TankEnemy:
                // Debug.Log("Type.TankEnemy");
                return objectFromPool;
            case ObjectType.NormalEnemy:
                // Debug.Log("Type.NormalEnemy");
                return objectFromPool;
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
        _poolDeactivated[type].Add(objectToReturn);
    }
    
}
