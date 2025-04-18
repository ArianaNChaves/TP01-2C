using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Needed for error checking

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,    
        Moving,  
        Attacking 
    }

    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float rotationSpeed = 5.0f; 

    public EnemyState currentState = EnemyState.Idle;

    private List<Transform> _waypoints;
    private GameObject _finalTarget;
    private int _currentWaypointIndex = -1; 
    private Transform _currentTargetWaypoint; 

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Moving:
                UpdateMoving();
                break;
            case EnemyState.Attacking:
                UpdateAttacking();
                break;
        }
    }

    // --- State Logic ---

    private void UpdateIdle()
    {
        // Enemy remains idle until a path is set.
    }

    private void UpdateMoving()
    {
        if (_currentTargetWaypoint == null)
        {
             Debug.LogError("Current target waypoint is null while in Moving state. Switching to Idle.", this);
             currentState = EnemyState.Idle;
             return;
        }

        Vector3 targetPosition = _currentTargetWaypoint.position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (direction != Vector3.zero) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position += moveSpeed * Time.deltaTime * direction;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= _waypoints.Count)
            {
                currentState = EnemyState.Attacking;
                 _currentTargetWaypoint = null; 
            }
            else
            {
                _currentTargetWaypoint = _waypoints[_currentWaypointIndex];
                if (_currentTargetWaypoint == null)
                {
                    throw new System.NullReferenceException($"Waypoint at index {_currentWaypointIndex} in the assigned path is null for enemy {gameObject.name}.");
                }
            }
        }
    }

    private void UpdateAttacking()
    {
        if (_finalTarget != null)
        {
            Vector3 directionToTarget = (_finalTarget.transform.position - transform.position).normalized;
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // --- ADD YOUR ATTACK LOGIC HERE ---
        }
        else
        {
             Debug.LogWarning("Final target is null or destroyed. Switching back to Idle.", this);
             currentState = EnemyState.Idle;
        }
    }
    
    public void SetPath(Path path)
    {
        // Basic validation
        if (path == null || path.waypoints == null || path.waypoints.Count == 0 || path.target == null)
        {
            Debug.LogError("Attempted to assign an invalid path to EnemyMovement. Disabling enemy.", this);
            gameObject.SetActive(false); // RETURN TO POOL
            return;
        }
        if(path.waypoints[0] == null)
        {
            throw new System.NullReferenceException($"The first waypoint (index 0) in the assigned path is null for enemy {gameObject.name}.");
        }


        _waypoints = path.waypoints;
        _finalTarget = path.target;
        _currentWaypointIndex = 0;
        _currentTargetWaypoint = _waypoints[_currentWaypointIndex];

        currentState = EnemyState.Moving; 
        // play run animation
    }

    public void ResetEnemy()
    {
        currentState = EnemyState.Idle;
        _waypoints = null;
        _finalTarget = null;
        _currentWaypointIndex = -1;
        _currentTargetWaypoint = null;
    }
}