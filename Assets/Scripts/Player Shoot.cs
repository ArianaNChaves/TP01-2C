using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform laserSpawn;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float shootRange;
    [SerializeField] private float fireRate;
    [SerializeField] private float laserDuration;
    
    private LineRenderer _lineRenderer;
    private float _fireTimer;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        ShootHandler();
    }

    //todo el rayo que hace no corresponde con el punto de la mira
    private void ShootHandler()
    {
        _fireTimer += Time.deltaTime;

        if (!Input.GetMouseButtonDown(0) || !(_fireTimer >= fireRate)) return;
        
        _fireTimer = 0;
        _lineRenderer.SetPosition(0, laserSpawn.position);
        Ray rayCast = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        
        _lineRenderer.SetPosition(1, laserSpawn.position + (rayCast.direction * shootRange));

        StartCoroutine(ShootLaser());
    }

    private IEnumerator ShootLaser()
    {
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        _lineRenderer.enabled = false;
    }
}
