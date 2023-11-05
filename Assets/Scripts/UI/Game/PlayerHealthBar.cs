using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform _healthBar, _healthBarContainer;
    private float _width, _height;

    private void Start()
    {
        _width = _healthBarContainer.rect.width;
        _height = _healthBarContainer.rect.height;
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        float newWidth = health / maxHealth * _width;

        _healthBar.sizeDelta = new Vector2(newWidth, _height);
    }
}
