using System;
using System.Collections;
using UnityEngine;

public class MortarShoot : MonoBehaviour
{
    [Header("Morter Settings")]
    [SerializeField] private PoolController poolController;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float aimDistance;
    
    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootRate;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private int bulletDamage;
    
    [SerializeField] private GameObject bulletPrefab; //todo borrar y aplicar poolcontroller

    private bool _canShoot;
    private Vector3 _direction;
    private Vector3 _endHit;
    private float _timer;

    private void Update()
    {
        Shoot();
        DebugRaycast();
    }

    private bool CanShoot()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (!(distance <= aimDistance)) return false;
        _direction = (target.position - bulletSpawnPoint.position).normalized;
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
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity); //todo borrar y aplicar poolcontroller
            bullet.GetComponent<Bullet>().Activate(bulletSpawnPoint.position, _endHit, bulletSpeed, bulletLifeTime);
            _timer = 0;
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
