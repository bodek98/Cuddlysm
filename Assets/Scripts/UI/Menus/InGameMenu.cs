using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private GameManager _gameManager;

    public void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void ResumeGame()
    {
        _gameManager.UnpauseGame();
    }
    
    public void RestartGame()
    {
        _gameManager.ChangeScene(1);
        _gameManager.UnpauseGame();
        gameObject.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        _gameManager.ChangeScene(0);
    }
}
