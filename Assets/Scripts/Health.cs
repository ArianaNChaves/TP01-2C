using System;
using UnityEngine;

public abstract class HealthBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health;

    public virtual void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);
        OnDamage(damage);

        if (health == 0)
            OnDeath();
    }

    public int GetHealth() => health;

    protected virtual void OnDamage(int damage) { }

    protected abstract void OnDeath();
}