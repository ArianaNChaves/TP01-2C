using System;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private PlayerHealth playerHealthScript;

    private void Start()
    {
        UpdateHealth();
    }
    
    public void UpdateHealth()
    {
        playerHealth.text = playerHealthScript.GetHealth().ToString(); //todo Esto podria ser un evento para sacar la referencia al player y que haya un Game Manager

    }
}
