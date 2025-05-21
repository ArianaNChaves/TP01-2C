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
        SceneManager.LoadScene(gameOverSceneName);
    }
}