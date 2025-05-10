using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private EnemyMovement normalEnemyPrefab;
    [SerializeField] private EnemyMovement tankEnemyPrefab;
    void Start()
    {
        PoolManager.Instance.InitializePool(bulletPrefab, 15);
        PoolManager.Instance.InitializePool(normalEnemyPrefab, 20);
        PoolManager.Instance.InitializePool(tankEnemyPrefab, 20);
    }


}
