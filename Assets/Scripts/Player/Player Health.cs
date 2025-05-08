using UnityEngine;
using UnityEngine.Events;

// 2) Player
public class PlayerHealth : HealthBase
{
    [SerializeField] private PlayerUI playerUI;

    protected override void OnDamage(int damage)
    {
        playerUI.UpdateHealth();
    }

    protected override void OnDeath()
    {
        Debug.Log("Muerte y agonía");
    }
}