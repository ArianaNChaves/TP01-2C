using System;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        LookAround();
    }
    
    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        Quaternion targetRotation = Quaternion.Euler(transform.localEulerAngles.x + -mouseY, transform.localEulerAngles.y + mouseX, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
