using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform laserSpawn;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Material laserNormalMaterial;
    [SerializeField] private Material laserHitMaterial;
    [SerializeField] private MeshRenderer laserMeshRenderer;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float shootRange;
    [SerializeField] private float fireRate;
    [SerializeField] private float laserDuration;
    [SerializeField] private int damage;
    
    private float _fireTimer;
    private LineRenderer _lineRenderer;
    private bool _isAimingAEnemy;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void Update()
    {
        ShootHandler();
        // DebugRaycast();
    }

    private void ShootHandler()
    {
        _fireTimer += Time.deltaTime;
        if (Physics.Raycast(laserSpawn.position, laserSpawn.forward, out RaycastHit hit, shootRange))
        {
            if (hit.collider.gameObject.CompareTag("Enemy")) //todo compararlo por layermask en vez de tag
            {
                ChangeMaterial(laserHitMaterial);
                _isAimingAEnemy = true; 
            }
            else
            {
                ChangeMaterial(laserNormalMaterial);
                _isAimingAEnemy = false;
            }
        }
        
        if (!Input.GetMouseButtonDown(0) || !(_fireTimer >= fireRate)) return;
        _fireTimer = 0;
        _lineRenderer.SetPosition(0, laserSpawn.position);
        _lineRenderer.SetPosition(1, laserSpawn.position + (laserSpawn.forward * shootRange));
        
        if (_isAimingAEnemy)
        {
            hit.transform.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }

        StartCoroutine(ShootLaser());

    }

    private IEnumerator ShootLaser()
    {
        laserMeshRenderer.enabled = false;
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        _lineRenderer.enabled = false;
        yield return new WaitForSeconds(laserDuration * 1.5f);
        laserMeshRenderer.enabled = true;
    }

    private void ChangeMaterial(Material material)
    {
        laserMeshRenderer.material = material;
    }
    
    private void DebugRaycast()
    {
        Debug.DrawRay(laserSpawn.position, laserSpawn.forward * shootRange, Color.red);
        if (Physics.Raycast(laserSpawn.position, laserSpawn.forward, out RaycastHit hit, shootRange))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
    
}
