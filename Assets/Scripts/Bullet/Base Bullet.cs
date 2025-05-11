using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IPooleable
{
    //todo Esto podria cambiarlo a un scriptable object
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected int lifeTime;

    protected Vector3 _direction;
    protected bool _isActivated;
    
    protected virtual void Fire(){}
    public virtual void CalculateShoot(Vector3 initialPosition){}
    public virtual void CalculateShoot(Vector3 initialPosition, Vector3 finalPoisition){}

    public float GetSpeed()
    {
        return speed;
    }
    public int GetDamage()
    {
        return damage;
    }

    public void ReturnObjectToPool()
    {
        PoolManager.Instance.ReturnToPool(this);
    }

}
