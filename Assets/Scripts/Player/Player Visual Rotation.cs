using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualRotation : MonoBehaviour
{
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private float pitchAngle;
    [SerializeField] private float rollAngle;
    private void Update()
    {
        VisualRotation();
    }

    private void VisualRotation()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical   = Input.GetAxis("Vertical");
        
        float pitch = Mathf.Lerp(0, pitchAngle, Mathf.Abs(moveVertical)) * Mathf.Sign(moveVertical);
        float roll  = Mathf.Lerp(0, rollAngle, Mathf.Abs(moveHorizontal)) * -Mathf.Sign(moveHorizontal);
        
        playerVisual.transform.localRotation = Quaternion.Euler(pitch, 0, roll); 
    }
}
