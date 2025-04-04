using System;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private PlayerHealth playerHealthScript;

    private void Start()
    {
        playerHealth.text = playerHealthScript.GetHealth().ToString();
    }
    
    public void UpdateHealth()
    {
        playerHealth.text = playerHealthScript.GetHealth().ToString();

    }
}
