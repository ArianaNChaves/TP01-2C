using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    private float _timer;
    
    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!(_timer >= 0.5f)) return;
        _timer = 0;
        other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);

    }
}
