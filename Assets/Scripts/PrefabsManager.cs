using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    [SerializeField] private RaycastBullet raycastBulletPrefab;
    [SerializeField] private PhysicBullet physicBulletPrefab;
    [SerializeField] private EnemyMovement normalEnemyPrefab;
    [SerializeField] private EnemyMovement tankEnemyPrefab;
    void Start()
    {
        PoolManager.Instance.InitializePool(raycastBulletPrefab, 15);
        PoolManager.Instance.InitializePool(physicBulletPrefab, 15);
        PoolManager.Instance.InitializePool(normalEnemyPrefab, 20);
        PoolManager.Instance.InitializePool(tankEnemyPrefab, 20);
    }


}
