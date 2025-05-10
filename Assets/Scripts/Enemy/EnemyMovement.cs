using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Needed for error checking

public class EnemyMovement : MonoBehaviour, IPooleable
{
    public enum EnemyState
    {
        Idle,    
        Moving,  
        Attacking 
    }

    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private int damage;

    [SerializeField]
    private AnimationController animationController;

    public EnemyState currentState = EnemyState.Idle;

    private List<Transform> _waypoints;
    private GameObject _finalTarget;
    private int _currentWaypointIndex = -1; 
    private Transform _currentTargetWaypoint;
    private float _timeBetweenAttacks = 5;
    private float _attackRate = 3;

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


    private void UpdateIdle()
    {
        animationController.PlayAnimation(AnimationConstants.EnemyAnimations["Idle"]);
    }

    private void UpdateMoving()
    {
        if (_currentTargetWaypoint == null)
        {
             Debug.LogError("El siguiente waypoint es nulo.", this);
             currentState = EnemyState.Idle;
             return;
        }

        if (_finalTarget == null)
        {
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
                    throw new System.NullReferenceException($"Waypoint numero {_currentWaypointIndex} es nulo para el enemigo {gameObject.name}.");
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

            _timeBetweenAttacks += Time.deltaTime;
            
            if (_timeBetweenAttacks >= _attackRate)
            {
                Debug.Log("TROMPADA");
                animationController.PlayAnimation(AnimationConstants.EnemyAnimations["Attack"]);
                _finalTarget.GetComponent<IDamageable>().TakeDamage(damage);
                _timeBetweenAttacks = 0;
            }
        }
        else
        {
             Debug.LogWarning("El target final es nulo.", this);
             currentState = EnemyState.Idle;
        }
    }
    
    public void SetPath(Path path)
    {
        if (path == null || path.waypoints == null || path.waypoints.Count == 0 || path.target == null)
        {
            // Debug.LogError("Path invalido.", this);
            gameObject.SetActive(false); // RETURN TO POOL
            return;
        }
        if (path.waypoints[0] == null)
        {
            throw new System.NullReferenceException($"El primer waypoint es nulo {gameObject.name}.");
        }
        
        _waypoints = path.waypoints;
        _finalTarget = path.target;
        _currentWaypointIndex = 0;
        _currentTargetWaypoint = _waypoints[_currentWaypointIndex];

        currentState = EnemyState.Moving; 
        // correr animacion de correr
    }

    public void ReturnObjectToPool()
    {
        throw new System.NotImplementedException();
    }

    public void GetObjectFromPool()
    {
        throw new System.NotImplementedException();
    }
}