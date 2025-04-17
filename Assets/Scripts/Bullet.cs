using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private float _speed;
    private bool _isActivated;
    private void Update()
    {
        if (!_isActivated) return;
        Fire();
    }

    private void Fire()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }
    
    public void Activate(Vector3 start, Vector3 end, float speed, float lifeTime)
    {
        transform.position = start;
        _direction = (end - start).normalized;
        _speed = speed;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        _isActivated = true;
        Destroy(gameObject, lifeTime);
    }
    
}
