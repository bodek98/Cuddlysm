using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _menuCamera;
    [SerializeField] private GameObject _creditsCamera;

    public void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void OnPlayButton()
    {
        _gameManager.ChangeScene(1);
    }

    public void OnCreditsButton()
    {
        _creditsCamera.SetActive(true);
        _menuCamera.SetActive(false);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnQuitCreditsButton()
    {
        Debug.Log("ELO");
        _menuCamera.SetActive(true);
        _creditsCamera.SetActive(false);
    }
}
