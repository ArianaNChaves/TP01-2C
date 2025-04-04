using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private PlayerUI playerUI;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Death();
        }
        
        playerUI.UpdateHealth();
    }
    
    public int GetHealth()
    {
        return health;
    }

    private void Death()
    {
        Debug.Log("Muerte y agonia");
        //todo Implementar que pasa cuando el player muere
    }
    
}
