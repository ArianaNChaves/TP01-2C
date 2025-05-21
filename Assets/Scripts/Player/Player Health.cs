using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// 2) Player
public class PlayerHealth : HealthBase
{
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private string gameOverSceneName = "Main Menu"; 

    protected override void OnDamage(int damage)
    {
        playerUI.UpdateHealth();
    }

    protected override void OnDeath()
    {
        // Clear all pooled enemies before loading the game over scene
        PoolManager.Instance.ClearPool();
        SceneManager.LoadScene(gameOverSceneName);
    }
}