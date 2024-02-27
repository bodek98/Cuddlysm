using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _deathMenu = null;

    public void SetPauseMenuActive(bool shouldBeActive)
    {
        _pauseMenu.SetActive(shouldBeActive);
    }
    
    public void SetDeathMenuActive(bool shouldBeActive)
    {
        _deathMenu.SetActive(shouldBeActive);
    }
}
