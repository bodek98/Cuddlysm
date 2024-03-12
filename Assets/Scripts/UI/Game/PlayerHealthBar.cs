using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _easeHealthBar;
    [SerializeField] private float _lerpSpeed = 1f;

    private float _currentHealth = 0f;

    private void Update()
    {
        if (_healthBar && _easeHealthBar && _healthBar.value != _easeHealthBar.value)
        {
            _easeHealthBar.value = Mathf.Lerp(_easeHealthBar.value, _currentHealth, _lerpSpeed);
        }
    }
    public void UpdateHealthBar(float health, float maxHealth)
    {
        _currentHealth = health / maxHealth;
        _healthBar.value = _currentHealth;
    }
}
