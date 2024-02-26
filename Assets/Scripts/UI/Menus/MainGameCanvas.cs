using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu = null;

    public void SetPauseMenuActive(bool shouldBeActive)
    {
        _pauseMenu.SetActive(shouldBeActive);
    }
}
