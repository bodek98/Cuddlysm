using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    private GameManager _gameManager;
    
    public void Start()
    {
        _gameManager = GameManager.Instance;
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _gameManager.PauseOrUnpause();
        } 
    }
}
