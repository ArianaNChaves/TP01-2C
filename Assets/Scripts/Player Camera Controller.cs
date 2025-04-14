using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonCamera;

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
            }
            else
            {
                firstPersonCamera.SetActive(false);
                thirdPersonCamera.SetActive(true);
            }
        }
    }
}
