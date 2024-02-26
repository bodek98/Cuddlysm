using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool isGamePaused = false;

    private int _currentSceneId = 0;
    private MainGameCanvas _mainGameCanvas;
    
    private new void Awake()
    {
        base.Awake();
        _currentSceneId = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadInGameDependencies(scene.buildIndex);
    }

    public void ChangeScene(int sceneId)
    {
        _currentSceneId = sceneId;
        SceneManager.LoadScene(_currentSceneId);
        UnpauseGame();
        UpdateTimeScale();
        ClearInGameDependencies();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        HandleGameStateChanged();
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        HandleGameStateChanged();
    }

    public void PauseOrUnpause()
    {
        if (!isGamePaused)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }

    private void UpdateTimeScale()
    {
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    private void HandleGameStateChanged()
    {
        _mainGameCanvas?.SetPauseMenuActive(isGamePaused);
        UpdateTimeScale();
    }
    
    private void LoadInGameDependencies(int sceneId)
    {
        switch (sceneId)
        {
            case 1:
                GameObject mainCanvas = LocateObject("MainGameCanvas");
                _mainGameCanvas = mainCanvas?.GetComponent<MainGameCanvas>();
                break;
        }
    }

    private void ClearInGameDependencies()
    {
        _mainGameCanvas = null;
    }
    
    GameObject LocateObject(string name)
    {
        var go = GameObject.FindGameObjectWithTag(name);
        if(go == null)
        {
            Debug.LogError("Unable to locate " + name);
        }
        return go;
    }
}
