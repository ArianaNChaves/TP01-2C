using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private List<GameObject> enemyPrefabs;

    [Header("Spawning")]
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private Transform spawnPoint;

    [Header("Destinations")]
    [SerializeField] private List<Transform> destinationPoints;

    private float _spawnTimer;

    void Start()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0)
        {
            enabled = false;
            return;
        }

        if (destinationPoints == null || destinationPoints.Count == 0)
        {
            enabled = false;
            return;
        }

        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }

        _spawnTimer = spawnRate;
    }

    void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f)
        {
            SpawnEnemy();
            _spawnTimer = spawnRate;
        }
    }

    void SpawnEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject selectedEnemyPrefab = enemyPrefabs[randomEnemyIndex];

        int randomDestinationIndex = Random.Range(0, destinationPoints.Count);
        Transform selectedDestination = destinationPoints[randomDestinationIndex];

        GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation);

        var enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetDestination(selectedDestination.position);
        }
    }
}
