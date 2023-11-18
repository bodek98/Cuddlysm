using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerEntity _player;
    [SerializeField] private PlayerHealthBar _playerHealthBar;
    private float lastPlayerHealth, lastPlayerMaxHealth;

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
        HandlePlayerHealth();
    }

    private void HandlePlayerHealth()
    {
        if (!HealthChanged()) return;
        
        _playerHealthBar.UpdateHealthBar(_player.Health, _player.MaxHealth);
    }

    private bool HealthChanged()
    {
        return _player.Health == lastPlayerHealth || _player.MaxHealth == lastPlayerMaxHealth;
    }
}
