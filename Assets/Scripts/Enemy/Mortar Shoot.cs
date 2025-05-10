using System;
using System.Collections;
using UnityEngine;

public class MortarShoot : MonoBehaviour
{
    [Header("Morter Settings")]
    // [SerializeField] private GenericPoolController poolController;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject target;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float aimDistance;
    
    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootRate;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private int bulletDamage;
    
    private bool _canShoot;
    private Vector3 _direction;
    private Vector3 _endHit;
    private float _timer;
    // private const string BulletPoolName = "bullet-pool";
    
    private void Update()
    {
        Shoot();
        DebugRaycast();
    }

    private bool CanShoot()
    {
        if (!target) return false;
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (!(distance <= aimDistance)) return false;
        _direction = (target.transform.position - bulletSpawnPoint.position).normalized;
        RaycastHit hit;
            
        if (Physics.Raycast(bulletSpawnPoint.position, _direction, out hit, distance))
        {
            bool hitIsTarget = Utilities.CompareLayerAndMask(targetLayer, hit.collider.gameObject.layer);
            _endHit = hit.point;
            
            return hitIsTarget;
        }
        else
        {
            return false;
        }
    }

    private void Shoot()
    {
        if (!CanShoot()) return;
        _timer += Time.deltaTime;
        if (_timer >= 2f)
        {
            Bullet bullet = PoolManager.Instance.Get<Bullet>();
            bullet.gameObject.SetActive(true);
            bullet.Activate(bulletSpawnPoint.position, _endHit, bulletSpeed, bulletLifeTime);
            
            _timer = 0;
            if (target && target.TryGetComponent(out PlayerHealth health))
            {
                health.TakeDamage(bulletDamage);
            }
            else
            {
                Debug.LogError($"El player no tiene el scrip de PlayerHealth o no existe");
            }
        }

    }
    
    private void DebugRaycast()
    {
        Debug.DrawRay(bulletSpawnPoint.position, _direction * aimDistance, Color.red);
        if (Physics.Raycast(bulletSpawnPoint.position, _direction, out RaycastHit hit, aimDistance))
        {
            // Debug.Log("Hit: " + hit.collider.name);
        }
    }
    
}
