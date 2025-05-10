using UnityEngine;
using UnityEngine.Events;

// 4) NPC
public class NpcHealth : HealthBase
{
    public static UnityEvent OnKillNPC = new UnityEvent();

    protected override void OnDeath()
    {
        OnKillNPC?.Invoke();
        Destroy(gameObject);
    }
}