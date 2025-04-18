using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Needed for Any() check

public class EnemySpawner : MonoBehaviour
{
    [Header("Pool Names")]
    [SerializeField] private string normalEnemyPoolName = "enemy-pool";
    [SerializeField] private string tankEnemyPoolName = "tank-pool";

    [Header("Spawning")]
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private Transform spawnPoint;

    [Header("Paths")]
    [SerializeField] private List<Path> paths;

    [SerializeField] private GenericPoolController poolController;


    private float _spawnTimer;
    private List<Path> _validPaths = new List<Path>();

    private void Start()
    {
        if (paths == null || paths.Count == 0)
        {
            Debug.LogError("No hay paths asignados.", this);
            enabled = false;
            return;
        }

        foreach (var path in paths)
        {
            bool pathIsValid = true;
            if (path.waypoints == null || path.waypoints.Count == 0)
            {
                Debug.LogError($"Path no tiene waypoints.", this);
                pathIsValid = false;
            }
            else
            {
                if (path.waypoints.Any(wp => wp == null))
                {
                     Debug.LogError($"Path contiene waypoints nulos.", this);
                     pathIsValid = false;
                }
            }

            if (path.target == null)
            {
                Debug.LogError($"Path no tiene target", this);
                pathIsValid = false;
            }

            if (pathIsValid)
            {
                _validPaths.Add(path);
            }
        }

        if (_validPaths.Count == 0)
        {
            Debug.LogError("No hay paths validos.", this);
            enabled = false;
            return;
        }

        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }

        if (poolController == null)
        {
            Debug.LogError("PoolController no asignada.", this);
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
        if (_validPaths.Count == 0) return;

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
            Debug.LogError($"Error obteniendo enemigo de la pool '{selectedPoolName}'.", this);
            return;
        }

        int randomPathIndex = Random.Range(0, _validPaths.Count);
        Path selectedPath = _validPaths[randomPathIndex];

        newEnemy.transform.position = spawnPoint.position;
        newEnemy.transform.rotation = spawnPoint.rotation;
        newEnemy.SetActive(true);

        var enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetPath(selectedPath);
        }
        else
        {
             Debug.LogWarning($"Enemigo de la pool '{selectedPoolName}' no tiene script EnemyMovement.", newEnemy);
             poolController.ReturnObjectToPool(selectedPoolName, newEnemy);
        }
    }
}

[System.Serializable]
public class Path
{
    public List<Transform> waypoints;
    public GameObject target;
}