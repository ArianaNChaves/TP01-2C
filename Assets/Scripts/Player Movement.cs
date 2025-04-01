using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalAcceleration;
    [SerializeField] private float verticalForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float horizontalDrag;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        HorizontalMovement();
        VerticalMovement();
    }

    private void HorizontalMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        _moveDirection = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        
        Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        
        horizontalVelocity += _moveDirection * (horizontalAcceleration * Time.fixedDeltaTime);
        
        if (_moveDirection == Vector3.zero)
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, horizontalDrag * Time.fixedDeltaTime);
        }
        
        if (horizontalVelocity.magnitude >= maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized  * maxSpeed;
        }
        
        _rigidbody.velocity = new Vector3(horizontalVelocity.x, _rigidbody.velocity.y, horizontalVelocity.z);
    }

    private void VerticalMovement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * verticalForce, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _rigidbody.AddForce(Vector3.up * -verticalForce, ForceMode.Impulse);
        }
        
    }
}
