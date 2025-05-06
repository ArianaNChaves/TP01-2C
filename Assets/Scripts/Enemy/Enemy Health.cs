using UnityEngine;
using UnityEngine.Events;

// 3) Enemy
public class EnemyHealth : HealthBase
{
    public static UnityEvent OnKillEnemy = new UnityEvent();
    
    protected override void OnDeath()
    {
        OnKillEnemy?.Invoke();
        Destroy(gameObject);
    }
}