using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager _gameManager;

    public void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void OnPlayButton()
    {
        _gameManager.ChangeScene(1);
    }

    public void OnSettingsButton()
    {
        Debug.Log("TODO: Settings");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
