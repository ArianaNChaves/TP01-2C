using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    // [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private PoolController poolController;

    [Header("Spawning")]
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private Transform spawnPoint;

    [Header("Destinations")]
    [SerializeField] private List<Transform> destinationPoints;
    
    [Header("Paths")]
    [SerializeField] private List<Path> paths;
    
    
    private float _spawnTimer;

    private void Start()
    {
        // if (enemyPrefabs == null || enemyPrefabs.Count == 0)
        // {
        //     enabled = false;
        //     return;
        // }

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
        // int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count);
        int randomNumer = Random.Range(0, 2);
        GameObject selectedEnemyPrefab;
        if (randomNumer == 0)
        {
            selectedEnemyPrefab = poolController.GetObjectFromPool(ObjectType.NormalEnemy);
        }
        else
        {
            selectedEnemyPrefab = poolController.GetObjectFromPool(ObjectType.TankEnemy);
        }
        
            
        int randomDestinationIndex = Random.Range(0, destinationPoints.Count);
        Transform selectedDestination = destinationPoints[randomDestinationIndex];

        // GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        selectedEnemyPrefab.transform.position = spawnPoint.position;
        selectedEnemyPrefab.transform.rotation = spawnPoint.rotation;
        selectedEnemyPrefab.SetActive(true);
        var enemyMovement = selectedEnemyPrefab.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetDestination(selectedDestination.position);
        }
    }
    
    
}
[System.Serializable]
public class Path //todo Ver donde iria este choclo de codigo, enemy manager? en el mismo enemy?
{
    public List<Transform> waypoints;
    public GameObject target;
}
