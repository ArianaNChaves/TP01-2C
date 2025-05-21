using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HIGH_SCORE_KEY = "HighScore";
    
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private int _score;
    private int _highScore;
    
    private void Start()
    {
        _score = 0;
        _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        UpdateScore();
        EnemyHealth.OnKillEnemy.AddListener(AddScore);
        NpcHealth.OnKillNPC.AddListener(RestScore);
    }

    private void OnDestroy()
    {
        EnemyHealth.OnKillEnemy.RemoveListener(AddScore);
        NpcHealth.OnKillNPC.RemoveListener(RestScore);
        
        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, _highScore);
            PlayerPrefs.Save();
        }
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

    public int GetHighScore()
    {
        return _highScore;
    }
}
