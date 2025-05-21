using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject playerPanel;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UpdateHighScoreDisplay();
    }

    private void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = $"{highScore}";
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "City") return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!menuPanel.activeInHierarchy)
            {
                ActivateMenuPanel(true);
            }
            else
            {
                ActivateMenuPanel(false);
            }
        }
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
        exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnPlayButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "City")
        {
            ActivateMenuPanel(false);
        }
        else
        {
            SceneManager.LoadScene("City");
        }
    }
    
    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void ActivateMenuPanel(bool active)
    {
        if (active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerPanel.SetActive(false);
            menuPanel.SetActive(true);
            Time.timeScale = 0;
            UpdateHighScoreDisplay();
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            menuPanel.SetActive(false);
            playerPanel.SetActive(true);
            Time.timeScale = 1;
        }
    }
}
