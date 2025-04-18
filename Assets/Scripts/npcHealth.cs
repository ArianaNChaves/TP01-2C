using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class npcHealth : MonoBehaviour, IDamageable
{
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
        Destroy(this.gameObject);
    }
}
