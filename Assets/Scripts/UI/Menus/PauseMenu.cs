using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
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

    public void ExitToMainMenu()
    {
        _gameManager.ChangeScene(0);
    }
}
