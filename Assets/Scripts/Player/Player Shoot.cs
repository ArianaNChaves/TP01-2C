using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform laserSpawn;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject laserGameObject;
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
    private bool _isLaserActive;
    [SerializeField] private PlayerCameraController _playerCameraController;
    
    private void Awake()
    {
        // _playerCameraController = GetComponent<PlayerCameraController>();
        _lineRenderer = GetComponent<LineRenderer>();
        _isLaserActive = true;
        laserGameObject.SetActive(_isLaserActive);
    }
    
    private void Update()
    {
        ShootHandler();
        LaserSwitch();
        // DebugRaycast();
    }

    private void ShootHandler()
    {
        if (!_isLaserActive) return;
        
        _fireTimer += Time.deltaTime;
        IDamageable currentTarget = null;
        if (Physics.Raycast(laserSpawn.position, laserSpawn.forward, out RaycastHit hit, shootRange))
        {
            currentTarget = hit.collider.GetComponent<IDamageable>();
            if (Utilities.CompareLayerAndMask(enemyLayerMask, hit.collider.gameObject.layer))
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
        ShootType(currentTarget);
        
    }

    private void ShootType(IDamageable currentTaget)
    {
        switch (_playerCameraController.GetCurrentCameraView())
        {
            case PlayerCameraController.CameraView.FirstPerson:
                PhysicBullet bullet = PoolManager.Instance.Get<PhysicBullet>();
                bullet.gameObject.SetActive(true);
                bullet.CalculateShoot(bulletSpawnPoint.position, transform.forward, transform.rotation);
                break;
            
            case PlayerCameraController.CameraView.ThirdPerson:
                _lineRenderer.SetPosition(0, laserSpawn.position);
                _lineRenderer.SetPosition(1, laserSpawn.position + (laserSpawn.forward * shootRange));
                if (_isAimingAEnemy && currentTaget != null)
                {
                    currentTaget.TakeDamage(damage);
                }
                StartCoroutine(ShootLaser());
                break;
            
            default:
                Debug.LogError("PlayerShoot.cs - AimHandler - CameraView no definida");
                break;
        }
    }

    private void LaserSwitch()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        _isLaserActive = !_isLaserActive;
        laserGameObject.SetActive(_isLaserActive);

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
