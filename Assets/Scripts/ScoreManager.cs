using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private int _score;
    
    private void Start()
    {
        _score = 0;
        UpdateScore();
        EnemyHealth.OnKillEnemy.AddListener(AddScore);
        NpcHealth.OnKillNPC.AddListener(RestScore);
    }

    private void OnDestroy()
    {
        EnemyHealth.OnKillEnemy.RemoveListener(AddScore);
        NpcHealth.OnKillNPC.RemoveListener(RestScore);
    }
    
    private void UpdateScore()
    {
        scoreText.text = _score.ToString();
    }
    
    private void AddScore()
    {
        _score++;
        UpdateScore();
    }

    private void RestScore()
    {
        _score--;
        if (_score <= 0)
        {
            _score = 0;
        }
        UpdateScore();
    }
}
