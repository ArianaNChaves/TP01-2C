using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;

    private Vector3 _targetDestination;
    private bool _hasTarget = false;

    void Update()
    {
        if (_hasTarget)
        {
            Vector3 direction = (_targetDestination - transform.position).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, _targetDestination) < 0.1f)
            {
                _hasTarget = false;
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        _targetDestination = destination;
        _hasTarget = true;
    }
}