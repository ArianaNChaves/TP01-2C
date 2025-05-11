using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBullet : BaseBullet
{
    private void Update()
    {
        if (!_isActivated) return;
        Fire();
    }

    protected override void Fire()
    {
        transform.position += _direction * (speed * Time.deltaTime);
    }

    public override void CalculateShoot(Vector3 initialPosition, Vector3 finalPosition)
    {
        transform.position = initialPosition;
        _direction = (finalPosition - initialPosition).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        _isActivated = true;
        Invoke(nameof(ReturnObjectToPool),lifeTime);
    }
}
