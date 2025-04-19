using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject playerPanel;
    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
            if (!menuPanel.activeInHierarchy)
            {
                menuPanel.SetActive(true);
            }   
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
}
