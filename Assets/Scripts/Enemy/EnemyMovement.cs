using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Moving,
        Attacking
    }

    [SerializeField] private float moveSpeed = 3.0f;

    public EnemyState currentState = EnemyState.Idle; // Initial state
    [SerializeField] private Animator animator;

    private Vector3 _targetDestination;
    private bool _hasTarget = false;

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
        // Idle

        if (_hasTarget)
        {
            currentState = EnemyState.Moving; // Transition to moving if we have a target.
        }
    }

    private void UpdateMoving()
    {
        if (_hasTarget)
        {
            Vector3 direction = (_targetDestination - transform.position).normalized;

            transform.position +=  moveSpeed * Time.deltaTime * direction;

            if (Vector3.Distance(transform.position, _targetDestination) < 0.1f)
            {
                _hasTarget = false;
                currentState = EnemyState.Idle;
            }
        }
        else
        {
            currentState = EnemyState.Idle;
        }
    }

    private void UpdateAttacking()
    {
        // Attack
        //animator.Play(AnimationConstants.EnemyAnimations["Attack"]);
        currentState = EnemyState.Idle; //Temporary, so it doesn't get stucked
    }

    public void SetDestination(Vector3 destination)
    {
        _targetDestination = destination;
        _hasTarget = true;
        currentState = EnemyState.Moving;
    }
}