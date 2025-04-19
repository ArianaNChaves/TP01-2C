using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;

public class npcHealth : MonoBehaviour, IDamageable
{
    public static UnityEvent OnKillNPC = new UnityEvent();
    [SerializeField] private int healthPoints = 10;

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
            Die();
        }
    }

    private void Die()
    {
        OnKillNPC?.Invoke();
        Destroy(this.gameObject);
    }
}
