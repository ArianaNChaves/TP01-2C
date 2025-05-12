using System;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonCamera;
    
    public enum CameraView
    {
        FirstPerson,
        ThirdPerson
    }
    private CameraView _currentCameraView;

    private void Start()
    {
        _currentCameraView = CameraView.ThirdPerson;
    }

    private void Update()
    {
        SwitchCamera();
    }
    
    private void SwitchCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (thirdPersonCamera.activeSelf)
            {
                thirdPersonCamera.SetActive(false);
                firstPersonCamera.SetActive(true);
                _currentCameraView = CameraView.FirstPerson;
            }
            else
            {
                firstPersonCamera.SetActive(false);
                thirdPersonCamera.SetActive(true);
                _currentCameraView = CameraView.ThirdPerson;
            }
        }
    }

    public CameraView GetCurrentCameraView()
    {
        return _currentCameraView;
    }
}
