using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Pool Names")]
    [SerializeField] private string normalEnemyPoolName = "enemy-pool";
    [SerializeField] private string tankEnemyPoolName = "tank-pool";

    [Header("Spawning")]
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private Transform spawnPoint;

    [Header("Destinations")]
    [SerializeField] private List<Transform> destinationPoints;

    [Header("Paths")]
    [SerializeField] private List<Path> paths;

    [SerializeField] private GenericPoolController poolController;


    private float _spawnTimer;

    private void Start()
    {
        if (destinationPoints == null || destinationPoints.Count == 0)
        {
            Debug.LogError("No destination points assigned. Disabling EnemySpawner.");
            enabled = false;
            return;
        }

        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }

        if (poolController == null)
        {
            Debug.LogError("PoolController not assigned. Disabling EnemySpawner.");
            enabled = false;
            return;
        }

        _spawnTimer = spawnRate;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f)
        {
            SpawnEnemy();
            _spawnTimer = spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        string selectedPoolName;
        if (Random.Range(0, 2) == 0)
        {
            selectedPoolName = normalEnemyPoolName;
        }
        else
        {
            selectedPoolName = tankEnemyPoolName;
        }

        GameObject newEnemy = poolController.GetObjectFromPool(selectedPoolName);

        if (newEnemy == null)
        {
            Debug.LogError($"Failed to get enemy from pool '{selectedPoolName}'.  Check pool configuration.");
            return;
        }

        int randomDestinationIndex = Random.Range(0, destinationPoints.Count);
        Transform selectedDestination = destinationPoints[randomDestinationIndex];

        newEnemy.transform.position = spawnPoint.position;
        newEnemy.transform.rotation = spawnPoint.rotation;
        newEnemy.SetActive(true);

        var enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetDestination(selectedDestination.position);
        }
        else
        {
            Debug.LogWarning("Enemy prefab does not have an EnemyMovement script attached.");
        }
    }
}

[System.Serializable]
public class Path
{
    public List<Transform> waypoints;
    public GameObject target;
}