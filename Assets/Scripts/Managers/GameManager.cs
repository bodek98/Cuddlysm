using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerEntity _player;
    [SerializeField] private PlayerHealthBar _playerHealthBar;
    private int lastPlayerHealth, lastPlayerMaxHealth;

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        lastPlayerHealth = _player.Health;
        lastPlayerMaxHealth = _player.MaxHealth;
    }
    
    private void Update()
    {
        handlePlayerHealth();
    }

    private void handlePlayerHealth()
    {
        if (!healthChanged()) return;
        
        _playerHealthBar.UpdateHealthBar(_player.Health, _player.MaxHealth);
    }

    private bool healthChanged()
    {
        return _player.Health == lastPlayerHealth || _player.MaxHealth == lastPlayerMaxHealth;
    }
}
