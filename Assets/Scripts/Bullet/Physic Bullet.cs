using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PhysicBullet : BaseBullet
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private Rigidbody rigidbody;
    
    private void FixedUpdate()
    {
        if (!_isActivated) return;
        Fire();
    }
    
    protected override void Fire()
    {
        rigidbody.velocity = _direction * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Utilities.CompareLayerAndMask(collisionLayer, other.gameObject.layer))
        {
            //si hubo un choque podria poner particulas de bum
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
            _isActivated = false;
            ReturnObjectToPool();
        }
    }
    
    public override void CalculateShoot(Transform spawnTransform)
    {
        _isActivated = true;
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;
        _direction = spawnTransform.forward;
    }
}
