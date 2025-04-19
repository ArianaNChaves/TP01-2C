using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static UnityEvent OnKillEnemy = new UnityEvent();
    [SerializeField] private int health;
    

    public void TakeDamage(int damage) //todo agregar alguna UI o algo para ver la vida de los enemigos?
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            OnKillEnemy?.Invoke();
            Destroy(gameObject);
        }
    }
}
